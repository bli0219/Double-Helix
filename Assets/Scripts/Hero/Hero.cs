using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBehaviorTree;

public class Hero : MonoBehaviour {

    Rigidbody2D rb;
    List<GameObject> arrows;
    Vector3 arrowPos = new Vector3(0.15f, 0f, 0f);
    public GameObject arrowPrefab;
    GameObject arrow;
    Animator anim;
    GameObject hitbox;
    public Enemy target;
    public PathFinder pf;
    public BehaviorTree bt;
    List<Node> path;

    Vector3 faceDir;
    float bowChargeLimit = 1f;
    float chargeTime = 0f;
    public float speed = 2f;
    public float dashSpeed = 5f;
    public Level level = Level.hill;
    public int health = 100;
    float meleeRange = 1f;
    float attackRange = 0.5f;
    int hitCount = 0;
    bool disabled = false;
    bool aiming = false;
    bool dashing = false;
    bool dodging = false;
    bool attacked = false;
    public bool usingAI = false;
    int attackCount = 0;

    SortedDictionary<float ,float > dict;

    #region MonoBehavior

    void Awake () {
        faceDir = Vector3.zero;
        rb = GetComponent<Rigidbody2D>();
        arrows = new List<GameObject>();
        hitbox = transform.GetChild(0).gameObject;

        if (usingAI) {
            bt = GetComponent<BehaviorTree>();
            //pf = new PathFinder();
            BuildTree();
        }
    }

    void Start() {
        target = EnemyManager.Instance.GetTarget(Level.hill);    
    }

    void BuildTree() {
        var rootRepeat = new NaiveRepeater("rootRepeat", bt);
        //var riskSwitch = new ConditionNode("riskSwitch", RiskBranch, bt);
        //var approachAttack = new SequenceNode("approachAttack", bt);
        //var pathFinding = new SelectorNode("pathFinding", bt);
        //var repeatTillFail = new SuccessRepeater("repeatTillFail", bt);
        //var approachSeq = new SequenceNode("repeatSq", bt);
        //var findPath = new ActionNode("findPath", FindPath, bt);
        //var pathApproach = new ActionNode("pathApproach", PathApproach, bt);
        //var closeToTarget = new ActionNode("checkDistance", CloseToTarget, bt);
        var tryMeleeAttack = new SequenceNode("tryMeleeAttack", bt);
        var getInRange = new ActionNode("getInRange", GetInRange, bt);
        var meleeAttackAction = new ActionNode("meleeAttackAction", MeleeAttackAction, bt);
        var idleAction = new ActionNode("idleAction", IdleAction, bt);

        rootRepeat.Build(
            tryMeleeAttack.Build(
                getInRange,
                meleeAttackAction
            )
        );
        bt.Build(rootRepeat);

        //rootRepeat.Build(
        //    approachAttack.Build(
        //         pathFinding.Build(
        //             repeatTillFail.Build(
        //                 approachSeq.Build(
        //                     findPath,
        //                     pathApproach
        //                 )
        //             ),
        //             closeToTarget 
        //        ),
        //        tryMeleeAttack.Build(
        //            getInRange,
        //            meleeAttackAction
        //        )
        //    )
        //);
    }

    void Update() {

        EnemyDetection();

        if (arrow != null) {
            arrow.transform.localPosition = arrowPos;
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        //if (other.tag == "Enemy") {
        //    if (dashing) {
        //        Debug.Log("Stop");
        //        rb.velocity = Vector2.zero;
        //    } else {
        //        Debug.Log("not dashing");
        //    }
        //}
        if (other.tag == "EnemyHitbox") {
            Attacked(other);
        }
    }

    #endregion

    #region Unit Action

    void Stop() {
        rb.velocity = Vector2.zero;
    }

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

    void Attacked(Collider2D other) {
        attacked = true;
        attackCount++;
        rb.velocity = Direction2D(other.transform).normalized * -2f;
        Invoke("ResumeFromAttack", 0.2f);
        //    anim.SetBool("Attacked", true);
        //    Invoke("ResumeFromAttack", 0.5f);

    }

    void ResumeFromAttack() {
        attacked = false;
        rb.velocity = Vector2.zero;
    }

    public void MeleeAttack() {
        
    }


    #endregion



    #region AI Action

    void Finish(bool success) {
        if (success) {
            bt.FinishSuccess();
        } else {
            bt.FinishFailure();
        }
    } 
    void CloseToTarget() {
        Finish(DistanceToTarget() < 1f);
    }

    void NaiveApproach() {
        MoveToPoint(target.PositionV2());
    }

    void PathApproach() {
        if (path.Count != 0) {
            if (Vector2.Distance(new Vector2(transform.position.x, transform.position.y), path[0].pos) < 0.05f) {
                path.RemoveAt(0);
            } else {
                MoveToPoint(path[0].pos);
            }
        }
        Invoke("ReturnSuccess", 0.2f);
    }

    void ReturnSuccess() {
        bt.FinishSuccess();
    }

    void FindPath() {

        if (DistanceToTarget() < 1f) {
            bt.FinishFailure();
        }
        Node start = NodeManager.Instance.NearestNode(gameObject);
        Node goal = NodeManager.Instance.NearestNode(target.gameObject);
        path = pf.AStarPath(start, goal);
        // AStarPath returns null when no path found
        if (path != null) {
            bt.FinishSuccess();
        } else {
            bt.FinishFailure();
        }
    }

    void GetInRange() {
        StartCoroutine("GetInRangeCR");
    }

    IEnumerator GetInRangeCR() {
        float dist = DistanceToTarget();
        while (!attacked && dist > attackRange) {
            NaiveApproach();
            dist = DistanceToTarget();
            yield return null;
        }
        if (attacked) {
            bt.FinishFailure();
        } else {
            bt.FinishSuccess();
        }
    }

    void MeleeAttackAction() {
        Debug.Log("hello");
        StartCoroutine("MeleeAttackCR");
        //hitbox.SetActive(true);
    }

    IEnumerator MeleeAttackCR() {
        int count = attackCount;
        yield return new WaitForSecondsRealtime(0.3f);
        bt.Finish(count == attackCount);
    }

    void IdleAction() {
        Debug.Log("IDLE");
    }

    void MoveAroundTarget() {
        StartCoroutine("MoveAroundTargetCR");
    }


    IEnumerator MoveAroundTargetCR() {
        yield return new WaitForSeconds(0.5f);
    }

    /*
    // status set to be Running in Tick()
    IEnumerator MeleeAttackCR() {
        RotateToPoint(target.transform.position);
        anim.SetTrigger("Melee");
        disabled = true;
        yield return new WaitForSecondsRealtime(0.1f);
        hitCount = 0;
        hitbox.SetActive(true);
        yield return new WaitForSecondsRealtime(0.05f);
        hitbox.SetActive(false);
        yield return new WaitForSecondsRealtime(0.2f);
        disabled = false;
        ai.status = hitCount > 0 ? NodeStatus.Success : NodeStatus.Failure;
    }*/


    #endregion

    #region Utility

    float DistanceToTarget() {
        //TODO: Can be optimized
        return Vector3.Distance(target.transform.position, transform.position);
    }

    Vector2 Direction2D(Transform other) {
        Vector3 v3 = other.position - transform.position;
        return new Vector2(v3.x, v3.y);
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
