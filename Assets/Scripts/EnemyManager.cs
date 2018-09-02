using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Map {
    someMap, otherMap
}

public class EnemyManager : MonoBehaviour {

    public static EnemyManager Instance;

    Dictionary<Map, List<Enemy>> mapDict;
    public List<Enemy> someMap;
    public float alertRange = 2f;

    void Awake() {
        Instance = this;
        
    }

    public void WarnNearby(Enemy from) {
        foreach (Enemy enemy in mapDict[from.map]) {
            if (Vector2.Distance(enemy.transform.position, from.transform.position) < alertRange) {
                enemy.TurnAlert();
            }
        }
    }


}
