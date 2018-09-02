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

    void CheckEnemy() {
        
    }
}
