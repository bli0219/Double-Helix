using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

    Rigidbody2D rb;
    float chargeTime;
    float force;
    int damage;
    bool launched = false;
    Transform spriteTransform;
    bool firstHit = true;

	void Awake () {
        spriteTransform = transform.GetChild(0);
        rb = GetComponent<Rigidbody2D>();
	}

    private void OnEnable() {
        launched = false;
        firstHit = true;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (firstHit) {
            HitEnemy(other);
            firstHit = false;
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if (firstHit) {
            HitEnemy(other);
            firstHit = false;
        }
    }

    void HitEnemy(Collider2D other ) {
        if (other.tag == "Enemy" && launched) {
            force = chargeTime * 20f;
            Vector2 dir = rb.velocity.normalized;
            other.GetComponent<Enemy>().ArrowAttack(dir, force, damage);
            CancelInvoke("Drop");
            gameObject.SetActive(false);

        }
    }

    public void Launch(Vector2 _velocity, float _chargeTime, int _damage) {
        launched = true;
        chargeTime = _chargeTime;
        damage = _damage;
        rb.velocity = _velocity;
        Invoke("Drop", 1f);
    }

    public void Drop() {
        rb.velocity = Vector2.zero;
        firstHit = false;
        Invoke("Disappear", 1f);
    }

    void Disappear() {
        gameObject.SetActive(false);
    }

}
