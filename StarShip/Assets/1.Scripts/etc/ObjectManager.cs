using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject enemy2Prefab;
    public GameObject bulletEPrefab;
    public GameObject bulletPPrefab;
    public GameObject gearPrefab;

    GameObject[] enemyL;
    GameObject[] enemyR;

    GameObject[] bulletEnemy;
    GameObject[] bulletPlayer;

    GameObject[] gear;

    GameObject[] targetPool;


    private void Awake()
    {
        enemyL = new GameObject[30];
        enemyR = new GameObject[20];

        bulletEnemy = new GameObject[50];
        bulletPlayer = new GameObject[50];

        gear = new GameObject[15];

        Generate();
    }

    void Generate()
    {
        for(int index = 0; index < enemyL.Length; index++)
        {
            enemyL[index] = Instantiate(enemyPrefab);
            enemyL[index].SetActive(false);
        }
        
        for(int index = 0; index < enemyR.Length; index++)
        {
            enemyR[index] = Instantiate(enemy2Prefab);
            enemyR[index].SetActive(false);
        }

        for (int index = 0; index < bulletEnemy.Length; index++)
        {
            bulletEnemy[index] = Instantiate(bulletEPrefab);
            bulletEnemy[index].SetActive(false);
        }

        for (int index = 0; index < bulletPlayer.Length; index++)
        {
            bulletPlayer[index] = Instantiate(bulletPPrefab);
            bulletPlayer[index].SetActive(false);
        }

        for (int index = 0; index < gear.Length; index++)
        {
            gear[index] = Instantiate(gearPrefab);
            gear[index].SetActive(false);
        }
    }

    public GameObject MakeObj(string type)
    {
        switch (type)
        {
            case "EnemyL":
                targetPool = enemyL;
                break;

            case "EnemyR":
                targetPool = enemyR;
                break;
            case "BulletEnemy":
                targetPool = bulletEnemy;
                break;

            case "BulletPlayer":
                targetPool = bulletPlayer;
                break;
            case "Gear":
                targetPool = gear;
                break;
        }

        for (int index = 0; index < targetPool.Length; index++)
        {
            if (!targetPool[index].activeSelf)
            {
                targetPool[index].SetActive(true);
                return targetPool[index];
            }
        }

        return null;
    }
}
