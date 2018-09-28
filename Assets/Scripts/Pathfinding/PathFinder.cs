using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour {

    Node start;
    List<List<Node>> nodeMap;
    A_Star a_star;

    void Awake() {
        nodeMap = NodeManager.Instance.nodeMap;
        a_star = new A_Star();
        a_star.GetMap(nodeMap);
    }

    public void UpdateMap() {
        nodeMap = NodeManager.Instance.nodeMap;
        a_star.GetMap(nodeMap);
    }

    // Recalculate Path
    public List<Node> BestPath(Node start, Node goal) {
        a_star.Initialize(start, goal);
        return a_star.OptimalPath();
    }

}
