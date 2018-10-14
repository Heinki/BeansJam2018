using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public enum CurrGame { game1, game2, game3, game4 };

    static public CurrGame currGame;

    static float highscore;
    static int boothsRescued;
    static float timeSurvived;


    float maxGuests;
    float currentGuests;
    public float decreaseValue = 0.001f;
    static public int amountOfIssues = 0;


    static public Slider guestoMeter;

    //MANAGERS
    public WhackManager whackManager;
    public MotionManager motionManager;
    public WaterballGameManager waterBallManager;
    public KotzManager kotzManager;
    public SceneLoader sceneloader;

    public Vector2 range = new Vector2(2f, 4f);
    private float currentTimeTilProblem;
    private float probAcc = 0f;

    Text gameText;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }


    void Start () {
        currGame = CurrGame.game1;
        guestoMeter = GameObject.Find("Guestometer").GetComponent<Slider>();
        currentTimeTilProblem = Random.Range(range.x, range.y);
        gameText = GameObject.Find("GameText").GetComponent<Text>();
    }
	
	void Update () {
        if (whackManager != null)
        {
            deductPoints();
            ActivateRandomIssue();
            AddScore();
            CheckGuestoMeter();
            CheckGameText();
        }
	}

    void CheckGameText()
    {
        if(currGame == CurrGame.game1) {
            gameText.text = "HIT!";
        } else if (currGame == CurrGame.game2) {
            gameText.text = "SHAKE!";
        } else if (currGame == CurrGame.game3) {
            gameText.text = "WIPE!";
        } else if (currGame == CurrGame.game4) {
            gameText.text = "THROW!";
        }
    }

    public static void ResetHighScore()
    {
        highscore = 0;
        boothsRescued = 0;
        timeSurvived = 0;
    }

    public static int GetRescueValue()
    {
        return boothsRescued;
    }

    public static float GetTimeSurvived()
    {
        return Mathf.Round(timeSurvived);
    }

    public static float GetHighscore()
    {
        return Mathf.Round(highscore);
    }


    private void CheckGuestoMeter()
    {
        if (guestoMeter.value <= 0)
        {
            amountOfIssues = 0;
            highscore = boothsRescued * timeSurvived;
            sceneloader.LoadGameOverScene();
        }
    }

    public static void AddRescued()
    {
        boothsRescued++;
    }

    void AddScore()
    {
        timeSurvived += Time.deltaTime;
    }

    void deductPoints()
    {
        guestoMeter.value -= Time.deltaTime * decreaseValue * amountOfIssues;
    }

    static public void AddPoints(float value)
    {
        guestoMeter.value += value;
    }

    static public void IncreaseAmountOfIssues()
    {
        Mathf.Clamp(amountOfIssues++, 0, 4);
    }

    static public void DecreaseAmountOfIssues()
    {
        Mathf.Clamp(amountOfIssues--, 0, 4);
    }

    public void ActivateRandomIssue()
    {
        if (amountOfIssues < 4 )
        {
            probAcc += Time.deltaTime;
        }

        if(probAcc >= currentTimeTilProblem)
        {
            probAcc = 0;
            bool issueSpawned = false;

            while(!issueSpawned && amountOfIssues < 4)
            {
                int index = Random.Range(0, 4);
                if(index == 0)
                {
                    if(motionManager.game_solved)
                    {
                        motionManager.ResetIssue();
                        issueSpawned = true;
                    }
                }
                else if(index == 1)
                {
                    if(kotzManager.game_solved)
                    {
                        kotzManager.ResetIssue();
                        issueSpawned = true;
                    }
                }
                else if(index == 2)
                {
                    if(whackManager.game_solved)
                    {
                        whackManager.ResetIssue();
                        issueSpawned = true;
                    }
                }
                else if (index == 3)
                {
                    if(waterBallManager.game_solved)
                    {
                        waterBallManager.ResetIssue();
                        issueSpawned = true;
                    }
                }
            }
        }
    }
    

}
