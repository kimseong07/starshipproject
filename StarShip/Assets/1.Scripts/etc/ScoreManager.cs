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

    public Button shopButton;

    public float time = 12f;

    public int gearValue = 0;

    ShipMove ship;
    Spawner spawn;

    DataController dataController;
    void Start()
    {
        ship = FindObjectOfType<ShipMove>();
        spawn = FindObjectOfType<Spawner>();
        dataController = FindObjectOfType<DataController>();

        gear.text = "" + gearValue;
        hp.text = ship.maxhp + " /" + ship.hp;
    }

    void Update()
    {
        gear.text = "" + gearValue;
        hp.text = ship.maxhp + " /" + ship.hp;
        enemyt.text = "" + spawn.maxEnemy;

        if (spawn.maxEnemy <= 0)
        {
            timet.gameObject.SetActive(true);
            shopButton.interactable = true;
            time -= Time.deltaTime;

            timet.text = "" + time.ToString("N2");
            if(time <= 0)
            {
                timet.gameObject.SetActive(false);
                shopButton.interactable = false;
                time = 12f;
                spawn.stageEnemy += 2;
                spawn.maxEnemy = spawn.stageEnemy;
                dataController.gameData._enemyValue = spawn.maxEnemy;
            }
        }
    }
}
