using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenOrc : Orc {

    [SerializeField]
    private float runSpeed = 1.5f;

    protected override void Attack()
    {
        animator.SetBool("isRunning", true);
        float value = this.getDirection(HeroRabbit.lastRabbit.transform.position);
        sr.flipX = (value > 0) ? false : true;
        transform.position += new Vector3(-value, .0f, .0f) * runSpeed * Time.deltaTime;
    }

}
