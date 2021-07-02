using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public string[] enemyObj;

    public int stageEnemy = 10;
    public int maxEnemy = 10;
    public int enemyCount;

    private float delay = 1f;

    public ObjectManager objectManager;

    DataController dataController;

    private void Awake()
    {
        dataController = FindObjectOfType<DataController>();
        enemyObj = new string[] { "EnemyL" , "EnemyR"};
        spawnPoints = GameObject.Find("Spawner").GetComponentsInChildren<Transform>();
    }

    private void Start()
    {
        maxEnemy = dataController.gameData._enemyValue;
    }

    private void Update()
    {
        int idx = Random.Range(1, spawnPoints.Length);

        enemyCount = (int)GameObject.FindGameObjectsWithTag("Enemy").Length;

        if (spawnPoints.Length > 0)
        {
            if (enemyCount < maxEnemy)
            {
                if (delay <= 0)
                {
                    GameObject enemy = objectManager.MakeObj(enemyObj[0]);
                    enemy.transform.position = spawnPoints[idx].position;

                    GameObject enemy2 = objectManager.MakeObj(enemyObj[1]);
                    enemy2.transform.position = spawnPoints[idx].position;

                    delay = 1f;
                }
            }
        }
        delay -= Time.deltaTime;
    }
}
