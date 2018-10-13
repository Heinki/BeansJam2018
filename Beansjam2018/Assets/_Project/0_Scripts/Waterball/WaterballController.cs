using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterballController : MonoBehaviour {

    public float yDistToSwipe;
    public float belowYDestroy;
    public float lerpFactor;
    public Vector3 testForce;
    public Vector2 screenShake;
    public Vector3 velocity;
    public Vector3 throwMultiplier;
    public AudioSource fx_watersplash;

    //Private References
    Rigidbody rb;

    //Private Logic
    private bool waterballTouched = false;
    private bool throwable = true;
    private bool touched = false;

    private Vector3 firstTouch;
    private Vector3 lastTouch;

    private Vector2 fromTouch, toTouch;
    private float fromToTimeDiff;

    // Use this for initialization
    void Start () {
        rb = this.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddRelativeForce(testForce, ForceMode.Force);
            rb.AddRelativeTorque(testForce, ForceMode.Force);
            GameObject.FindGameObjectWithTag("WaterballGamemanager").GetComponent<WaterballGameManager>().waterball_shot = true;
            rb.useGravity = true;
        }
        CheckForSwipe();
        //CheckWaterBallTouch();
        CheckForSelfDestroy();
    }

    private void CheckForSelfDestroy()
    {
        if(this.transform.position.y < belowYDestroy)
        {
            Destroy(this.gameObject);
        }
    }

    private void CheckWaterBallTouch()
    {
        //MAUSINPUT
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(touch.position);

            if(Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "waterball")
                {
                    waterballTouched = true;
                }
            }
        } else if (Input.touchCount == 0)
        {
            if (waterballTouched)
            {
                waterballTouched = false;
            }
        } 
    }

    private void CheckForSwipe()
    {

        if (Input.touchCount == 1 && throwable)
        {
            touched = true;
            Touch touch = Input.GetTouch(0);
            Vector3 touchPos = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10f));
            this.transform.position = Vector3.Lerp(transform.position, touchPos, Time.deltaTime * lerpFactor) ;
            velocity = new Vector3(touchPos.x - transform.position.x, 0f, touchPos.y - transform.position.y);
        }
        if (Input.touchCount == 0 && touched)
        {
            touched = false;
            throwable = false;
            GameObject.FindGameObjectWithTag("WaterballGamemanager").GetComponent<WaterballGameManager>().waterball_shot = true;
            rb.useGravity = true;
            Debug.Log("FINAL FORCE:" + new Vector3(velocity.x * throwMultiplier.x, velocity.y * throwMultiplier.y, velocity.z * throwMultiplier.z));
            rb.AddRelativeForce(new Vector3(velocity.x * throwMultiplier.x, velocity.y * throwMultiplier.y, velocity.z * throwMultiplier.z), ForceMode.Force);
            rb.AddRelativeTorque(new Vector3(velocity.z * throwMultiplier.z, velocity.x * throwMultiplier.x, 0f), ForceMode.Force);
        }

        //MOUSEMOUSE
        if (Input.GetMouseButton(0) && throwable)
        {
            Vector3 touchPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));
            this.transform.position = Vector3.Lerp(transform.position, touchPos, Time.deltaTime * lerpFactor);
            velocity = new Vector3(touchPos.x - transform.position.x, 0f, touchPos.y - transform.position.y);
            Debug.Log(velocity);
        }
        if (Input.GetMouseButtonUp(0))
        {
            throwable = false;
            GameObject.FindGameObjectWithTag("WaterballGamemanager").GetComponent<WaterballGameManager>().waterball_shot = true;
            rb.useGravity = true;
            Debug.Log("FINAL FORCE:" + new Vector3(velocity.x * throwMultiplier.x, velocity.y * throwMultiplier.y, velocity.z * throwMultiplier.z));
            rb.AddRelativeForce(new Vector3(velocity.x * throwMultiplier.x, velocity.y * throwMultiplier.y, velocity.z * throwMultiplier.z), ForceMode.Force);
        }


        /*if(Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Began)
            {
                firstTouch = touch.position;
                lastTouch = touch.position;
            }
            else if(touch.phase == TouchPhase.Moved)
            {
                lastTouch = touch.position;
            } else if(touch.phase == TouchPhase.Ended)
            {
                lastTouch = touch.position;
                //Calculate X and Y Touch Distances
                float xDist = -(firstTouch.x - lastTouch.x);
                float yDist = -(firstTouch.y - lastTouch.y);

                if(yDist > yDistToSwipe && waterballTouched)
                {
                    AddForceToBall(new Vector2(xDist, yDist));
                }
            }
        }*/
    }

    private void AddForceToBall(Vector2 force)
    {
        GameObject.FindGameObjectWithTag("WaterballGamemanager").GetComponent<WaterballGameManager>().waterball_shot = true;
        //GameObject.FindGameObjectWithTag("Debug").GetComponent<TextMesh>().text = "Debug: Force x: " + System.Math.Round(force.x,2) + " y: " + System.Math.Round(force.y,2);
        rb.useGravity = true;
        rb.AddRelativeTorque(testForce, ForceMode.Force);
        rb.AddRelativeForce(new Vector3(force.x, 0f, force.y));
    }

    private void OnTriggerEnter(Collider other)
    {
        fx_watersplash.Play();
        if (other.tag == "Fire")
        {
            iTween.ShakePosition(Camera.current.gameObject, iTween.Hash("y", screenShake.y, "x", screenShake.x, "time", 0.3f));
            Handheld.Vibrate();
            GameObject.FindGameObjectWithTag("WaterballGamemanager").GetComponent<WaterballGameManager>().currFireList.GetComponent<FireList>().fireList.Remove(other.gameObject);
            GameObject.FindGameObjectWithTag("WaterballGamemanager").GetComponent<WaterballGameManager>().UpdateFireSoundVol();
            Destroy(other.gameObject);
        } else if(other.tag == "waterball_booth")
        {
            Destroy(this.gameObject);
            //SPLASH PARTICLE & SOUND
        }
    }
}
