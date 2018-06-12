using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrot : MonoBehaviour {

    [SerializeField]
    private float speed = 3f;

    private Vector3 direction;

	void Start() {
        StartCoroutine (destroyLater ());
    }

    public void Launch(float direct) {
        direction = new Vector3(direct, 0, 0);
        transform.localScale = new Vector3((direct > 0) ? -1 : 1, transform.localScale.y, 1);
    }

    void Update()
    {
        transform.position += -direction * speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D collider) 
    {
        if (this.isActiveAndEnabled)
        {
            HeroRabbit rabbit = collider.GetComponent<HeroRabbit>();
            if (rabbit != null)
            {
                rabbit.Kill();
                Destroy(this.gameObject);
            }
        }
    }

    IEnumerator destroyLater() {
        yield return new WaitForSeconds (3.0f); 
        Destroy (this.gameObject);
    }
}
