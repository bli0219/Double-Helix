using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBehaviorTree;

public class WolfAI : MonoBehaviour {

    BehaviorTree bt;
    Enemy enemy;

    void Awake() {
        bt = GetComponent<BehaviorTree>();
        enemy = GetComponent<Enemy>();
    }

    void Start() {
        BuildTree();

    }

    void BuildTree() {
        var rootRepeat = new NaiveRepeater("rootRepeat", bt);
        var approach = new ActionNode("approach", Approach, bt);
        //bt.Build(rootRepeat);
    }

    void Approach() {
        StartCoroutine("ApproachCR");
    }

    //IEnumerator ApproachCR() {

    //}
}
