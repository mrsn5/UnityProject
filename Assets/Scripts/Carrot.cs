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
        GetComponent<SpriteRenderer>().flipX = (direct > 0) ? false : true;
    }

    void Update()
    {
        if (direction != null) 
        {
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    IEnumerator destroyLater() {
        yield return new WaitForSeconds (3.0f); 
        Destroy (this.gameObject);
    }
}
