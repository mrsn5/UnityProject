﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinPopUp : MonoBehaviour {

    [SerializeField]
    AudioClip winClip;
    AudioSource winSourse;

    static public WinPopUp Instance = null;

    private void Awake()
    {
        Instance = this;
        winSourse = gameObject.AddComponent<AudioSource>();
        winSourse.clip = winClip;
        winSourse.playOnAwake = false;
        gameObject.SetActive(false);
    }

    public void Open()
    {
        gameObject.SetActive(true);
        if (SoundManager.Instance.isSoundOn()) winSourse.Play();
        
    }

    public void Next()
    {
        SceneManager.LoadScene("Level_Picker");
    }

    public void Again()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
