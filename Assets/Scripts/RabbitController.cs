using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitController : MonoBehaviour {
    
	[SerializeField]
	private float speed = 5;
	private Rigidbody2D rb = null;
	private SpriteRenderer sr = null;

	// Use this for initialization
	void Start () {
		rb = this.GetComponent<Rigidbody2D> ();
		sr = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float value = Input.GetAxis ("Horizontal");
		if (Mathf.Abs (value) > 0) {

			Vector2 vel = rb.velocity;
			vel.x = value * speed;
			rb.velocity = vel;

			if(value < 0) {
				sr.flipX = true;
			} else if(value > 0) {
				sr.flipX = false;
			}
		}
	}
}
