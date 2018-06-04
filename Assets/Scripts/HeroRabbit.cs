using System.Collections;
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

    private Rigidbody2D rb = null;
    private SpriteRenderer sr = null;
    private Animator animator = null;

    private bool isGrounded = false;
    private bool jumpActive = false;
    private float jumpTime = 0f;


    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        LevelController.current.setStartPosition(rb.transform.position);
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float value = Input.GetAxis("Horizontal");
        if (Mathf.Abs(value) > 0)
        {
            rb.velocity = new Vector2(value * speed, rb.velocity.y);
            sr.flipX = (value > 0) ? false : true;
        }
       
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
        RaycastHit2D hit1 = Physics2D.Linecast(from, to - new Vector3(-.1f, 0f, 0f), layer_id);
        RaycastHit2D hit2 = Physics2D.Linecast(from, to - new Vector3(.1f, 0f, 0f), layer_id);
        Debug.DrawLine(from, to - new Vector3(-.1f, 0f, 0f), Color.red);
        Debug.DrawLine(from, to - new Vector3(.1f, 0f, 0f), Color.red);
        return hit1 || hit2;
    }

}
