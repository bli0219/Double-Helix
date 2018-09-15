using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Source : MonoBehaviour {

    public float threat;
    public float priority;

    void Awake() {
        threat = Random.Range(-5f, 5f);
        priority = Random.Range(-5f, 5f);
    }

    void Start() {

    }

}
