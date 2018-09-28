using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Level {
    mountain, hill
}

public class EnemyManager : MonoBehaviour {

    public static EnemyManager Instance;
    public Dictionary<Level, List<Enemy>> levelDict = new Dictionary<Level, List<Enemy>>();
    public float alertRange = 2f;
    public Enemy[] enemies;

    void Awake() {
        Instance = this;

        enemies = FindObjectsOfType<Enemy>();
        foreach (Level level in (Level[]) Enum.GetValues(typeof(Level))) {
            List<Enemy> list = new List<Enemy>();
            levelDict.Add(level, list);
        }
        foreach (Enemy enemy in enemies) {
            levelDict[enemy.level].Add(enemy);
        }
    }

    void Start() {
        
    }
    public Enemy GetTarget(Level level) {

        List<Enemy> enemies = levelDict[level];
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
        foreach (Enemy enemy in levelDict[from.level]) {
            if (Vector2.Distance(enemy.transform.position, from.transform.position) < alertRange) {
                enemy.TurnAlert();
            }
        }
    }


}
