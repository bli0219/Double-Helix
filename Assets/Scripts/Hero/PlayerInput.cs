using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Control {
    Mouse, Xbox, PS
}

public class PlayerInput : MonoBehaviour {

    public static PlayerInput instance;
    public Control control = Control.Mouse;
	// Use this for initialization
	void Awake () {
        instance = this;
	}
	
    public bool MeleeDown() {
        return Input.GetButtonDown("melee");
    }

    public bool DashDown() {
        return Input.GetButtonDown("dash");
    }

    public bool DodgeDown() {
        return Input.GetButtonDown("dodge");
    }

    public bool InteractDown() {
        return Input.GetButtonDown("interact");
    }

    public bool ArrowDown() {
        return Input.GetButtonDown("bow");
    }

    public bool ArrowUp() {
        return Input.GetButtonUp("bow");
    }

    public Vector2 Direction1() {
        return new Vector2(
            Input.GetAxis("Horizontal"), 
            Input.GetAxis("Vertical"));
    }

    public Vector2 Direction2() {
        return new Vector2(
            Input.GetAxis("AimX"),
            Input.GetAxis("AimY"));
    }

}
