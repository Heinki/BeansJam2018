using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour {

    Text rescueValue;
    Text timeValue;
    Text totalValue;


	void Start () {
        rescueValue = GameObject.Find("RescuedValue").GetComponent<Text>();
        timeValue = GameObject.Find("SurvivedValue").GetComponent<Text>();
        totalValue = GameObject.Find("TotalValue").GetComponent<Text>();


        rescueValue.text = GameManager.GetRescueValue().ToString();
        timeValue.text = GameManager.GetTimeSurvived().ToString();
        totalValue.text = GameManager.GetHighscore().ToString();

	}
}
