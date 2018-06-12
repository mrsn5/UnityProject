using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : Collectable
{
    void Start()
    {
        LevelController.current.incrementFruitNumber(); 
    }

    protected override void OnRabbitHit(HeroRabbit rabbit)
    {
        LevelController.current.addFruits(1);
        this.CollectedHide();
    }
}
