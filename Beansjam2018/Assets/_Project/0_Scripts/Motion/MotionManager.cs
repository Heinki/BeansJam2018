using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MotionManager : MonoBehaviour, IProblem {

    float accelerometerUpdateInterval = 1.0f / 60.0f;
    float lowPassKernelWidthInSeconds = 1.0f;
    float shakeDetectionThreshold = 2.0f;
    public bool game_solved = false;

    float lowPassFilterFactor;
    Vector3 lowPassValue;
    GameObject[] flies;
   
    void Start()
    {
        GameManager.IncreaseAmountOfIssues();
        lowPassFilterFactor = accelerometerUpdateInterval / lowPassKernelWidthInSeconds;
        shakeDetectionThreshold *= shakeDetectionThreshold;
        lowPassValue = Input.acceleration;
        flies = GameObject.FindGameObjectsWithTag("Flies");
    }

    void Update()
    {
        CheckShake();
    }

    void CheckShake()
    {
        Vector3 acceleration = Input.acceleration;
        lowPassValue = Vector3.Lerp(lowPassValue, acceleration, lowPassFilterFactor);
        Vector3 deltaAcceleration = acceleration - lowPassValue;


        if (game_solved == false)
        {
            if (deltaAcceleration.sqrMagnitude >= shakeDetectionThreshold)
            {
                for (int i = 0; i < flies.Length; i++)
                {
                    FlyObject fly = flies[i].GetComponent<FlyObject>();
                    fly.setPositionOutsideOfScreen();
                }

                game_solved = true;
                GameManager.DecreaseAmountOfIssues();
                GameManager.AddPoints(5.0f);
                GameManager.AddRescued();
            }
        }
    }

    public void ResetIssue()
    {
        for (int i = 0; i < flies.Length; i++)
        {
            FlyObject fly = flies[i].GetComponent<FlyObject>();
            fly.ResetMotion();
        }

        game_solved = false;
        GameManager.IncreaseAmountOfIssues();
    }
}
