using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour {

    [SerializeField]
    private AudioClip music = null;
    private AudioSource musicSource = null;

	void Start () {
        musicSource = gameObject.AddComponent<AudioSource>(); musicSource.clip = music;
        musicSource.loop = true;
        musicSource.Play();
	}

    private void Update()
    {
        if (!musicSource.isPlaying && SoundManager.Instance.isMusicOn()) musicSource.Play();
        if (!SoundManager.Instance.isMusicOn()) musicSource.Stop();
    }
}
