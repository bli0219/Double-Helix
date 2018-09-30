using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    Vector3 mousePos = Vector3.zero;
    public Hero hero;
    bool RT = false;

	void Update () {
        //mousePos = Input.mousePosition;
        //mousePos.z = 0f;
        //Vector3 mousePosWorld = Camera.main.ScreenToWorldPoint(mousePos);
        //hero.RotateTowards(mousePosWorld);

        if (Input.GetAxis("bow") > 0f && !RT) {
            hero.StartCharge();
            RT = true;
        }
        if (Input.GetAxis("bow") == 0f && RT) {
            hero.EndCharge();
            RT = false;
        }

        if (Input.GetButtonUp("dash")) {
            hero.Dash();
        }

        if (Input.GetButtonDown("dodge")) {
            hero.Dodge();
        }

        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        hero.MoveToDirection(new Vector2(moveX, moveY).normalized);
        

        if (RT) {
            float aimX = Input.GetAxis("AimX");
            float aimY = Input.GetAxis("AimY");
            hero.RotateToDir(new Vector2(aimX, aimY));
            Debug.Log(aimX + " " + aimY);
        }






    }
}
