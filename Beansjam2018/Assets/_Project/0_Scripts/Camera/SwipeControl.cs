using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SwipeControl : MonoBehaviour {

    enum CurrGame { game1, game2, game3, game4 };

    public Transform camPosGame1, camPosGame2, camPosGame3, camPosGame4;
    public Transform camPosBetween12, camPosBetween23, camPosBetween34, camPosBetween41;

    public float camLerpFactor;
    public float minDist;

    public Vector2 startPos;
    public Vector2 direction;

    public bool directionChosen;

    private bool cameraMoving = false;
    private bool transitionReached = false;
    private bool dirRight = false;

    private Text debug;

    private CurrGame currGame;

    float timer;
    float timerMax;
    bool anim = false;
    float cameraSpeed;

    private void Start()
    {
        currGame = CurrGame.game1;
        debug = GameObject.Find("Debug").GetComponent<Text>();
    }

    void Update()
    {
        //checkSwipe();
        KeyboardMovement();
        checkAnimation();
        Debug.Log(transitionReached);
       // GameObject.FindGameObjectWithTag("vcam").GetComponent<Ci>
    }

    void KeyboardMovement()
    {
        if (!cameraMoving)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                dirRight = true;
                debug.text = "SWIPE RIGHT";
                switch (currGame)
                {
                    case CurrGame.game1:
                        currGame = CurrGame.game2;
                        cameraMoving = true;
                        break;
                    case CurrGame.game2:
                        currGame = CurrGame.game3;
                        cameraMoving = true;
                        break;
                    case CurrGame.game3:
                        currGame = CurrGame.game4;
                        cameraMoving = true;
                        break;
                    case CurrGame.game4:
                        currGame = CurrGame.game1;
                        cameraMoving = true;
                        break;
                }
            }
            else if ((Input.GetKeyDown(KeyCode.A)))
            {
                dirRight = false;
                debug.text = "SWIPE LEFT";
                switch (currGame)
                {
                    case CurrGame.game1:
                        currGame = CurrGame.game4;
                        cameraMoving = true;
                        break;
                    case CurrGame.game2:
                        currGame = CurrGame.game1;
                        cameraMoving = true;
                        break;
                    case CurrGame.game3:
                        currGame = CurrGame.game2;
                        cameraMoving = true;
                        break;
                    case CurrGame.game4:
                        currGame = CurrGame.game3;
                        cameraMoving = true;
                        break;
                }
            }
        }
    }

    void checkAnimation()
    {
        if (cameraMoving)
        {
            if (!transitionReached)
            {
                if (dirRight)
                {
                    switch (currGame)
                    {
                        case CurrGame.game1:
                            TranslateCam(camPosBetween41, !transitionReached);
                            break;
                        case CurrGame.game2:
                            TranslateCam(camPosBetween12, !transitionReached);
                            break;
                        case CurrGame.game3:
                            TranslateCam(camPosBetween23, !transitionReached);
                            break;
                        case CurrGame.game4:
                            TranslateCam(camPosBetween34, !transitionReached);
                            break;
                    }
                }
                else
                {
                    switch (currGame)
                    {
                        case CurrGame.game1:
                            TranslateCam(camPosBetween12, !transitionReached);
                            break;
                        case CurrGame.game2:
                            TranslateCam(camPosBetween23, !transitionReached);
                            break;
                        case CurrGame.game3:
                            TranslateCam(camPosBetween34, !transitionReached);
                            break;
                        case CurrGame.game4:
                            TranslateCam(camPosBetween41, !transitionReached);
                            break;
                    }
                }
            } else
            {
                    switch (currGame)
                    {
                        case CurrGame.game1:
                            TranslateCam(camPosGame1, !transitionReached);
                            break;
                        case CurrGame.game2:
                            TranslateCam(camPosGame2, !transitionReached);
                            break;
                        case CurrGame.game3:
                            TranslateCam(camPosGame3, !transitionReached);
                            break;
                        case CurrGame.game4:
                            TranslateCam(camPosGame4, !transitionReached);
                            break;
                    }
            }
        }
    }

    void TranslateCam(Transform translateToTransform, bool transitioning)
    {
        if (transitioning)
        {
            if ((this.transform.position - translateToTransform.position).magnitude < minDist)
            {
                transitionReached = true;
                Debug.Log("TRANSITIONDONE!");
            }
        } else
        {
            if ((this.transform.position - translateToTransform.position).magnitude < minDist)
            {
                cameraMoving = false;
                transitionReached = false;
            }
        }
        this.transform.position = Vector3.Lerp(this.transform.position, translateToTransform.position, Time.deltaTime * camLerpFactor);
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
                        dirRight = true;
                        debug.text = "SWIPE RIGHT";
                        switch (currGame)
                        {
                            case CurrGame.game1:
                                currGame = CurrGame.game2;
                                cameraMoving = true;
                                break;
                            case CurrGame.game2:
                                currGame = CurrGame.game3;
                                cameraMoving = true;
                                break;
                            case CurrGame.game3:
                                currGame = CurrGame.game4;
                                cameraMoving = true;
                                break;
                            case CurrGame.game4:
                                currGame = CurrGame.game1;
                                cameraMoving = true;
                                break;
                        }
                    }
                    else
                    {
                        dirRight = false;
                        debug.text = "SWIPE LEFT";
                        switch (currGame)
                        {
                            case CurrGame.game1:
                                currGame = CurrGame.game4;
                                cameraMoving = true;
                                break;
                            case CurrGame.game2:
                                currGame = CurrGame.game1;
                                cameraMoving = true;
                                break;
                            case CurrGame.game3:
                                currGame = CurrGame.game2;
                                cameraMoving = true;
                                break;
                            case CurrGame.game4:
                                currGame = CurrGame.game3;
                                cameraMoving = true;
                                break;
                        }
                    }
                }
                
            }
        } 
    }
}
