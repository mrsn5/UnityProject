using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : Collectable
{
    [SerializeField]
    private int id = 0;

    void Start()
    {
        LevelController.current.incrementFruitNumber();
        if (LevelController.current.fruitIsPickedUp(id)) 
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, .5f);
    }

    protected override void OnRabbitHit(HeroRabbit rabbit)
    {
        LevelController.current.addFruits(id);
        this.CollectedHide();
    }

}
