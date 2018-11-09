using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MyBehaviorTree;

public class PartnerAI : MonoBehaviour {

    public PathFinder pathFinder;
    public Enemy target;
    public Hero hero;
    public BehaviorTree bt;

    Node start;
    Node goal;
    List<Node> path;
    float time;

    Dictionary<string, float> parameters = new Dictionary<string, float> {
        {"RiskThreshold", 0.5f }
    };

    void Start() {
        BuildBT();
    }

    void BuildBT() {
        var root = new NaiveRepeater("root", bt);
        var riskSwitch = new ConditionNode("riskSwitch", RiskBranch, bt);
        var riskyPlay = new SequenceNode("riskyPlay", bt);
        var safePlay = new SequenceNode("safePlay", bt);
        var meleeAttack = new ActionNode("meleeAttack ", hero.MeleeAttack, bt);
    }

    #region Action

    #endregion

    #region Conditional

    // riskThreshold <- [0,1]
    NodeStatus RiskBranch() {
        if (EstimateRisk() < parameters["RiskThreshold"]) {
            return NodeStatus.Success;
        } else {
            return NodeStatus.Failure;
        }
    }  

    float EstimateRisk() {
        float risk = 0;
        risk += (100 - hero.health)/100;
        return risk;
    }

    #endregion



    void FixedUpdate() {
        bt.Tick();
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
