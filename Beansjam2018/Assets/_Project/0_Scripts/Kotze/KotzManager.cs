using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KotzManager : MonoBehaviour, IProblem {

    public GameObject kotzList;
    public bool game_solved = false;

    private GameObject currKotzList;

	// Use this for initialization
	void Start () {
        GameManager.IncreaseAmountOfIssues();
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
        if(currKotzList.GetComponent<KotzListe>().kotzListe.Count == 0 && !game_solved)
        {
            Destroy(currKotzList);
            game_solved = true;
            GameManager.AddPoints(5.0f);
            GameManager.DecreaseAmountOfIssues();
            GameManager.AddRescued();
        }
    }


    public void ResetIssue()
    {
        game_solved = false;
        currKotzList = Instantiate(kotzList, kotzList.transform.position, kotzList.transform.rotation) as GameObject;
        GameManager.IncreaseAmountOfIssues();

    }
}
