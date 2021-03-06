﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    #region objects
    Rigidbody2D rb;
    GameObject alertAnim;
    EnemyManager manager;
    public Hero target;
    GameObject hitbox;
    #endregion

    #region parameters
    float attackRange = 2f;
    public Level level = Level.hill;
    public float knockDuration = 0.5f;
    public float knockForce = 2f;
    public float moveSpeed = 1f;
    bool attacked = false;
    public bool alert = false;


    #endregion

    #region stats
    public float threat = 5f;
    public float attraction = 5f;
    public int health = 5;
    #endregion

    void Awake() {
        //threat = Random.Range(-5f, 5f);
        //attraction = Random.Range(-5f, 5f);
        rb = GetComponent<Rigidbody2D>();
        alertAnim = transform.GetChild(0).gameObject;
        hitbox = transform.GetChild(1).gameObject;
    }

    void Update() {
        TryAttack(); 
    }

    public Vector2 PositionV2() {
        return new Vector2(transform.position.x, transform.position.y);
    }

    #region Attack
    void Attack(Vector2 dir) {
        //rb.velocity = dir * moveSpeed;
        hitbox.SetActive(true);
    }

    void TryAttack() {
        if (Vector2.Distance(target.transform.position, transform.position) < attackRange) {
            Vector2 dir = target.transform.position - transform.position;
            transform.right = dir;
            Attack(dir);
        }
    }
    #endregion

    #region BeingAttacked

    public void MeleeAttack(int damage) {
        TakeDamage(damage);
    }

    void TakeDamage(int damage) {
        health -= damage;
        if (health <= 0) {
            Die();
        }
    }

    public void ArrowAttack(Vector2 from, float force, int damage) {
        rb.velocity = from * force;
        Invoke("KnockStop", 0.05f);
        if (!alert) {
            alert = true;
            Invoke("Yell", knockDuration);
        }
        TakeDamage(damage);
    }

    void KnockStop() {
        rb.velocity = Vector2.zero;
    }

    void Die() {
        // Add effects
        gameObject.SetActive(false);
    }
    #endregion

    #region HeroDetection
    //void Warn() {
    //    //anim
    //    Debug.Log(manager);
    //    manager.WarnNearby(this);
    //}

    public void SeeHero(Hero hero) {
        if (target==null) {
            target = hero;
            TurnAlert();
        }
    }

    public void TurnAlert() {
        
        alertAnim.SetActive(true);
        Invoke("TurnOffAlertAnim", 1f);
        alert = false;
    }

    void TurnOffAlertAnim() {
        alertAnim.SetActive(false);
    }
    #endregion

    #region Utility
    float DistanceTo(Hero hero) {
        return (hero.transform.position - transform.position).magnitude;
    }

    Vector2 DirectionTo(Hero hero) {
        Vector3 dir = hero.transform.position - transform.position;
        return new Vector2(dir.x, dir.y).normalized;
    }

    #endregion
}
