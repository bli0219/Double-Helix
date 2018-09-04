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
    public float dashSpeed = 5f;
    bool disabled = false;

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

    #region Actions
    public void MoveTowards(Vector2 dir) {
        if (!disabled) {
            rb.velocity = dir * speed;
        }
    }

    public void RotateTowards(Vector3 point) {
        if (!disabled) {
            faceDir = point - transform.position;
            float angle = Mathf.Atan2(faceDir.y, faceDir.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 7f * Time.deltaTime);
        }
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

    public void Dash() {
        if (!disabled) {
            rb.velocity = transform.right * dashSpeed * 2f;
            disabled = true;
            Invoke("DashEnd", 0.1f);
        }
    }

    void DashEnd() {
        rb.velocity = Vector2.zero;
        disabled = false;
    }
    public void Dodge() {
        if (!disabled) {
            rb.velocity = -transform.right * dashSpeed;
            disabled = true;
            Invoke("DodgeEnd", 0.1f);
        }
    }

    void DodgeEnd() {
        rb.velocity = Vector2.zero;
        disabled = false;
    }
    #endregion
    #region Basic Functions
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
    #endregion
}
