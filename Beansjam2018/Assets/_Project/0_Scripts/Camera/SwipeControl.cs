using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


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
        timerMax = 0.5f;
    }

    void Update()
    {
        checkSwipe();
        checkAnimation();
    }

    void checkAnimation()
    {
        if(anim && timer <= timerMax)
        {
            timer += Time.deltaTime;
            transform.Translate(new Vector3(cameraSpeed * Time.deltaTime, 0, 0));
        } 
        else
        {
            anim = false;
            timer = 0;
        }
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
                        cameraSpeed = 7.5f;
                    }
                    else
                    {
                        debug.text = "SWIPE LEFT";
                        cameraSpeed = -7.5f;
                    }
                }
                
            }
        } 
    }
}
