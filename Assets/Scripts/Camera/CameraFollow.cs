using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    Camera camera;
    GameObject player;
    public GameObject boss;
    Vector3 offset; // y offset

    void Awake() {
        camera = GetComponent<Camera>();
    }
    void Start() {
        player = Player.instance.gameObject;
        offset = transform.position - player.transform.position;
    }

    void Update() {
        if (boss == null) {
            transform.position = offset + player.transform.position;
        } else {
            transform.position = offset + (player.transform.position + boss.transform.position) / 2;
            float xDist = Mathf.Abs( player.transform.position.x - boss.transform.position.x);
            float yDist = Mathf.Abs(player.transform.position.y - boss.transform.position.y);
            
            // assuming a 16:9 screen
            float max = Mathf.Max(xDist / 3f, yDist / 1.6875f);
            camera.orthographicSize = Mathf.Max(1, max);


        }
    }
}
