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
    public float decreaseValue = 0.1f;
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

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }


    void Start () {
        currGame = CurrGame.game1;
        guestoMeter = GameObject.Find("Guestometer").GetComponent<Slider>();
        currentTimeTilProblem = Random.Range(range.x, range.y);
    }
	
	void Update () {
        if (whackManager != null)
        {
            deductPoints();
            ActivateRandomIssue();
            AddScore();
            CheckGuestoMeter();
        }
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
        probAcc += Time.deltaTime;

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
