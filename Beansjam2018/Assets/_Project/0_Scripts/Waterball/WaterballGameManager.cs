using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterballGameManager : MonoBehaviour, IProblem {

    public GameObject waterballPrefab;
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
            GameManager.DecreaseAmountOfIssues();
            game_solved = true;
            GameManager.AddPoints(5.0f);

        }
	}

    public void ResetIssue()
    {
        currFireList = Instantiate(fireListPrefab, fireStartPos.transform.position, fireListPrefab.transform.rotation) as GameObject;
        GameManager.IncreaseAmountOfIssues();
        game_solved = false;
    }

    public void UpdateFireSoundVol()
    {
        float volume = (float)currFireList.GetComponent<FireList>().fireList.Count / (float)fireNumStart;
        fx_fire.volume = volume;
    }

    private void Respawn()
    {
        Instantiate(waterballPrefab, waterballoonStartPos.position, waterballPrefab.transform.rotation);
    }
}
