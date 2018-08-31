using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    
    public Map map = Map.someMap;
    public float knockDuration = 0.5f;
    bool alert = false;
    EnemyManager manager;

    void Awake() {
        manager = EnemyManager.Instance;
    }

    void Update() {
        if (alert) {
            SearchPlayer();
        }
    }

    void SearchPlayer() {
        
    }

    void Attacked(Vector2 from) {
        Knockback(from);
        Invoke("Yell", knockDuration);
        alert = true;
    }

    void Knockback(Vector2 from) {
        //TODO
    }

    void Yell() {
        //anim
        manager.AlertNearby(this);
    }

    public void TurnAlert() {
        //anim
        alert = false;
    }
}
