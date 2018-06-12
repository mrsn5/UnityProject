using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour {

    public static LevelController current;
    Vector3 startingPosition;

    public int coins = 0;
    public int numberOfFruits = 0;
    public int fruits = 0;
    public int gems = 0;

    public Text coinsText;
    public Text fruitsText;

    void Awake()
    {
        current = this;
    }

    void Update()
    {
        coinsText.text = coins.ToString("D4");
        fruitsText.text = string.Format("{0}/{1}", fruits, numberOfFruits);
    }

    public void setStartPosition(Vector3 pos)
    {
        this.startingPosition = pos;
    }

    public void Respawn(HeroRabbit rabbit)
    {
        rabbit.transform.position = this.startingPosition;
    }

    public void addCoins(int n) 
    {
        coins += n;
    }

    public void addFruits(int n)
    {
        fruits += n;
    }

    public void addGems(int n)
    {
        gems += n;
    }

    public void incrementFruitNumber() 
    {
        numberOfFruits++;
    }
}
