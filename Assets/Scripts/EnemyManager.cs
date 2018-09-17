using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Group {
    mountain, hill
}

public class EnemyManager : MonoBehaviour {

    public static EnemyManager Instance;

    Dictionary<Group, List<Enemy>> groupDict;
    public List<Enemy> someMap;
    public float alertRange = 2f;

    void Awake() {
        Instance = this;
    }

    public Enemy GetTarget(Group group) {

        List<Enemy> enemies = groupDict[group];
        if (enemies.Count < 1) {
            Debug.Log("empty map");
        }

        Enemy target = enemies[0];
        foreach (Enemy e in enemies) {
            if (e.attraction > target.attraction) {
                target = e;
            }
        }

        return target;
    }

    public void WarnNearby(Enemy from) {
        foreach (Enemy enemy in groupDict[from.group]) {
            if (Vector2.Distance(enemy.transform.position, from.transform.position) < alertRange) {
                enemy.TurnAlert();
            }
        }
    }


}
