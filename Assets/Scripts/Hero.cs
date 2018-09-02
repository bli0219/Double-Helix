using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour {

    Rigidbody2D rb;

	void Awake () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	void Update () {
        
    }

    void EnemyDetection() {
        //normal 
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 10f);
        foreach (Collider2D col in colliders) {
            if (col.tag == "Enemy") {
                Enemy e = col.GetComponent<Enemy>();
                if (e.alert) {
                    e.SeeHero(this);
                } else if (Vector3.Distance(e.transform.position, transform.position) < 5f) {
                    e.SeeHero(this);
                }
            }
        }
    }
}
