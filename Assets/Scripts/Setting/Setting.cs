using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ControlScheme {
    Keyboard, Xbox, PS
}

public class Setting : MonoBehaviour {

    public ControlScheme scheme = ControlScheme.Keyboard;

}
