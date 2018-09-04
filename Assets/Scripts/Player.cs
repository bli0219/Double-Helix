using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    Vector3 mousePos = Vector3.zero;
    public Hero hero;

	void Update () {
        mousePos = Input.mousePosition;
        mousePos.z = 0f;
        Vector3 mousePosWorld = Camera.main.ScreenToWorldPoint(mousePos);
        hero.RotateTowards(mousePosWorld);

        if (Input.GetButtonDown("bow")) {
            hero.StartCharge();
        }
        if (Input.GetButtonUp("bow")) {
            hero.EndCharge();
        }

        if (Input.GetButtonUp("dash")) {
            hero.Dash();
        }

        if (Input.GetButtonDown("dodge")) {
            hero.Dodge();
        }

        float hor = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");
        hero.MoveTowards(new Vector2(hor, vert).normalized);
	}
}
