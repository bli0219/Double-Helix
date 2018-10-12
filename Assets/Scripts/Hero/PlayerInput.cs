using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Control {
    Mouse, Xbox, PS
}

public class PlayerInput : MonoBehaviour {


    public Control control = Control.Mouse;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool ButtonDownMelee() {
        return Input.GetButtonDown("melee");
    }

    public bool ButtonDownDash() {
        return Input.GetButtonDown("dash");
    }

    public bool ButtonDownDodge() {
        return Input.GetButtonDown("dodge");
    }

    public bool ButtonDownInteract() {
        return Input.GetButtonDown("interact");
    }

    public bool ArrowCharge() {
        return Input.GetButtonDown("bow");
    }

    public bool ArrowUncharge() {
        return Input.GetButtonUp("bow");
    }

}
