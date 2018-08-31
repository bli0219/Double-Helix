using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    Vector3 mousePos;
    float bowChargeLimit = 1f;
    float chargeTime = 0f;
    List<GameObject> arrows;
    public GameObject arrowPrefab;
    GameObject arrow;
    Rigidbody2D rb;
    public float speed = 2f;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        arrows = new List<GameObject>();
    }

    void GetArrow() {
        foreach(GameObject a in arrows) {
            if (!a.activeSelf) {
                a.SetActive(true);
                arrow = a;
                break;
            }
        }
        if (arrow == null) {
            GameObject go = Instantiate(arrowPrefab, gameObject.transform);
            arrows.Add(go);
            arrow = go;
        }
        arrow.transform.parent = transform;
        arrow.transform.localPosition= new Vector3(0.15f, 0f, 0f);
    }

	void Update () {
        mousePos = Input.mousePosition;
        mousePos.z = 0f;
        Vector3 mouseDir = Camera.main.ScreenToWorldPoint(mousePos) - transform.position;
        float angle = Mathf.Atan2(mouseDir.y, mouseDir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 10f*Time.deltaTime);

        if (Input.GetButtonDown("bow")) {
            chargeTime = Time.time;
            GetArrow();
        }
        if (Input.GetButtonUp("bow")) {
            float charge = Mathf.Min(Time.time-chargeTime, bowChargeLimit);
            float range = charge;
            float dmg = 2f * charge;
            arrow.GetComponent<Rigidbody2D>().AddForce(new Vector2(mouseDir.x, mouseDir.y).normalized * 500f);
            arrow.transform.parent = null;
        }

        float hor = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");
        rb.velocity = new Vector2(hor, vert) * speed;

	}
}
