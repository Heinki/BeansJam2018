using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticle : MonoBehaviour {

    public float timeTildeath = 1.0f;
    public float acc = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        acc += Time.deltaTime;

        if(acc >= timeTildeath)
        {
            Destroy(this.gameObject);
        }
	}
}
