using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LosePopUp : MonoBehaviour {
    
    [SerializeField]
    AudioClip loseClip;
    AudioSource loseSourse;

    static public LosePopUp Instance = null;

    private void Awake()
    {
        Instance = this;
        loseSourse = gameObject.AddComponent<AudioSource>();
        loseSourse.clip = loseClip;
        loseSourse.playOnAwake = false;
        gameObject.SetActive(false);
	}
	
    public void Open()
    {
        gameObject.SetActive(true);
        if (SoundManager.Instance.isSoundOn()) loseSourse.Play();

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
