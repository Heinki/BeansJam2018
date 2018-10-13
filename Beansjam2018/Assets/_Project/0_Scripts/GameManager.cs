using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public enum CurrGame { game1, game2, game3, game4 };

    static public CurrGame currGame;

    float highscore;

    float maxGuests;
    float currentGuests;
    public float decreaseValue = 2;
    static public int amountOfIssues = 0;


    Slider guestoMeter;

    //MANAGERS
    public WhackManager whackManager;
    public MotionManager motionManager;
    public WaterballGameManager waterBallManager;
    public KotzManager kotzManager;

    public Vector2 range = new Vector2(2f, 4f);
    private float currentTimeTilProblem;
    private float probAcc = 0f;

    void Start () {
        currGame = CurrGame.game1;
        guestoMeter = GameObject.Find("Guestometer").GetComponent<Slider>();
        currentTimeTilProblem = Random.Range(range.x, range.y);
    }
	
	void Update () {
        deductPoints();
        ActivateRandomIssue();
	}

    void deductPoints()
    {
        guestoMeter.value -= Time.deltaTime * decreaseValue * amountOfIssues;
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
