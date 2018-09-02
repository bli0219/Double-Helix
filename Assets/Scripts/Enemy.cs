using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    Hero target;
    public Map map = Map.someMap;
    public float knockDuration = 0.5f;
    public float knockForce = 2f;
    public float moveSpeed = 1f;
    bool attacked = false;
    public bool alert = false;
    Rigidbody2D rb;
    GameObject alertAnim;
    EnemyManager manager;


    /* 
     * alert: Yell() would be triggered with larger circle 
     * target: 
     */

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        alertAnim = transform.GetChild(0).gameObject;
    }

    private void Start() {
        manager = EnemyManager.Instance;

        Debug.Log(manager);

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
    
    //private void OnTriggerEnter2D(Collider2D other) {
    //    if (other.tag == "Arrow") {
    //        Vector3 knockDir = transform.position - other.transform.position;
    //        ArrowAttack(new Vector2(knockDir.x, knockDir.y).normalized);
    //    }
    //}

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

    public void TurnAlert() {
        alertAnim.SetActive(true);
        Invoke("TurnOffAlertAnim", 1f);
        alert = false;
    }

    void TurnOffAlertAnim() {
        alertAnim.SetActive(false);
    }
}
