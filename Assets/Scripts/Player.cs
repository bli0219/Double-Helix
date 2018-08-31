using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    float bowChargeLimit = 1f;
    float bowCharge = 0f;
    List<Arrow> arrows;
    Rigidbody2D rb;
    public float speed = 2f;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();    
    }
    void Start () {
		
	}
	
	void Update () {
        //if (Input.GetButtonDown("bow")) {
        //    bowCharge += Time.smoothDeltaTime;
        //}
        //if (Input.GetButtonUp("bow")) {
        //    float charge = Mathf.Min(bowCharge, bowChargeLimit);
        //    float range = charge;
        //    float dmg = 2f * charge;
        //}

        float hor = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");
        rb.velocity = new Vector2(hor, vert) * speed;

	}
}
