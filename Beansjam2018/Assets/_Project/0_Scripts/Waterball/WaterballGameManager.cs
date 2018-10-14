using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterballGameManager : MonoBehaviour, IProblem {

    public GameObject waterballRedPrefab;
    public GameObject waterballBluePrefab;
    public float respawnTime;
    public GameObject fireListPrefab;
    public GameObject currFireList;
    public Transform fireStartPos;
    public Transform waterballoonStartPos;
    public AudioSource fx_fire;

    public bool waterball_shot = false;
    public bool game_solved = false;

    //Private Logic
    private float respawnAcc = 0f;
    private int fireNumStart;

	// Use this for initialization
	void Start () {
        GameManager.IncreaseAmountOfIssues();
        currFireList = Instantiate(fireListPrefab, fireStartPos.position, fireListPrefab.transform.rotation, fireStartPos.transform) as GameObject;
        //currFireList = Instantiate(fireListPrefab, fireStartPos.transform.position, fireListPrefab.transform.rotation) as GameObject;
        fx_fire.Play();
        fireNumStart = currFireList.GetComponent<FireList>().fireList.Count;
    }
	
	// Update is called once per frame
	void Update () {
        if (waterball_shot)
        {
            respawnAcc += Time.deltaTime;
            if(respawnAcc > respawnTime)
            {
                Respawn();
                respawnAcc = 0f;
                waterball_shot = false;
            }
        }
        if(currFireList.GetComponent<FireList>().fireList.Count == 0)
        {
            Destroy(currFireList);
            game_solved = true;
            GameManager.DecreaseAmountOfIssues();
            GameManager.AddPoints(8.0f);
            GameManager.AddRescued();
            fx_fire.Stop();

        }
	}

    public void ResetIssue()
    {
        currFireList = Instantiate(fireListPrefab, fireStartPos.transform.position, fireListPrefab.transform.rotation) as GameObject;
        GameManager.IncreaseAmountOfIssues();
        game_solved = false;
        fx_fire.Play();
    }

    private void Respawn()
    {
        int ran = Random.Range(0, 2);
        if (ran == 0)
        {
            Instantiate(waterballBluePrefab, waterballoonStartPos.position, waterballBluePrefab.transform.rotation);
        } else
        {
            Instantiate(waterballRedPrefab, waterballoonStartPos.position, waterballRedPrefab.transform.rotation);
        }
    }
}
