using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    Hero target;
    public Map map = Map.someMap;
    public float knockDuration = 0.5f;
    public float knockMult = 2f;
    public float moveSpeed = 1f;
    bool alert = false;
    Rigidbody2D rb;
    EnemyManager manager;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        manager = EnemyManager.Instance;
    }

    void Update() {
        if (alert) {
            AttackMode();
        }
    }

    void AttackMode() {
        Vector3 dir = target.transform.position - transform.position;
        rb.velocity = new Vector2(dir.x, dir.y).normalized * moveSpeed;
    }

    void Attacked(Vector2 from) {
        Knockback(from);
        Invoke("Yell", knockDuration);
        alert = true;
    }

    void Knockback(Vector2 from) {
        rb.AddForce(-from.normalized * knockMult);
    }

    void Yell() {
        //anim
        manager.AlertNearby(this);
    }

    public void SeeHero(Hero hero) {
        target = hero;
        TurnAlert();
    }

    public void TurnAlert() {
        //anim
        alert = false;
    }
}
