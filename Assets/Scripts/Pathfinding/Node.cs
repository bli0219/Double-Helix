using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node {

    public Vector2 pos;
    public float f;
    public float g;
    public float h;
    //public float rhs;
    //public float key1;
    //public float key2;
    public Node[][] neighbors;

    public Node(float posX, float posY) {
        pos = new Vector2(posX, posY);
        g = float.MaxValue;
        rhs = float.MaxValue;
        // Consistent when initialized
        neighbors = new Node[3][] {
            new Node[3], new Node[3], new Node[3]
        };
    }

    void UpdateKeys() {
        
    }

    void UpdateG() {
        
    }

    void UpdateH() {
        
    }

    float SqrDist(float x, float y) {
        return x * x + y * y;
    }

    public void UpdateCosts(List<Enemy> enemies, Enemy target) {
        float cost = 0f;
        foreach(Enemy e in enemies) {
            float sqrDist = SqrDist(e.transform.position.x - pos.x, e.transform.position.y - pos.y);
            float scaledThreat = e.threat / sqrDist;
            cost += scaledThreat;
        }

        Vector3 targetPos = target.transform.position;
        float scaledAttraction = target.attraction / SqrDist(targetPos.x - pos.x, targetPos.y - pos.y);
        cost -= scaledAttraction;
    }


}
