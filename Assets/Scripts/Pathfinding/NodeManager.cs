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

    public Node NearestNode(GameObject go) {
        Vector3 enemyPos = go.transform.position;
        int numX = (int)Mathf.Round((enemyPos.x - bottomLeft.x) / dist);
        int numY = (int)Mathf.Round((enemyPos.y - bottomLeft.y) / dist);

        if (numX < 0 || numY < 0) {
            Debug.LogError("GameObject out of node map. x=" + numX + " y=" + numY );
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

	    for (int y = 0; y < yCount; y ++) {
            List<Node> row = new List<Node>();
            for (int x = 0; x < xCount; x++) {
                float _x = bottomLeft.x + x * dist;
                float _y = bottomLeft.y + y * dist;
                Collider2D col = Physics2D.OverlapBox(new Vector2(_x, _y), new Vector2(0.1f, 0.1f), 0f);
                if (col != null && col.tag == "Block")
                    row.Add(null);
                else
                    row.Add(new Node(_x, _y));
            }
            nodeMap.Add(row);
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
