using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    float attackRange = 2f; 
    Hero target;
    public Group group = Group.hill;
    public float knockDuration = 0.5f;
    public float knockForce = 2f;
    public float moveSpeed = 1f;
    bool attacked = false;
    public bool alert = false;
    Rigidbody2D rb;
    GameObject alertAnim;
    EnemyManager manager;

    public float threat;
    public float attraction;

    /* 
     * alert: Yell() would be triggered with larger circle 
     * target: 
     */

    void Awake() {
        threat = Random.Range(-5f, 5f);
        attraction = Random.Range(-5f, 5f);
        rb = GetComponent<Rigidbody2D>();
        alertAnim = transform.GetChild(0).gameObject;
    }

    void Update() {

    }

    void Attack(Vector2 dir) {
        rb.velocity = dir * moveSpeed;
    }

    void ApproachTarget() {
        
    }

    void CheckAttack() {
        if (alert && Vector2.Distance(target.transform.position, transform.position) < attackRange) {
            Vector2 dir = target.transform.position - transform.position;
            Attack(dir);
        }
    }

    public void ArrowAttack(Vector2 knockDir, float force, float dmg) {
        // substract damage
        rb.velocity = knockDir * force;
        Debug.Log(rb.velocity);
        Invoke("KnockStop", 0.05f);
        attacked = true;
        if (!alert) {
            Invoke("Yell", knockDuration);
        }
        alert = true;
    }

    void KnockStop() {
        rb.velocity = Vector2.zero;
    }

    void Warn() {
        //anim
        Debug.Log(manager);
        manager.WarnNearby(this);
    }

    public void SeeHero(Hero hero) {
        target = hero;
        TurnAlert();
    }

    float DistanceTo(Hero hero) {
        return (hero.transform.position - transform.position).magnitude;
    }

    Vector2 DirectionTo(Hero hero) {
        Vector3 dir = hero.transform.position - transform.position;
        return new Vector2(dir.x, dir.y).normalized;
    }


    public void TurnAlert() {
        alertAnim.SetActive(true);
        Invoke("TurnOffAlertAnim", 1f);
        alert = false;
    }

    void TurnOffAlertAnim() {
        alertAnim.SetActive(false);
    }
}
