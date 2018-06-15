using UnityEngine;

public class SoundManager
{
    private bool is_sound_on = true;

    public bool isSoundOn()
    {
        return is_sound_on;
    }

    public void setSoundOn(bool val)
    {
        this.is_sound_on = val;
        PlayerPrefs.SetInt("sound", is_sound_on ? 1 : 0); 
        PlayerPrefs.Save();
    }

    SoundManager()
    {
        is_sound_on = PlayerPrefs.GetInt("sound", 1) == 1;
    }

    public static SoundManager Instance = new SoundManager();
}
