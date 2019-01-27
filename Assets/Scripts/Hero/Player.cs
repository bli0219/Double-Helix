using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    Vector3 mousePos = Vector3.zero;
    Vector3 faceDir = Vector3.zero;
    Vector3 moveDir = Vector3.zero;
    public static Player instance;
    public PlayerInput input;
    public Hero hero;
    bool charging = false;
    public bool usingMouse;

    private void Awake() {
        instance = this;
    }
    void Start() {
        input = PlayerInput.instance;
    }

    void GetButtonInputs() {
        if (input.ArrowDown() && !charging) {
            hero.StartCharge();
            charging = true;
        }
        if (input.ArrowUp() && charging) {
            hero.EndCharge();
            charging = false;
        }
        if (input.DashDown()) {
            hero.Dash();
        }
        if (input.DodgeDown()) {
            hero.Dodge();
        }
        if (input.MeleeDown()) {
            hero.MeleeAttack();
        }
    }



    void GetAxisInputs() {

        // face that dir regardless
        if (usingMouse) {
            mousePos = Input.mousePosition;
            mousePos.z = 0f;
            Vector3 mousePosWorld = Camera.main.ScreenToWorldPoint(mousePos);
            faceDir = mousePosWorld - transform.position;
            hero.RotateToPoint(mousePosWorld);
        }

        if (charging) {
            hero.StandStill();
            if (!usingMouse) {
                faceDir = input.Direction2();
                hero.RotateToDir(faceDir);
            }
        } else {
            Vector2 dir = input.Direction1();
            hero.MoveToDirection(dir);
            if (!usingMouse) {
                hero.RotateToDir(dir);
            }
        }
    }

    void Update () {
        GetButtonInputs();
        GetAxisInputs();
    }
}
