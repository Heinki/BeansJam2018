using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SwipeControl : MonoBehaviour {

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
    

    float timer;
    float timerMax;
    bool anim = false;
    float cameraSpeed;

    private void Start()
    {
    }

    void Update()
    {
        checkSwipe();
        KeyboardMovement();
        checkAnimation();
    }

    void KeyboardMovement()
    {
        if (!cameraMoving)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                dirRight = true;
                switch (GameManager.currGame)
                {
                    case GameManager.CurrGame.game1:
                        GameManager.currGame = GameManager.CurrGame.game2;
                        cameraMoving = true;
                        break;
                    case GameManager.CurrGame.game2:
                        GameManager.currGame = GameManager.CurrGame.game3;
                        cameraMoving = true;
                        break;
                    case GameManager.CurrGame.game3:
                        GameManager.currGame = GameManager.CurrGame.game4;
                        cameraMoving = true;
                        break;
                    case GameManager.CurrGame.game4:
                        GameManager.currGame = GameManager.CurrGame.game1;
                        cameraMoving = true;
                        break;
                }
            }
            else if ((Input.GetKeyDown(KeyCode.A)))
            {
                dirRight = false;
                switch (GameManager.currGame)
                {
                    case GameManager.CurrGame.game1:
                        GameManager.currGame = GameManager.CurrGame.game4;
                        cameraMoving = true;
                        break;
                    case GameManager.CurrGame.game2:
                        GameManager.currGame = GameManager.CurrGame.game1;
                        cameraMoving = true;
                        break;
                    case GameManager.CurrGame.game3:
                        GameManager.currGame = GameManager.CurrGame.game2;
                        cameraMoving = true;
                        break;
                    case GameManager.CurrGame.game4:
                        GameManager.currGame = GameManager.CurrGame.game3;
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
                    switch (GameManager.currGame)
                    {
                        case GameManager.CurrGame.game1:
                            TranslateCam(camPosBetween41, !transitionReached);
                            break;
                        case GameManager.CurrGame.game2:
                            TranslateCam(camPosBetween12, !transitionReached);
                            break;
                        case GameManager.CurrGame.game3:
                            TranslateCam(camPosBetween23, !transitionReached);
                            break;
                        case GameManager.CurrGame.game4:
                            TranslateCam(camPosBetween34, !transitionReached);
                            break;
                    }
                }
                else
                {
                    switch (GameManager.currGame)
                    {
                        case GameManager.CurrGame.game1:
                            TranslateCam(camPosBetween12, !transitionReached);
                            break;
                        case GameManager.CurrGame.game2:
                            TranslateCam(camPosBetween23, !transitionReached);
                            break;
                        case GameManager.CurrGame.game3:
                            TranslateCam(camPosBetween34, !transitionReached);
                            break;
                        case GameManager.CurrGame.game4:
                            TranslateCam(camPosBetween41, !transitionReached);
                            break;
                    }
                }
            } else
            {
                    switch (GameManager.currGame)
                    {
                        case GameManager.CurrGame.game1:
                            TranslateCam(camPosGame1, !transitionReached);
                            break;
                        case GameManager.CurrGame.game2:
                            TranslateCam(camPosGame2, !transitionReached);
                            break;
                        case GameManager.CurrGame.game3:
                            TranslateCam(camPosGame3, !transitionReached);
                            break;
                        case GameManager.CurrGame.game4:
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
        if (!cameraMoving)
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
                            switch (GameManager.currGame)
                            {
                                case GameManager.CurrGame.game1:
                                    GameManager.currGame = GameManager.CurrGame.game2;
                                    cameraMoving = true;
                                    break;
                                case GameManager.CurrGame.game2:
                                    GameManager.currGame = GameManager.CurrGame.game3;
                                    cameraMoving = true;
                                    break;
                                case GameManager.CurrGame.game3:
                                    GameManager.currGame = GameManager.CurrGame.game4;
                                    cameraMoving = true;
                                    break;
                                case GameManager.CurrGame.game4:
                                    GameManager.currGame = GameManager.CurrGame.game1;
                                    cameraMoving = true;
                                    break;
                            }
                        }
                        else
                        {
                            dirRight = false;
                            switch (GameManager.currGame)
                            {
                                case GameManager.CurrGame.game1:
                                    GameManager.currGame = GameManager.CurrGame.game4;
                                    cameraMoving = true;
                                    break;
                                case GameManager.CurrGame.game2:
                                    GameManager.currGame = GameManager.CurrGame.game1;
                                    cameraMoving = true;
                                    break;
                                case GameManager.CurrGame.game3:
                                    GameManager.currGame = GameManager.CurrGame.game2;
                                    cameraMoving = true;
                                    break;
                                case GameManager.CurrGame.game4:
                                    GameManager.currGame = GameManager.CurrGame.game3;
                                    cameraMoving = true;
                                    break;
                            }
                        }
                    }

                }
            }
        }
    }
}
