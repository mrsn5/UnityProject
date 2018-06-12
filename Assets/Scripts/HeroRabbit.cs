using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroRabbit : MonoBehaviour
{
    public static HeroRabbit lastRabbit = null;

    [SerializeField]
    private float speed = 3f;
    [SerializeField]
    public float maxJumpTime = 2f;
    [SerializeField]
    public float jumpSpeed = 3f;
    [SerializeField]
    public float maxInvincibleTime = 4f;

    private bool isGrounded = false;
    private bool jumpActive = false;
    public bool isBig = true;
    public bool isDead = false;
    public bool isInvinc = false;

    private float invincibleTime = 0f;
    private float jumpTime = 0f;

    private Rigidbody2D rb = null;
    private SpriteRenderer sr = null;
    private Animator animator = null;
    private Transform heroParent = null;

    private Vector3 defaultSize;

    void Awake()
    {
        lastRabbit = this;
    }

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

    void FixedUpdate()
    {
        // Moving
        float value = Input.GetAxis("Horizontal");
        if (Mathf.Abs(value) > 0 && !isDead)
        {
            rb.velocity = new Vector2(value * speed, rb.velocity.y);
            sr.flipX = (value > 0) ? false : true;
        }

        // Sizing
        SetSize((isBig) ? defaultSize : defaultSize * .7f);

        // Jumping
        isGrounded = Grounded();
        if (Input.GetButtonDown("Jump") && isGrounded && !isDead)
        {
            jumpActive = true;
        }

        if (jumpActive)
        {
            if (Input.GetButton("Jump")) {
                jumpTime += Time.deltaTime;
                if (jumpTime < maxJumpTime)
                {
                    rb.velocity = new Vector2(rb.velocity.x, jumpSpeed * (1.0f - jumpTime / maxJumpTime));
                }
            } else {
                jumpActive = false;
                jumpTime = 0;
            }
        }

        // Invincible
        if (isInvinc) {
            invincibleTime -= Time.deltaTime;
            GetComponent<SpriteRenderer>().material.color =
                new Color(1, 1 - invincibleTime % 1 / 2, 1 - invincibleTime % 1 / 2);
            if (invincibleTime < 0)
            {
                isInvinc = false;
                GetComponent<SpriteRenderer>().material.color =
                    new Color(1, 1, 1);
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
        Debug.DrawLine(from - new Vector3(-.1f, 0f, 0f), to - new Vector3(-.1f, 0f, 0f), Color.red);
        Debug.DrawLine(from - new Vector3(.1f, 0f, 0f), to - new Vector3(.1f, 0f, 0f), Color.red);

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

    public void UpSize()
    {
        if (!isBig) isBig = true;
    }

    public void DownSize()
    {
        if (isBig) {
            isBig = false;
            invincibleTime = maxInvincibleTime;
            isInvinc = true;
        } else Kill();
    }

    private IEnumerator KillCoroutine()
    { 
        isDead = true;
        animator.SetBool("isDead", true);
        yield return new WaitForSeconds(1f);
        isBig = true;
        isDead = false;
        animator.SetBool("isDead", false);
        LevelController.current.Respawn(this);

    }

    public void Kill()
    {
        StartCoroutine(KillCoroutine());
    }

    public bool isInvincible()
    {
        return isInvinc;
    }

    public bool IsDead() 
    {
        return isDead;
    }

    public void Jump() 
    {
        StartCoroutine(JumpCoroutine());
    }

    private IEnumerator JumpCoroutine()
    {
        jumpTime = 0;
        while (true)
        {
            jumpTime += Time.deltaTime;
            if (jumpTime < 1f)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpSpeed * (1.0f - jumpTime / 1f));
                yield return null;
            } else {
                jumpActive = false;
                jumpTime = 0;
                break;
            }
        }
    }


}
