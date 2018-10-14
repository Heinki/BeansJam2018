using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

	public void LoadMainScene ()
    {
        SceneManager.LoadScene("Main");
    }

    public void LoadGameOverScene ()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void LoadStartScene()
    {
        SceneManager.LoadScene("Start");
    }
}
