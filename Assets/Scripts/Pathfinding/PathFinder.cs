using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class PathFinder : MonoBehaviour {

    Node start;
    List<List<Node>> nodeMap;
    Stopwatch sw = new Stopwatch();
    void Start() {
        GetNodeMap();
        
    }

    void GetNodeMap() {
        //nodeMap = NodeManager.Instance.nodeMap;
    }

    float SqrDist(Vector2 v1, Vector2 v2) {
        return (v1.x - v2.x) * (v1.x - v2.x) + (v1.y - v2.y) * (v1.y - v2.y);
    }

    float SqrDist(Node wp1, Node wp2) {
        return SqrDist(wp1.pos, wp2.pos);
    }

    // Collection type depends on pathfinding algo
    public List<Node> BestPath(Node node) {
        
        List<Node> path = new List<Node>();

        

        return path;
    }

}
