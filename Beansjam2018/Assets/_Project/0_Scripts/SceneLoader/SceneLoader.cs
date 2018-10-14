using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

	public void LoadMainScene ()
    {
        GameObject manager = GameObject.Find("GameManager");
        if (manager != null)
        {
            GameManager.ResetHighScore();
            Destroy(manager);
        }
        SceneManager.LoadScene("Main");
    }

    public void LoadGameOverScene ()
    {
        SoundManager.instance.PlayRandomSFX_GAMEOVER();
        SceneManager.LoadScene("GameOver");
    }

    public void LoadStartScene()
    {
        GameObject manager = GameObject.Find("GameManager");
        if (manager != null)
        {
            GameManager.ResetHighScore();
            Destroy(manager);
        }
        SoundManager.instance.PlayRandomSFX_WELCOME();

        SceneManager.LoadScene("Start");
    }
}
