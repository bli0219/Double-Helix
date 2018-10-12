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
    bool aiming = false;
    bool dashing = false;
    bool dodging = false;
    public Level level = Level.hill;
    public GameObject sword;

    SortedDictionary<float ,float > dict;

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

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Enemy") {
            if (dashing) {
                Debug.Log("Stop");
                rb.velocity = Vector2.zero;
            } else {
                Debug.Log("not dashing");
            }
        }
    }

    #region Actions
    public void StandStill() {
        rb.velocity = Vector2.zero;
    }
    public void MoveToPoint(Vector2 point) {
        Vector2 dir = new Vector2(point.x - transform.position.x, point.y - transform.position.y);
        MoveToDirection(dir);
    }

    public void MoveToPoint(Vector3 point) {
        Vector3 dir = point - transform.position;
        MoveToDirection(dir);
    }

    public void MoveToDirection(Vector2 dir) {
        if (!disabled) {
            rb.velocity = dir.normalized * speed;
        }
    }

    public void MoveToDirection(Vector3 dir) {
        MoveToDirection(new Vector2(dir.x, dir.y));
    }

    public void RotateToDir(Vector2 dir) {
        if (!disabled) { 
            if (dir != Vector2.zero) {
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                faceDir = new Vector2(dir.x, dir.y);
            }
        }
    }

    public void RotateToPoint(Vector3 point) {
        if (!disabled) {
            faceDir = point - transform.position;
            //float angle = Mathf.Atan2(faceDir.y, faceDir.x) * Mathf.Rad2Deg;
            //transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            float angle = Mathf.Atan2(faceDir.y, faceDir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            //transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 7f * Time.deltaTime);
        }
    }

    public void StartCharge() {
        GetArrow();
        chargeTime = Time.time;
    }

    public void EndCharge() {
        float charge = Mathf.Min(Time.time - chargeTime, bowChargeLimit);
        float range = charge;
        int damage = (int) (2f * charge);

        Debug.Log("end " + faceDir);

        Vector2 velocity = new Vector2(faceDir.x, faceDir.y).normalized * 10f;
        arrow.GetComponent<Arrow>().Launch(velocity, charge, damage);
        arrow.transform.parent = null;
        arrow = null;
    }

    public void Dash() {
        if (!disabled) {
            rb.velocity = transform.right * dashSpeed ;
            disabled = true;
            dashing = true;
            Invoke("DashEnd", 0.1f);
        }
    }

    void DashEnd() {
        rb.velocity = Vector2.zero;
        disabled = false;
        dashing = false;

    }
    public void Dodge() {
        if (!disabled) {
            rb.velocity = -transform.right * dashSpeed;
            disabled = true;
            dodging = true;
            Invoke("DodgeEnd", 0.1f);
        }
    }

    void DodgeEnd() {
        rb.velocity = Vector2.zero;
        disabled = false;
        dashing = false;

    }

    public void MeleeAttack() {
        StartCoroutine("Wave");
    }

    IEnumerator Wave() {
        sword.SetActive(true);
        float t0 = Time.time;
        float delta = Time.time - t0;
        
        Quaternion start = Quaternion.Euler(0, 0, -30);
        Quaternion end = Quaternion.Euler(0, 0, 30);
        while (delta < 0.1f) {
            sword.transform.localRotation = Quaternion.Slerp(start, end, delta /0.1f );
            delta = Time.time - t0;
            yield return new WaitForSecondsRealtime(0.01f);
        }
        sword.SetActive(false);
    }
    #endregion

    #region Utility
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
 
    // Ask enemies to detect the hero.
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
