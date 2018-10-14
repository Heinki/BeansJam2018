using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public AudioSource fxSource;
    public AudioSource musicSource;

    public static SoundManager instance = null;

    public List<AudioClip> sfxList_Aua;
    public List<AudioClip> sfxList_Kotze;
    public List<AudioClip> sfxList_Welcome;

    public AudioClip sfx_Clean;
    public AudioClip sfx_Crying;
    public AudioClip sfx_Punch;
    public AudioClip sfx_Quietschen;
    public AudioClip sfx_Shake;
    public AudioClip sfx_Success;
    public AudioClip sfx_Swipe;
    public AudioClip sfx_Tap;
    public AudioClip sfx_WasserPlatsch;
    public AudioClip sfx_Bienen;

    public float lowPitchRange = .95f;
    public float highPitchRange = 1.05f;

    // Use this for initialization

    void Awake () {
		if(instance == null)
        {
            instance = this;
        } else if(instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
	}
	
    public void PlaySingle (AudioClip clip)
    {
        fxSource.clip = clip;
        fxSource.Play();
    }

    public void Mute()
    {
        fxSource.volume = 0;
        musicSource.volume = 0;
    }

    public void UnMute()
    {
        fxSource.volume = 1;
        musicSource.volume = 1;
    }


    public void RandomizeSfx (params AudioClip[] clips)
    {
        int randomIndex = Random.Range(0, clips.Length);
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);

        fxSource.pitch = randomPitch;
        fxSource.clip = clips[randomIndex];
        fxSource.Play();
    }

    public void PlayRandomSFX_AUA()
    {
        int randomIndex = Random.Range(0, sfxList_Aua.Count);
        fxSource.clip = sfxList_Aua[randomIndex];
        PlayChosenSFX();
    }

    public void PlayRandomSFX_KOTZE()
    {
        int randomIndex = Random.Range(0, sfxList_Kotze.Count);
        fxSource.clip = sfxList_Kotze[randomIndex];
        PlayChosenSFX();
    }

    public void PlayRandomSFX_WELCOME()
    {
        int randomIndex = Random.Range(0, sfxList_Welcome.Count);
        fxSource.clip = sfxList_Welcome[randomIndex];
        PlayChosenSFX();
    }

    private void PlayChosenSFX()
    {
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);
        fxSource.pitch = randomPitch;
        fxSource.Play();
    }

    //PLAYSPECIFICSOND

    public void PlaySFX_WASSERPLATSCH()
    {
        fxSource.clip = sfx_WasserPlatsch;
        fxSource.Play();
    }

    public void PlaySFX_BIENEN()
    {
        fxSource.clip = sfx_Bienen;
        fxSource.Play();
    }

    public void PlaySFX_TAP()
    {
        fxSource.clip = sfx_Tap;
        fxSource.Play();
    }

    public void PlaySFX_SWIPE()
    {
        fxSource.clip = sfx_Swipe;
        fxSource.Play();
    }

    public void PlaySFX_SUCCESS()
    {
        fxSource.clip = sfx_Success;
        fxSource.Play();
    }

    public void PlaySFX_SHAKE()
    {
        fxSource.clip = sfx_Shake;
        fxSource.Play();
    }

    public void PlaySFX_CLEAN()
    {
        fxSource.clip = sfx_Clean;
        fxSource.Play();
    }

    public void PlaySFX_CRYING()
    {
        fxSource.clip = sfx_Crying;
        fxSource.Play();
    }

    public void PlaySFX_PUNCH()
    {
        fxSource.clip = sfx_Punch;
        fxSource.Play();
    }

    public void PlaySFX_QUIETSCHEN()
    {
        fxSource.clip = sfx_Quietschen;
        fxSource.Play();
    }




}
