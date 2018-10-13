using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KotzManager : MonoBehaviour {

    public GameObject kotzList;
    private bool gameSolved = false;

    private GameObject currKotzList;

	// Use this for initialization
	void Start () {
        currKotzList = Instantiate(kotzList, kotzList.transform.position, kotzList.transform.rotation) as GameObject;
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
                    currKotzList.GetComponent<KotzListe>().kotzListe.Remove(hit.transform.gameObject);
                    Destroy(hit.transform.gameObject);
                }
            }
        }
        if(currKotzList.GetComponent<KotzListe>().kotzListe.Count == 0)
        {
            Destroy(currKotzList);
            currKotzList = Instantiate(kotzList, kotzList.transform.position, kotzList.transform.rotation) as GameObject;
            gameSolved = true;
        }
    }
}
