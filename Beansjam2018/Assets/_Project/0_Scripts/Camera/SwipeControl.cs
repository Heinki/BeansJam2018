using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine.Utility;


public class SwipeControl : MonoBehaviour {
    public Vector2 startPos;
    public Vector2 direction;
    public bool directionChosen;
    private Text debug;
    float timer;
    float timerMax;
    bool anim = false;
    float cameraSpeed;

    private void Start()
    {
        debug = GameObject.Find("Debug").GetComponent<Text>();
    }

    void Update()
    {
        checkSwipe();
        //checkAnimation();
       // GameObject.FindGameObjectWithTag("vcam").GetComponent<Ci>
    }

    void checkAnimation()
    {
        
    }

    void checkSwipe()
    {
        // Track a single touch as a direction control.
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Handle finger movements based on touch phase.
            switch (touch.phase)
            {
                // Record initial touch position.
                case TouchPhase.Began:
                    startPos = touch.position;
                    directionChosen = false;
                    break;

                // Determine direction by comparing the current touch position with the initial one.
                case TouchPhase.Moved:
                    direction = touch.position - startPos;
                    break;

                // Report that a direction has been chosen when the finger is lifted.
                case TouchPhase.Ended:
                    directionChosen = true;
                    break;
            }

            if (directionChosen)
            {
                if (startPos.y <= 400)
                {
                    anim = true;
                    if (Mathf.Sign(direction.x) <= 0)
                    {
                        debug.text = "SWIPE RIGHT";
                        
                    }
                    else
                    {
                        debug.text = "SWIPE LEFT";
                        
                    }
                }
                
            }
        } 
    }
}
