using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrownOrc : Orc {

    [SerializeField]
    private float attackInterval;
    private float attackTime = 0;

    [SerializeField]
    private GameObject prefabCarrot;

    protected override void Attack()
    {
        animator.SetBool("isWalking", false);
        float value = this.getDirection(HeroRabbit.lastRabbit.transform.position);
        sr.flipX = (value > 0) ? false : true;

        attackTime -= Time.deltaTime;
        if (attackTime <= 0) 
        {
            animator.SetTrigger("isAttacking");
            launchCarrot(value);
            attackTime = attackInterval;
        }
    }

    void launchCarrot(float direction)
    {
        GameObject obj = Instantiate(this.prefabCarrot);
        obj.transform.position = this.transform.position + new Vector3(0, 1, 0);
        Carrot carrot = obj.GetComponent<Carrot>();
        carrot.Launch(direction);
    }
}
