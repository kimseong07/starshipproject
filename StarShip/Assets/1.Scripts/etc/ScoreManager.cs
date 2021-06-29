using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text gear;
    public Text hp;
    public Text enemyt;
    public Text timet;

    private float time = 12f;

    public int gearValue = 0;
    public int enemyValue = 0;

    ShipMove ship;
    Spawner spawn;
    void Start()
    {
        ship = FindObjectOfType<ShipMove>();
        spawn = FindObjectOfType<Spawner>();

        gear.text = "" + gearValue;
        hp.text = ship.maxhp + " /" + ship.hp;
    }

    void Update()
    {
        enemyValue = spawn.maxEnemy;

        gear.text = "" + gearValue;
        hp.text = ship.maxhp + " /" + ship.hp;
        enemyt.text = "" + enemyValue;

        if(enemyValue <= 0)
        {
            timet.gameObject.SetActive(true);
            time -= Time.deltaTime;

            timet.text = "" + time;
            if(time <= 0)
            {
                timet.gameObject.SetActive(false);
                time = 12f;
                spawn.maxEnemy = spawn.stageEnemy;
            }
        }
    }
}
