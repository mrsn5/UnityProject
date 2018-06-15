using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : Collectable
{
    enum Color { BLUE, GREEN, RED };
    [SerializeField]
    private Color color = Color.BLUE;

    protected override void OnRabbitHit(HeroRabbit rabbit)
    {
        LevelController.current.addGems((int)color);
        this.CollectedHide();
    }

}
