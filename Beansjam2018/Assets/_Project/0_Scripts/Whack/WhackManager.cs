using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class WhackManager : MonoBehaviour, IProblem {

    public GameObject boot_red;
    public GameObject boot_green;

    Ray ray;
    RaycastHit hit;
    GameObject[] whackables;
    public bool game_solved = false;
    int hitItems;


    // Use this for initialization
    void Start () {
        boot_green.SetActive(false);
        boot_red.SetActive(true);
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
                        SoundManager.instance.PlayRandomSFX_AUA();
                        SoundManager.instance.PlaySFX_PUNCH();
                        whackedObject.setTouched(true);
                        hitItems++;

                        if (hitItems == whackables.Length)
                        {
                            boot_green.SetActive(true);
                            boot_red.SetActive(false);
                            this.game_solved = true;
                            GameManager.DecreaseAmountOfIssues();
                            GameManager.AddPoints(5.0f);
                            GameManager.AddRescued();
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
                    SoundManager.instance.PlaySFX_PUNCH();
                    SoundManager.instance.PlayRandomSFX_AUA();
                    whackedObject.setTouched(true);
                    hitItems++;

                    if (hitItems == whackables.Length)
                    {
                        boot_green.SetActive(true);
                        boot_red.SetActive(false);
                        this.game_solved = true;
                        GameManager.DecreaseAmountOfIssues();
                        GameManager.AddPoints(5.0f);
                        GameManager.AddRescued();
                    }
                }

            }
        }
    }

    public void ResetIssue()
    {
        for (int i = 0; i < whackables.Length; i++)
        {
            whackables[i].GetComponent<WhackObject>().resetObject();
        }

        hitItems = 0;   
        this.game_solved = false;
        boot_green.SetActive(false);
        boot_red.SetActive(true);
        GameManager.IncreaseAmountOfIssues();
       
    }
}
