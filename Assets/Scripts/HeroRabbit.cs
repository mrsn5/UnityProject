using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroRabbit : MonoBehaviour
{

    [SerializeField]
    private float speed = 3;
    private Rigidbody2D rb = null;
    private SpriteRenderer sr = null;

    // Use this for initialization
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float value = Input.GetAxis("Horizontal");
        if (value != 0)
        {
            rb.velocity = new Vector2(value * speed, rb.velocity.y);
            sr.flipX = (value > 0) ? false : true;
        }
    }
}
