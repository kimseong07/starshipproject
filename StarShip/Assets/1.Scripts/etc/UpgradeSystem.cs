using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeSystem : MonoBehaviour
{
    public GameObject ba_se;

    public GameObject cannon;
    public GameObject canLeft;
    public GameObject canRight;
    public GameObject turretObject;
    public GameObject turretObject2;

    public Button cannonButton;
    public Button nCanButton;
    public Button turretButton;
    public Button nTurretButton;

    ShipMove ship;
    ScoreManager score;

    DataController dataController;

    public int needGear = 1;
    public int needGearFire = 1;

    public bool doubleCan = false;
    public bool tripleCan = false;
    public bool tur = false;
    public bool doubleTur = false;

    void Start()
    {
        dataController = FindObjectOfType<DataController>();
        ship = FindObjectOfType<ShipMove>();
        score = FindObjectOfType<ScoreManager>();

        needGear = dataController.gameData._needGear;
        needGearFire = dataController.gameData._needGearFire;
        doubleCan = dataController.gameData._doublecan;
        tripleCan = dataController.gameData._trippleCan;
        tur = dataController.gameData._tur;
        doubleTur = dataController.gameData._doubleTur;
    }
    public void Update()
    {
        if (doubleCan == true)
        {
            cannon.SetActive(true);
            cannonButton.gameObject.SetActive(false);

            dataController.gameData._doublecan = doubleCan;
        }

        if (tripleCan == true)
        {
            ba_se.SetActive(true);

            nCanButton.interactable = false;

            dataController.gameData._trippleCan = tripleCan;
        }

        if (tur == true)
        {
            turretObject.SetActive(true);
            turretButton.gameObject.SetActive(false);

            dataController.gameData._tur = tur;
        }

        if (doubleTur == true)
        {
            turretObject2.SetActive(true);

            nTurretButton.interactable = false;

            dataController.gameData._doubleTur = doubleTur;
        }
    }

    public void Repair()
    {
        dataController.gameData._gearValue = score.gearValue;

        if (score.gearValue < 1)
        {
            Debug.Log("기어가 부족합니다");
        }
        else if (score.gearValue > 1 && ship.hp >= ship.maxhp)
        {
            Debug.Log("hp가 최대치 입니다");
        }
        else if (score.gearValue > 1 && ship.hp < ship.maxhp)
        {
            score.gearValue -= 1;
            ship.hp = ship.maxhp;
        }
    }

    public void Duraility()
    {
        dataController.gameData._gearValue = score.gearValue;

        if (score.gearValue < needGear)
        {
            Debug.Log("기어가 부족합니다");
        }
        else if (score.gearValue > needGear)
        {
            score.gearValue -= needGear;
            ship.maxhp += 10;
            ship.hp = ship.maxhp;
            needGear += 1;

            dataController.gameData._maxhp = ship.maxhp;

            dataController.gameData._needGear = needGear;
        }
    }

    public void Fire()
    {
        dataController.gameData._gearValue = score.gearValue;

        if (score.gearValue < needGearFire)
        {
            Debug.Log("기어가 부족합니다");
        }
        else if (score.gearValue > needGearFire)
        {
            score.gearValue -= needGearFire;

            ba_se.GetComponent<Shooter>().Cdelay -= 0.02f;

            canLeft.GetComponent<Shooter>().Cdelay -= 0.02f;
            canRight.GetComponent<Shooter>().Cdelay -= 0.02f;
            turretObject.GetComponent<Shooter>().Cdelay -= 0.02f;
            turretObject2.GetComponent<Shooter>().Cdelay -= 0.02f;

            needGearFire *= 2;

            dataController.gameData._needGearFire = needGearFire;
        }
    }

    public void doubleCannon()
    {
        dataController.gameData._gearValue = score.gearValue;

        if (score.gearValue < 20)
        {
            Debug.Log("기어가 부족합니다");
        }
        else if (score.gearValue >= 20)
        {
            score.gearValue -= 20;

            doubleCan = true;
        }
    }
    public void trippleCannnon()
    {
        dataController.gameData._gearValue = score.gearValue;

        if (score.gearValue < 30)
        {
            Debug.Log("기어가 부족합니다");
        }
        else if (score.gearValue >= 30)
        {
            score.gearValue -= 30;

            tripleCan = true;
        }
    }

    public void turret()
    {
        dataController.gameData._gearValue = score.gearValue;

        if (score.gearValue < 30)
        {
            Debug.Log("기어가 부족합니다");
        }
        else if (score.gearValue >= 30)
        {
            score.gearValue -= 30;

            tur = true;
        }
    }
    public void doubleTurret()
    {
        dataController.gameData._gearValue = score.gearValue;

        if (score.gearValue < 30)
        {
            Debug.Log("기어가 부족합니다");
        }
        else if (score.gearValue >= 30)
        {
            score.gearValue -= 30;

            doubleTur = true;
        }
    }
}
