using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour {

    int damage = 1;
    bool disable = false;


    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Enemy") {
            other.GetComponent<Enemy>().MeleeAttack(damage);
            if (disable) {
                // WHAT?
                //other.GetComponent<Enemy>().BeDisabled();
            }
        }    
    }
}
