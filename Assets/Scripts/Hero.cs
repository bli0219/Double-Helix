using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour {

    Rigidbody2D rb;
    List<GameObject> arrows;
    Vector3 arrowPos = new Vector3(0.15f, 0f, 0f);
    public GameObject arrowPrefab;
    GameObject arrow;
    float bowChargeLimit = 1f;
    float chargeTime = 0f;
    Vector3 faceDir;
    public float speed = 2f;

    void Awake () {
        faceDir = Vector3.zero;
        rb = GetComponent<Rigidbody2D>();
        arrows = new List<GameObject>();
    }

    void Update() {
        if (arrow != null) {
            arrow.transform.localPosition = arrowPos;
        }
    }

    public void MoveTowards(Vector2 dir) {
        rb.velocity = dir * speed;
    }

    public void RotateTowards(Vector3 point) {
        faceDir = point - transform.position;
        float angle = Mathf.Atan2(faceDir.y, faceDir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 7f * Time.deltaTime);
    }

    public void StartCharge() {
        GetArrow();
        chargeTime = Time.time;
    }

    public void EndCharge() {
        float charge = Mathf.Min(Time.time - chargeTime, bowChargeLimit);
        float range = charge;
        float dmg = 2f * charge;
        
        Vector2 velocity = new Vector2(faceDir.x, faceDir.y).normalized * 10f;
        arrow.GetComponent<Arrow>().Launch(velocity, charge, dmg);
        arrow.transform.parent = null;
        arrow = null;
    }

    public void GetArrow() {
        foreach (GameObject a in arrows) {
            if (!a.activeSelf) {
                Debug.Log("find inactive");
                a.SetActive(true);
                arrow = a;
                break;
            }
        }
        if (arrow == null) {
            Debug.Log("null block");
            GameObject go = Instantiate(arrowPrefab, gameObject.transform);
            arrows.Add(go);
            arrow = go;
        }
        arrow.transform.parent = transform;
        arrow.transform.localPosition = arrowPos;
        arrow.transform.localRotation = Quaternion.identity;
    }
 
    void EnemyDetection() {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 10f);
        foreach (Collider2D col in colliders) {
            if (col.tag == "Enemy") {
                Enemy e = col.GetComponent<Enemy>();
                if (e.alert) {
                    e.SeeHero(this);
                } else if (Vector3.Distance(e.transform.position, transform.position) < 5f) {
                    e.SeeHero(this);
                }
            }
        }
    }
}
