using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour {

    List<Source> sources;
    Rigidbody2D rb;

	void Awake () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	void Update () {
		
	}

    /* 
     * 
     * 
     * 
     */
    
    void UpdateForce() {
        foreach (Source s in sources) {
            Vector3 dir = s.transform.position - transform.position;
            //rb.AddForce(s.absPotential * new Vector2(dir.x, dir.y));
        }
    }
}
