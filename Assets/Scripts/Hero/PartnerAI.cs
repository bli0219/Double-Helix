using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using BehaviorTree;

public class PartnerAI : MonoBehaviour {

    public PathFinder pathFinder;
    public Enemy target;
    public Hero hero;

    Node start;
    Node goal;
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
        if ((target.PositionV2() - goal.pos).magnitude > 0.05f) {
            FindPath();
        }
        MoveAlongPath();
    }

    IEnumerator Task1(System.Action<int> follow) {
        yield return 1;
    }
    public void TestMethod2(int i) {

    }
    public int TestMethod() {
        return 0;
    }
    void CallFunc(Func<int> method) {
        int i = method();
    }
    void Test() {
        StartCoroutine(
            Task1(
                (x) => { Debug.Log(x); }
            )
        );
    }

    #region BattleAI

    NodeStatus Template() {
        return NodeStatus.Success;
    }

    NodeStatus NaiveFollow() {

        if ( Vector3.Distance(hero.transform.position, target.transform.position) < 1f ) {
            return NodeStatus.Success;
        }

        if (FindPath()) {
            return NodeStatus.Failure;
        }

        MoveAlongPath();
        return NodeStatus.Running;
    }



    #endregion

    #region Pathfinding



    bool FindPath() {
        start = NodeManager.Instance.NearestNode(this.gameObject);
        goal = NodeManager.Instance.NearestNode(target.gameObject);
        time = Time.realtimeSinceStartup;
        path = pathFinder.BestPath(start, goal);
        return path != null;
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
    #endregion
}
