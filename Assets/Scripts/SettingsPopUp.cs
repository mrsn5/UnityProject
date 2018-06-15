using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPopUp : MonoBehaviour {

    public static SettingsPopUp current;

    [SerializeField]
    Sprite musicOn, soundOn, musicOff, soundOff;
    [SerializeField]
    Image musicButt, soundButt;

    bool isMusicOn = true, isSoundOn = true;

    private void Awake()
    {
        Close();
    }

    void Start()
    {
        current = this;
        isSoundOn = SoundManager.Instance.isSoundOn();
        isMusicOn = SoundManager.Instance.isMusicOn();
        musicButt.sprite = (isMusicOn) ? musicOn : musicOff;
        soundButt.sprite = (isSoundOn) ? soundOn : soundOff;
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void MusicChange()
    {
        isMusicOn = !isMusicOn;
        SoundManager.Instance.setMusicOn(isMusicOn);
        musicButt.sprite = (isMusicOn) ? musicOn : musicOff;
    }

    public void SoundChange()
    {
        isSoundOn = !isSoundOn;
        SoundManager.Instance.setSoundOn(isSoundOn);
        soundButt.sprite = (isSoundOn) ? soundOn : soundOff;
    }

}
