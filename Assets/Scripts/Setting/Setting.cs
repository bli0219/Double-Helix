using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ControlScheme {
    Keyboard, Xbox, PS
}

public class Setting : MonoBehaviour {

    public ControlScheme scheme = ControlScheme.Keyboard;

    public int fps = 60;

    void Awake() {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = fps;    
    }

}
