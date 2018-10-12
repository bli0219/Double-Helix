using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    Vector3 mousePos = Vector3.zero;
    Vector3 faceDir = Vector3.zero;
    Vector3 moveDir = Vector3.zero;

    public PlayerInput playerInput;
    public Hero hero;
    bool charging = false;
    public bool usingMouse;

    void GetButtonInputs() {
        if (Input.GetButtonDown("bow") && !charging) {
            hero.StartCharge();
            charging = true;
        }
        if (Input.GetButtonUp("bow") && charging) {
            hero.EndCharge();
            charging = false;
        }
        if (Input.GetButtonDown("dash")) {
            hero.Dash();
        }
        if (Input.GetButtonDown("dodge")) {
            hero.Dodge();
        }
        if (Input.GetButtonDown("melee")) {
            hero.MeleeAttack();
        }
    }



    void GetAxisInputs() {

        if (charging) {
            // Don't move; face.
            hero.StandStill();

            if (usingMouse) {
                mousePos = Input.mousePosition;
                mousePos.z = 0f;
                Vector3 mousePosWorld = Camera.main.ScreenToWorldPoint(mousePos);
                faceDir = mousePosWorld - transform.position;
                hero.RotateToPoint(mousePosWorld);
            } else {
                float aimX = Input.GetAxis("AimX");
                float aimY = Input.GetAxis("AimY");
                faceDir = new Vector2(aimX, aimY);
                hero.RotateToDir(new Vector2(aimX, aimY));
            }
        } else {
            float moveX = Input.GetAxis("Horizontal");
            float moveY = Input.GetAxis("Vertical");
            Vector2 dir = new Vector2(moveX, moveY).normalized;
            hero.MoveToDirection(dir);
        }
    }

    void Update () {
        GetButtonInputs();
        GetAxisInputs();
    }
}
