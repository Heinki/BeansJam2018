using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterballController : MonoBehaviour {

    public float yDistToSwipe;

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
        CheckForSwipe();
        CheckWaterBallTouch();

    }

    private void CheckWaterBallTouch()
    {
        //MAUSINPUT
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "waterball")
                {
                    waterballTouched = true;
                    Debug.Log("waterball clicked!!");
                }
            }
        } else if (Input.touchCount == 0 || Input.GetMouseButtonUp(0))
        {
            if (waterballTouched)
            {
                waterballTouched = false;
            }
        } 
        //TOUCHINPUT || 
        if(Input.touchCount == 1){
            Touch touch = Input.GetTouch(0);
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(touch.position);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "waterball")
                {
                    waterballTouched = true;
                    Debug.Log("waterball clicked!!");
                }
            }
        } else if(Input.touchCount == 0)
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
                float xDist = firstTouch.x - lastTouch.x;
                float yDist = firstTouch.y - lastTouch.y;

                if(yDist > yDistToSwipe && waterballTouched)
                {
                    AddForceToBall(new Vector2(xDist, yDist));
                }
            }
        }
    }

    private void AddForceToBall(Vector2 force)
    {
        rb.AddForce(new Vector3(force.x, 0f, force.y));
    }

}
