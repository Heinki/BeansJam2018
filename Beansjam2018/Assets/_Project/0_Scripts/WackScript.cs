using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class WackScript : MonoBehaviour {

    Ray ray;
    RaycastHit hit;    

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        touched();
	}

    void touched() {
        for (int i = 0; i < Input.touchCount; i++) {
            ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "Wack")
                {
                    Destroy(hit.transform.gameObject);
                }
            }
        }
      
    }
}
