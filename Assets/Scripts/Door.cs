using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour {

    [SerializeField]
    SpriteRenderer crystal;
    [SerializeField]
    SpriteRenderer fruit;
    [SerializeField]
    SpriteRenderer check;
    [SerializeField]
    SpriteRenderer doorLock;

    [SerializeField]
    Sprite allCrystals;
    [SerializeField]
    Sprite allFruits;

    [SerializeField]
    string levelName = "";
    [SerializeField]
    string prevLevel = "";
    [SerializeField]
    private bool isOpen = false;

    private void Awake()
    {
        LevelStats stats = LevelStats.Load(levelName);
        if (stats == null) stats = new LevelStats();

        if (stats.hasCrystals)
        {
            crystal.sprite = allCrystals;
            crystal.GetComponent<Transform>().localScale = new Vector3(.7f, .7f, 1f);
        }
        if (stats.hasAllFruits)
        {
            fruit.sprite = allFruits;
            fruit.GetComponent<Transform>().localScale = new Vector3(.8f, .8f, 1f);
        }
        if (LevelStats.Load(prevLevel) != null)
            isOpen = LevelStats.Load(prevLevel).levelPassed;
        doorLock.enabled = !isOpen;
        check.enabled = stats.levelPassed;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (this.isActiveAndEnabled && isOpen)
        {
            HeroRabbit rabbit = collider.GetComponent<HeroRabbit>();
            if (rabbit != null)
            {
                SceneManager.LoadScene(levelName);
            }
        }
    }
}
