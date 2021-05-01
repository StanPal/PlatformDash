using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField]
    private Queue<GameObject> enemyList = new Queue<GameObject>();
    [SerializeField]
    private int maxSpawn = 5;
    public Queue<GameObject> Enemies { get { return enemyList; } }

    public bool canSpawn()
    {
        if (Enemies.Count <= maxSpawn)
            return true;
        return false;
    }


}
