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
    public Text coinsTextPopUp;
    public Text fruitsTextPopUp;

    public Image[] heartsImages;
    public Image[] gemsImages;
    public Image[] gemsImagesPopUp;

    public Sprite noHeart;
    public Sprite noGem;
    public Sprite heartSprite;
    public Sprite[] gemSprites;

    private LevelStats stats;
    private string levelName;

    void Awake()
    {
        SoundManager.Instance.setMusicOn(false);
        current = this;
        levelName = SceneManager.GetActiveScene().name;
        Load();
    }

    void Update()
    {
        coinsText.text = coins.ToString("D4");
        fruitsText.text = string.Format("{0}/{1}", fruits, numberOfFruits);
        coinsTextPopUp.text = coins.ToString("D4");
        fruitsTextPopUp.text = string.Format("{0}/{1}", fruits, numberOfFruits);
    }

    public void setStartPosition(Vector3 pos)
    {
        this.startingPosition = pos;
    }

    public void Respawn(HeroRabbit rabbit)
    {
        rabbit.transform.position = this.startingPosition;
        lives--;
        if (lives == 0)
        {
            rabbit.Kill();
            LosePopUp.Instance.Open();
        }
        if (lives >= 0) heartsImages[lives].sprite = noHeart;
    }

    public void Load()
    {
        this.stats = LevelStats.Load(levelName);
        if (this.stats == null) this.stats = new LevelStats();
    }

    public void Save()
    {
        if (stats.levelPassed)
        {
            if (gems == 3) stats.hasCrystals = true;
            if (fruits == numberOfFruits) stats.hasAllFruits = true;
            PlayerPrefs.SetString(levelName, JsonUtility.ToJson(this.stats));

            PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins", 0) + coins);
            PlayerPrefs.Save();
        }
    }

    public void SetLevelPassed()
    {
        stats.levelPassed = true;
    }

    public void addCoins(int n) 
    {
        coins += n;
    }

    public void addFruits(int id)
    {
        fruits++;
        if (!fruitIsPickedUp(id))
            stats.collectedFruits.Add(id);
    }

    public void addGems(int n)
    {
        gems++;
        gemsImages[n].sprite = gemSprites[n];
        gemsImagesPopUp[n].sprite = gemSprites[n];
    }

    public void incrementFruitNumber() 
    {
        numberOfFruits++;
    }

    public bool fruitIsPickedUp(int id)
    {
        return stats.collectedFruits.Contains(id);
    }
}
