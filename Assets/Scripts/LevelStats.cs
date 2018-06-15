using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelStats
{
    public bool hasCrystals = false;
    public bool hasAllFruits = false;
    public bool levelPassed = false;
    public List<int> collectedFruits = new List<int>();

    static public LevelStats Load(string key) {
        string str = PlayerPrefs.GetString(key, null);
        return JsonUtility.FromJson<LevelStats>(str);
    }
}
