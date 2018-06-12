using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc : MonoBehaviour {

    public enum Mode { GoToA, GoToB, Attack, Dead }

    public Vector3 MoveBy;
    Vector3 pointA;
    Vector3 pointB;

    [SerializeField]
    private float speed = 1f;

    private Mode mode = Mode.GoToB;
    private Mode prevMode = Mode.GoToB;

    protected SpriteRenderer sr = null;
    protected Animator animator = null;

	// Use this for initialization
	public void Start () {
        this.pointA = this.transform.position;
        this.pointB = this.pointA + MoveBy;

        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        GetComponent<Rigidbody2D>().freezeRotation = true;
	}

    public void Update()
    {
        Debug.DrawLine(transform.position - new Vector3(.5f, 0, 0), transform.position + new Vector3(.5f, 0, 0));
        Debug.DrawLine(transform.position - new Vector3(0, .5f, 0), transform.position + new Vector3(0, .5f, 0));

        if (mode != Mode.Dead)
        {
            mode = (rabbitHere()) ? Mode.Attack : prevMode;

            // Moving
            animator.SetBool("isWalking", true);
            if (mode == Mode.Attack)
            {
                Attack();
            }
            else
            {
                Move();
            }
        }
    }

    private void Move() 
    {
        animator.SetBool("isRunning", false);
        if (isArrived(transform.position, (mode == Mode.GoToA) ? pointA : pointB))
        {
            mode = (mode == Mode.GoToA) ? Mode.GoToB : Mode.GoToA;
            prevMode = mode;
        }
        float value = this.getDirection((mode == Mode.GoToA) ? pointA : pointB);
        sr.flipX = (value > 0) ? false : true;
        transform.position += new Vector3(-value, .0f, .0f) * speed * Time.deltaTime;
    }

    protected virtual void Attack() {}

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (this.isActiveAndEnabled)
        {
            HeroRabbit rabbit = collision.gameObject.GetComponent<HeroRabbit>();
            if (rabbit != null)
            {
                this.OnRabbitHit(rabbit);
            }
        }
    }

    void OnRabbitHit(HeroRabbit rabbit) 
    {
        Vector3 v = rabbit.transform.position - transform.position;
        float angle = Mathf.Atan2(v.y, v.x) / Mathf.PI * 180;
        if (angle > 60f && angle < 150f) 
        {
            rabbit.Jump();
            Kill();
        } else {
            animator.SetTrigger("isAttacking");
            rabbit.Kill();
        }
    }


    public float getDirection(Vector3 obj)
    {
        float value = (transform.position - obj).x;
        if (Mathf.Abs(value) < 0.02f)
            return 0; 
        else if ((transform.position - obj).x > 0)
            return 1; 
        else return -1; 

        //Vector3 dir = (transform.position - obj);
        //dir.Normalize();
        //return dir.x;
    }

    bool isArrived(Vector3 pos, Vector3 target)
    {
        return Mathf.Abs(pos.x - target.x) < 0.02f;   
    }

    bool rabbitHere() {
        Vector3 rabbit_pos = HeroRabbit.lastRabbit.transform.position;
        if (System.Math.Abs(Mathf.Abs(rabbit_pos.x - pointA.x) 
                            + Mathf.Abs(rabbit_pos.x - pointB.x) 
                            - Mathf.Abs(pointA.x - pointB.x)) < 0.1f) return true;
        return false;
    }

    private IEnumerator KillCoroutine()
    {
        mode = Mode.Dead;
        animator.SetTrigger("isDead");
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }

    public void Kill()
    {
        StartCoroutine(KillCoroutine());
    }


}
