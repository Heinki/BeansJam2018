using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterballGameManager : MonoBehaviour {

    public Vector3 spawnPos;
    public GameObject waterballPrefab;
    public float respawnTime;
    public List<GameObject> fireList;

    public bool waterball_shot = false;
    public bool game_solved = false;

    //Private Logic
    private float respawnAcc = 0f;

	// Use this for initialization
	void Start () {
		
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
        if(fireList.Count == 0)
        {
            GameObject.FindGameObjectWithTag("Debug").GetComponent<TextMesh>().text = "GAME SOLVED";
        }
	}

    private void Respawn()
    {
        Instantiate(waterballPrefab, spawnPos, waterballPrefab.transform.rotation);
    }
}
