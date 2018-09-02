using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

    Rigidbody2D rb;
    float charge;
    float force;
    float dmg;
    bool launched = false;
    Transform spriteTransform;

	void Awake () {
        spriteTransform = transform.GetChild(0);
        rb = GetComponent<Rigidbody2D>();
	}

    private void OnEnable() {
        launched = false;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        HitEnemy(other);
    }

    private void OnTriggerStay2D(Collider2D other) {
        HitEnemy(other);
    }

    void HitEnemy(Collider2D other ) {
        if (other.tag == "Enemy" && launched) {
            dmg = charge;
            force = charge * 20f;
            Vector2 dir = rb.velocity.normalized;
            other.GetComponent<Enemy>().ArrowAttack(dir, force, dmg);
            gameObject.SetActive(false);
            CancelInvoke();
        }
    }

    public void Launch(Vector2 velocity, float chargeTime, float damage) {
        launched = true;
        charge = chargeTime;
        dmg = damage;
        rb.velocity = velocity;
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
