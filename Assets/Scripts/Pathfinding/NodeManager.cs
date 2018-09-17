using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeManager : MonoBehaviour {

    public static NodeManager Instance;
    public static Enemy target;
    public static Node nearest;
    public GameObject enemyParent;

    Vector2 topLeft = new Vector2(0f, 0f);
    Vector2 bottomRight = new Vector2(20f, 10f);
    float dist = 0.1f;

    float time = 0f;
    public List<List<Node>> nodeMap;
    //public Node[,] nodeMap;
    List<Enemy> enemies;

    int xCount = 0;
    int yCount = 0;
    int count = 0;


    void Awake() {
        Instance = this;
    }
    void Start() {
        CreateNodes();
        AssignNeighbors();
    }

    public Node NearestNode(Enemy enemy) {
        Vector3 enemyPos = enemy.transform.position;
        int numX = (int)Mathf.Round((enemyPos.x - topLeft.x) / dist);
        int numY = (int)Mathf.Round((enemyPos.y - topLeft.y) / dist);
        nearest = nodeMap[numY][numX];
        return nearest;
    }

    public void ChangeTarget(Enemy enemy) {
        target = enemy;
    }

    void CreateNodes () {
        yCount = Mathf.CeilToInt((bottomRight.y - topLeft.y) / dist);
        xCount = Mathf.CeilToInt((bottomRight.x - topLeft.x) / dist);
        count = xCount * yCount;
        nodeMap = new List<List<Node>>();

	    for (int y = 0; y < yCount; y ++) {
            List<Node> row = new List<Node>();
            for (int x = 0; x < xCount; x++) {
                row.Add (new Node(topLeft.x + x*dist, topLeft.y + y*dist)); 
            }
            nodeMap.Add(row);
        }
    }

    void AssignNeighbors() {
        time = Time.realtimeSinceStartup;
        for (int y = 0; y < yCount; y++) {
            for (int x = 0; x < xCount; x++) {

                if (nodeMap[y][x] != null) {
                    Node[][] neighbors = nodeMap[y][x].neighbors;
                    for (int ny = 0; ny < 3; ny++) {
                        for (int nx = 0; nx < 3; nx++) {
                            int indexY = y - 1 + ny;
                            int indexX = x - 1 + nx;
                            //TODO: CAN WE JUST ASSUME NEIGHBORS ARE +-1 ON X AND Y?
                            if (indexY < yCount && indexY >= 0
                                && indexX < xCount && indexX >= 0) {
                                neighbors[ny][nx] = nodeMap[indexY][indexX];
                            }
                            neighbors[1][1] = null;
                        }
                    }
                }

            }
        }
        Debug.Log("assigning neighbors for " + count + " nodes takes " + (Time.realtimeSinceStartup - time) + "seconds");

    }

}
