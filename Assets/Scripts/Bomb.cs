using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Collectable
{
    protected override void OnRabbitHit(HeroRabbit rabbit)
    {
        Debug.Log(this.name);
        if (!rabbit.isInvincible())
        {
            rabbit.DownSize();
            this.CollectedHide();
        }
    }
}
