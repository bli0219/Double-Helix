using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

    Rigidbody2D rb;

	void Awake () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	void Update () {
		
	}

    public void Launch() {
        Invoke("Drop", 1f);
        Invoke("Disappear", 2f);
    }

    public void Drop() {
        rb.velocity = Vector2.zero;
    }

    void Disappear() {
        gameObject.SetActive(false);
    }

}
