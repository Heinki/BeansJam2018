﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class WhackManager : MonoBehaviour, IProblem {

    public AudioSource fx_punch;

    Ray ray;
    RaycastHit hit;
    GameObject[] whackables;
    public bool game_solved = false;
    int hitItems;


    // Use this for initialization
    void Start () {
        GameManager.IncreaseAmountOfIssues();
        whackables = GameObject.FindGameObjectsWithTag("Whack");
        hitItems = 0;
    }
	
	// Update is called once per frame
	void Update () {
        clicked();
        touched();

    }

    void touched() {
        for (int i = 0; i < Input.touchCount; i++) {
            ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "Whack")
                {
                    GameObject whacked = hit.transform.gameObject;
                    WhackObject whackedObject = whacked.GetComponent<WhackObject>();

                    if (!whackedObject.getTouched())
                    {
                        fx_punch.Play();
                        whackedObject.setTouched(true);
                        hitItems++;

                        if (hitItems == whackables.Length)
                        {
                            this.game_solved = true;
                            GameManager.DecreaseAmountOfIssues();
                        }
                    }
                }
            }
        }
    }

    void clicked()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.tag == "Whack")
            {
                GameObject whacked = hit.transform.gameObject;
                WhackObject whackedObject = whacked.GetComponent<WhackObject>();

                if (!whackedObject.getTouched())
                {
                    fx_punch.Play();
                    whackedObject.setTouched(true);
                    hitItems++;

                    if (hitItems == whackables.Length)
                    {
                        this.game_solved = true;
                        GameManager.DecreaseAmountOfIssues();
                    }
                }

            }
        }
    }

    public void ResetIssue()
    {
        if(this.game_solved)
        {
            for (int i = 0; i < whackables.Length; i++)
            {
                whackables[i].GetComponent<WhackObject>().resetObject();
            }

            this.game_solved = false;
            GameManager.IncreaseAmountOfIssues();
        }
       
    }
}
