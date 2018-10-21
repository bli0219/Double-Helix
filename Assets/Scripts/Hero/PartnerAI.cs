using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PartnerAI : MonoBehaviour {

    public PathFinder pathFinder;
    public Enemy target;
    public Hero hero;
    private Node start;
    private Node goal;
    List<Node> path;
    float time;

    void Start() {
        //if (target == null) {
        //    target = EnemyManager.Instance.GetTarget(hero.level);
        //}
        FindPath();
        
    }

    void Update() {
        //if (Input.GetKeyDown(KeyCode.Space)) {
        if ((target.PositionV2() - goal.pos).magnitude > 0.05f ) {
            FindPath();
        }
        MoveAlongPath();
    }
    void FixedUpdate() {
            
    }

    void FindPath() {
        start = NodeManager.Instance.NearestNode(this.gameObject);
        goal = NodeManager.Instance.NearestNode(target.gameObject);
        time = Time.realtimeSinceStartup;
        path = pathFinder.BestPath(start, goal);
        Debug.Log("A* takes " + (Time.realtimeSinceStartup - time));
    }

    void MoveAlongPath() {
        if (path.Count != 0) {
            if (Vector2.Distance(new Vector2(transform.position.x, transform.position.y), path[0].pos) < 0.05f) {
                path.RemoveAt(0);
            } else {
                //hero.transform.position = new Vector3(path[0].pos.x, path[0].pos.y, 0f);
                //Debug.Log("move");
                //Debug.Log(path[0].pos);
                hero.MoveToPoint(path[0].pos);
            }
        }
    }
}
