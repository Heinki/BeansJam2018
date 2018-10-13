using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KotzManager : MonoBehaviour {

    
    private bool gameSolved = false;

	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(touch.position);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "Kotze")
                {
                    listKotze.Remove(hit.transform.gameObject);
                    Destroy(hit.transform.gameObject);
                }
            }
        }
        if(listKotze.Count == 0)
        {
            gameSolved = true;
        }
    }
}
