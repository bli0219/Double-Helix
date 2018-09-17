using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartnerAI : MonoBehaviour {

    PathFinder pf = new PathFinder();
    public Enemy target;
    public Hero hero;
    List<Node> path;

    void Update() {
        if (target == null) {
            EnemyManager.Instance.GetTarget(hero.group);
        }
    }

	void FindPath() {
        Node goal = NodeManager.Instance.NearestNode(target);
        path = pf.BestPath(goal);
    }

    void MoveAlongPath() {
        if (Vector2.Distance(new Vector2(transform.position.x, transform.position.y), path[0].pos) < 0.2f) {
            path.RemoveAt(0);
        } else {
            hero.MoveTowards(path[0].pos);
        }
    }
}
