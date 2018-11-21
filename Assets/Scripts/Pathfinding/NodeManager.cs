using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wintellect.PowerCollections;

public class NodeManager : MonoBehaviour {

    public static NodeManager Instance;
    public static Enemy target;
    public static Node nearest;
//    public GameObject enemyParent;

    Vector2 bottomLeft = new Vector2(-1.7f, -1f);
    Vector2 topRight = new Vector2(1.7f, 1f);
    float dist = 0.05f;

    float time = 0f;
    public List<List<Node>> nodeMap;
    //List<List<int>> activeMap;
    //public Node[,] nodeMap;
    List<Enemy> enemies;

    int xCount = 0;
    int yCount = 0;
    int count = 0;


    void Awake() {
        Instance = this;
        time = Time.realtimeSinceStartup;
        CreateNodes();
        Debug.Log("Creating " + count + " Nodes takes " + (Time.realtimeSinceStartup - time));
        time = Time.realtimeSinceStartup;
        AssignNeighbors();
        Debug.Log("Assigning Neighbors takes " + (Time.realtimeSinceStartup - time));
        Test();
    }

    /* Test History
     * 
     * OverlapBox > OverlapPoint >> OverlapCircle
     * runtime does not scale with area for all
     * 0.005 sec for 10000 box, 0.008 sec for 10000 point, 1.4 sec for 1000 circle
     * 
     */
    void Test() {
        //time = Time.realtimeSinceStartup;
        //for (int i=0; i!=1000; i++) {

        //}
        //Debug.Log(" takes " + (Time.realtimeSinceStartup-time));
    }

    //public void EnableAndDisableNodes() {
    //    Enemy[] enemies = EnemyManager.Instance.enemies;
    //}

    void Update() {

    }

    float SqrDist(Vector2 v2, Vector3 v3) {
        return (v2.x - v3.x) * (v2.x - v3.x) + (v2.y - v3.y) * (v2.y - v3.y);
    }

    public void UpdateThreatCosts() {
        foreach (List<Node> row in nodeMap) {
            foreach (Node node in row) {
                float t_cost = 0f;
                foreach (Enemy enemy in enemies) {
                    t_cost += enemy.threat / SqrDist(node.pos, enemy.transform.position);
                }
                node.t = t_cost;
            }
        }
    }

    public bool Contains(Node start, Node end) {
        bool found1 = false;
        bool found2 = false;
        while (!found1 || !found2) {
            foreach(List<Node> row in nodeMap) {
                foreach (Node node in row) {
                    if (node == start) found1 = true;
                    if (node == end) found2 = true;
                }
            }
        }
        return found1 && found2;
    }

    public Node NearestNode(GameObject go) {
        Vector3 enemyPos = go.transform.position;
        int numX = (int)Mathf.Round((enemyPos.x - bottomLeft.x) / dist);
        int numY = (int)Mathf.Round((enemyPos.y - bottomLeft.y) / dist);

        if (numX < 0 || numY < 0 || numX >= xCount || numY >= yCount) {
            Debug.LogError("GameObject out of node map. x=" + numX + " y=" + numY );
            numX = Mathf.Clamp(numX, 0, xCount - 1);
            numY = Mathf.Clamp(numY, 0, yCount - 1);
        }
        nearest = nodeMap[numY][numX];
        return nearest;
    }

    public void ChangeTarget(Enemy enemy) {
        target = enemy;
    }

    void CreateNodes () {
        yCount = Mathf.CeilToInt((topRight.y - bottomLeft.y) / dist);
        xCount = Mathf.CeilToInt((topRight.x - bottomLeft.x) / dist);
        count = xCount * yCount;
        nodeMap = new List<List<Node>>();
        //activeMap = new List<List<int>>();

	    for (int y = 0; y < yCount; y ++) {
            List<Node> rowNode = new List<Node>();
            //List<int> rowActive = new List<int>();
            for (int x = 0; x < xCount; x++) {
                float _x = bottomLeft.x + x * dist;
                float _y = bottomLeft.y + y * dist;
                Collider2D col = Physics2D.OverlapBox(new Vector2(_x, _y), new Vector2(0.1f, 0.1f), 0f);
                if (col != null && col.tag == "Block") {
                    rowNode.Add(null);
                    //rowActive.Add(0);
                } else {
                    rowNode.Add(new Node(_x, _y));
                    //rowActive.Add(1);
                }
            }
            nodeMap.Add(rowNode);
            //activeMap.Add(rowActive);
        }
    }

    /*
     * TODO: CAN WE JUST ASSUME NEIGHBORS ARE +-1 ON X AND Y WITHOUT STORING THEM?
     */
    void AssignNeighbors() { 

        time = Time.realtimeSinceStartup;
        for (int y = 0; y < yCount; y++) {
            for (int x = 0; x < xCount; x++) {

                if (nodeMap[y][x] != null) {
                    List<Node> neighbors = nodeMap[y][x].neighbors;
                    // Loop through 8 neighbors 1 index away
                    for (int offsetY = -1; offsetY <= 1; offsetY++) {
                        for (int offsetX = -1; offsetX <= 1; offsetX++) {
                            // if not the node itself
                            if (offsetX != 0 || offsetY != 0) {
                                // actual x, y 
                                int indexY = y + offsetY;
                                int indexX = x + offsetX;
                                // if within bounds
                                if (indexY < yCount && indexY >= 0
                                    && indexX < xCount && indexX >= 0) {
                                    // not checking null, add null if it's null
                                    neighbors.Add(nodeMap[indexY][indexX]);
                                }
                            }
                        }
                    }
                }
            }
        }
        //Debug.Log("assigning neighbors for " + count + " nodes takes " + (Time.realtimeSinceStartup - time) + "seconds");
    }

}
