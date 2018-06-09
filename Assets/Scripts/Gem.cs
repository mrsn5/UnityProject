using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : Collectable
{
    protected override void OnRabbitHit(HeroRabbit rabbit)
    {
        LevelController.current.addGems(1);
        this.CollectedHide();
    }
}
