using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterballController : MonoBehaviour {

    public float yDistToSwipe;
    public float belowYDestroy;
    public Vector3 testForce;

    //Private References
    Rigidbody rb;

    //Private Logic
    private bool waterballTouched = false;

    private Vector3 firstTouch;
    private Vector3 lastTouch;

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
        CheckWaterBallTouch();
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
        if(Input.touchCount == 1)
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
        }
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
        if(other.tag == "Fire")
        {
            GameObject.FindGameObjectWithTag("WaterballGamemanager").GetComponent<WaterballGameManager>().fireList.Remove(other.gameObject);
            Destroy(other.gameObject);
        }
    }

}
