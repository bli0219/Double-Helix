using System.Collections;
using System.Collections.Generic;


public class A_Star {

    // Use this for initialization

    SortedDictionary<float, Node> open;
    HashSet<Node> closed;
    List<List<Node>> map;
    Node start;
    Node end;


    public A_Star() {
        open = new SortedDictionary<float, Node>();
        closed = new HashSet<Node>();
    }

    public void Initialize(List<List<Node>> _map, Node _start, Node _end) {
        map = _map;
        start = _start;
        end = _end;
        foreach (List<Node> list in map) {
            foreach (Node node in list) {
                node.g = float.MaxValue;
            }
        }
        start.g = 0f;
    }

    public List<Node> OptimalPath() {

        List<Node> path = new List<Node>();
        


        open.Add(start.f, start);

        return path;
    }

}
