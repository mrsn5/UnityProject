using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {

    public static LevelController current;
    Vector3 startingPosition;

    public int coins = 0;
    public int fruits = 0;
    public int gems = 0;

    void Awake()
    {
        current = this;
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
}
