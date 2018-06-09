﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroRabbit : MonoBehaviour
{

    [SerializeField]
    private float speed = 3f;
    [SerializeField]
    public float maxJumpTime = 2f;
    [SerializeField]
    public float jumpSpeed = 3f;

    private bool isGrounded = false;
    private bool jumpActive = false;
    public bool isBig = true;
    private float jumpTime = 0f;

    private Rigidbody2D rb = null;
    private SpriteRenderer sr = null;
    private Animator animator = null;
    private Transform heroParent = null;

    private Vector3 defaultSize;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        LevelController.current.setStartPosition(rb.transform.position);
        rb.freezeRotation = true;
        heroParent = transform.parent;
        defaultSize = transform.lossyScale;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Moving
        float value = Input.GetAxis("Horizontal");
        if (Mathf.Abs(value) > 0)
        {
            rb.velocity = new Vector2(value * speed, rb.velocity.y);
            sr.flipX = (value > 0) ? false : true;
        }

        // Sizing
        SetSize((isBig) ? defaultSize : defaultSize * .5f);

        isGrounded = Grounded();

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            jumpActive = true;
        }

        if (jumpActive)
        {
            if (Input.GetButton("Jump"))
            {
                jumpTime += Time.deltaTime;
                if (jumpTime < maxJumpTime)
                {
                    rb.velocity = new Vector2(rb.velocity.x, jumpSpeed * (1.0f - jumpTime / maxJumpTime));
                }
            }
            else
            {
                jumpActive = false;
                jumpTime = 0;
            }
        }

        // Animation
        animator.SetBool("isRunning", Mathf.Abs(value) > 0);
        animator.SetBool("isJumping", !isGrounded);
    }

    private bool Grounded() 
    {
        Vector3 from = transform.position + Vector3.up * 0.3f;
        Vector3 to = transform.position + Vector3.down * 0.1f;
        int layer_id = 1 << LayerMask.NameToLayer("Ground");
        RaycastHit2D hit1 = Physics2D.Linecast(from - new Vector3(-.1f, 0f, 0f), to - new Vector3(-.1f, 0f, 0f), layer_id);
        RaycastHit2D hit2 = Physics2D.Linecast(from - new Vector3(.1f, 0f, 0f), to - new Vector3(.1f, 0f, 0f), layer_id);
        Debug.DrawLine(from, to - new Vector3(-.1f, 0f, 0f), Color.red);
        Debug.DrawLine(from, to - new Vector3(.1f, 0f, 0f), Color.red);

        if (hit1 || hit2)
        {
            if(hit1.transform != null && hit1.transform.GetComponent<MovingPlatform>() != null)
            { 
                SetNewParent(this.transform, hit1.transform);
            }
            if (hit2.transform != null && hit2.transform.GetComponent<MovingPlatform>() != null)
            {
                SetNewParent(this.transform, hit2.transform);
            }
        }
        else
        {
            SetNewParent(this.transform, this.heroParent);
        }


        return hit1 || hit2;
    }

    static void SetNewParent(Transform obj, Transform new_parent)
    {
        if (obj.transform.parent != new_parent)
        {
            Vector3 pos = obj.transform.position;
            obj.transform.parent = new_parent;
            obj.transform.position = pos;
        }
    }

    void SetSize(Vector3 size)
    {
        transform.localScale = Vector3.one;
        transform.localScale = new Vector3(size.x / transform.lossyScale.x, 
                                           size.y / transform.lossyScale.y, 
                                           size.z / transform.lossyScale.z);
    }

}
