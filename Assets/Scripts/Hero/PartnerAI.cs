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
        var rootRepeat = new NaiveRepeater("rootRepeat", bt);
        //var riskSwitch = new ConditionNode("riskSwitch", RiskBranch, bt);
        var riskSel = new SelectorNode("riskSel", bt);
        var attackSeq = new SequenceNode("attackSeq", bt);
        var defendSeq = new SequenceNode("defendSeq", bt);
        var approachTarget = new ActionNode("approachTarget", hero.ApproachTarget, bt);
        var moveAroundTarget = new ActionNode("moveAroundTarget", hero.MoveAroundTarget, bt);
        var safePlay = new SequenceNode("safePlay", bt);
        var meleeAttack = new ActionNode("meleeAttack ", hero.MeleeAttack, bt);

        rootRepeat.Build(
            riskSel.Build(
                attackSeq.Build(
                    approachTarget,
                    meleeAttack
                ),
                defendSeq.Build(
                    approachTarget,
                    moveAroundTarget,
                    meleeAttack
                )
            )
        );
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
        path = pathFinder.AStarPath(start, goal);
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
