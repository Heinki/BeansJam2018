using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTester : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.U))
        {
            SoundManager.instance.PlayRandomSFX_AUA();
        } else if (Input.GetKeyDown(KeyCode.I))
        {
            SoundManager.instance.PlayRandomSFX_KOTZE();
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            SoundManager.instance.PlayRandomSFX_WELCOME();
        }
    }
}
