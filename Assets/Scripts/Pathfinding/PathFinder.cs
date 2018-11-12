using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wintellect.PowerCollections;

public class PathFinder : MonoBehaviour {

    // Use this for initialization
    OrderedSet<Node> open;
    HashSet<Node> closed;
    List<List<Node>> map;
    Node start;
    Node goal;
    Node current;

    void Awake() {
        map = NodeManager.Instance.nodeMap;
        open = new OrderedSet<Node>();
        closed = new HashSet<Node>();
    }

    public void UpdateMap() {
        map = NodeManager.Instance.nodeMap;
    }

    // Recalculate Path
    //public List<Node> BestPath(Node start, Node goal) {
    //    a_star.Initialize(start, goal);
    //    return a_star.OptimalPath();
    //}



    public float h_sqrDist(Node node) {
        return Vector2.SqrMagnitude(goal.pos - node.pos);
    }

    public void GetMap(List<List<Node>> _map) {
        map = _map;
    }
    public void Initialize(Node _start, Node _goal) {

        start = _start;
        goal = _goal;
        open.Clear();
        closed.Clear();
        foreach (List<Node> list in map) {
            foreach (Node node in list) {
                if (node != null) {
                    node.g = float.MaxValue;
                    node.h = Heuristic(node, goal);
                    node.parent = null;
                }
            }
        }
        start.g = 0f;
        start.CalcF();
        open.Add(start);
    }

    // Path is from next node to goal
    public List<Node> RecoverPath(Node goal) {
        List<Node> path = new List<Node>();
        path.Add(goal);

        Node cur = current;
        while (cur != start) {
            path.Add(cur);
            cur = cur.parent;
        }
        path.Reverse();
        Debug.Log("Path length " + path.Count);
        return path;
    }

    public List<Node> OptimalPath(Node _start, Node _goal) {
        start = _start;
        goal = _goal;

        int count = 0;
        int count2 = 0;
        while (open.Count != 0) {
            count++; 
            current = open.GetFirst();
            if (current == goal) {
                Debug.Log(count + " times while loop");
                Debug.Log(count + " times add");
                return RecoverPath(current);
            }
            open.RemoveFirst();
            closed.Add(current);

            foreach (Node neighbor in current.neighbors) {
                if (neighbor == null)
                    continue;
                if (closed.Contains(neighbor)) //evaluated
                    continue;
                float new_g = current.g + DistCost(current, neighbor);
                if (new_g < neighbor.g) {
                    neighbor.parent = current;
                    neighbor.g = new_g;
                    neighbor.CalcF();
                }
                if (!open.Contains(neighbor)) { //not added
                    if (neighbor == goal) {
                        Debug.Log(neighbor.parent);
                    }
                    open.Add(neighbor);
                    count2++;
                }

                //better path since new_g < neighbor.g, update neighbor

            }
        }

        Debug.LogError("No path found.");
        return null;
    }

    float Heuristic(Node a, Node b) {
        //return Vector2.SqrMagnitude(a.pos - b.pos);
        return Vector2.Distance(a.pos, b.pos);
        //return Mathf.Min(Mathf.Abs(a.pos.x - b.pos.x), Mathf.Abs(a.pos.y - b.pos.y));
    }

    float DistCost(Node a, Node b) {
        //return Mathf.Min(Mathf.Abs(a.pos.x - b.pos.x), Mathf.Abs(a.pos.y - b.pos.y));
        //return Vector2.Distance(a.pos, b.pos);
        return Vector2.SqrMagnitude(a.pos - b.pos);
    }

}
