using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour {

    public static LevelController current;
    Vector3 startingPosition;

    public int coins = 0;
    public int numberOfFruits = 0;
    public int fruits = 0;
    public int gems = 0;
    public int lives = 3;

    public Text coinsText;
    public Text fruitsText;
    public Image[] heartsImages;
    public Image[] gemsImages;

    public Sprite noHeart;
    public Sprite noGem;
    public Sprite heartSprite;
    public Sprite[] gemSprites;

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
        lives--;
        if (lives==0) SceneManager.LoadScene("Level_Picker");
        heartsImages[lives].sprite = noHeart;

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
        gems++;
        gemsImages[n].sprite = gemSprites[n];
    }

    public void incrementFruitNumber() 
    {
        numberOfFruits++;
    }
}
