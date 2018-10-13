using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class WackManager : MonoBehaviour {

    Ray ray;
    RaycastHit hit;
    GameObject[] wackables;
    bool finished = false;
    int hitItems;

    // Use this for initialization
    void Start () {
        wackables = GameObject.FindGameObjectsWithTag("Wack");
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
                if (hit.transform.tag == "Wack")
                {
                    GameObject wacked = hit.transform.gameObject;
                    WackObject wackedObject = wacked.GetComponent<WackObject>();

                    if (!wackedObject.getTouched())
                    {
                        wackedObject.setTouched(true);
                        hitItems++;

                        if (hitItems == wackables.Length)
                        {
                            this.finished = true;
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
            if (hit.transform.tag == "Wack")
            {
                GameObject wacked = hit.transform.gameObject;
                WackObject wackedObject = wacked.GetComponent<WackObject>();

                if (!wackedObject.getTouched())
                {
                    wackedObject.setTouched(true);
                    hitItems++;

                    if (hitItems == wackables.Length)
                    {
                        this.finished = true;
                    }
                }

            }
        }
    }
}
