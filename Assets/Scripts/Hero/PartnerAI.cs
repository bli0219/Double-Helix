using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MyBehaviorTree;

public class PartnerAI : MonoBehaviour {

    public Enemy target;
    public Hero hero;
    public BehaviorTree bt;
    public NodeStatus status;

    Node start;
    Node goal;
    List<Node> path;
    public PathFinder pathFinder;

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
    bool RiskBranch() {
        return EstimateRisk() < parameters["RiskThreshold"];
    }

    float EstimateRisk() {
        float risk = 0;
        risk += (100 - hero.health) / 100;
        return risk;
    }

    #endregion

    public void NaiveFollow() {
        if (Vector3.Distance(transform.position, target.transform.position) < 1f) {

        }

    }


    void FindPath() {
        start = NodeManager.Instance.NearestNode(this.gameObject);
        goal = NodeManager.Instance.NearestNode(target.gameObject);
        path = pathFinder.OptimalPath(start, goal);
    }

    void MoveAlongPath() {
        if (path.Count != 0) {
            if (Vector2.Distance(new Vector2(transform.position.x, transform.position.y), path[0].pos) < 0.05f) {
                path.RemoveAt(0);
            } else {
                hero.MoveToPoint(path[0].pos);
            }
        }
    }
}
