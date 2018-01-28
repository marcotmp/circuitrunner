using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

    public AudioClip titleClip;
    public AudioClip gameplayClip;
    public AudioClip introClip;
    public AudioClip deadClip;
    public AudioClip flipEffect;
    public AudioClip boltEffect;

    public AudioSource audioSource;

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(gameObject);
	}

    public void PlayTitleClip()
    {
        audioSource.Stop();
        audioSource.clip = gameplayClip;
        audioSource.Play();
    }
	
    public void PlayGameplayClip()
    {
        audioSource.Stop();
        audioSource.clip = gameplayClip;
        audioSource.Play();
    }

    public void PlayFlipEffect()
    {
        audioSource.PlayOneShot(flipEffect);
    }

    public void PlayFireEffect()
    {
        audioSource.PlayOneShot(boltEffect);
    }

    public void PlayDeadEffect()
    {
        audioSource.PlayOneShot(deadClip);
    }

    public void PlayIntroEffect()
    {
        audioSource.PlayOneShot(introClip);
    }
}
