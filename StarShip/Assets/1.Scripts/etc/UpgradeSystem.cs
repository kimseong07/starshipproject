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

    RepairShip repair;
    ShipMove ship;
    ScoreManager score;

    private int needGear = 1;
    private int needGearFire = 1;
    void Start()
    {
        repair = FindObjectOfType<RepairShip>();
        ship = FindObjectOfType<ShipMove>();
        score = FindObjectOfType<ScoreManager>();
    }

    public void Repair()
    {
        if (score.gearValue < 1)
        {
            Debug.Log("기어가 부족합니다");
        }
        else if(score.gearValue > 1 && ship.hp >= ship.maxhp)
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
        }
    }

    public void Fire()
    {
        if(score.gearValue < needGearFire)
        {
            Debug.Log("기어가 부족합니다");
        }
        else if(score.gearValue > needGearFire)
        { 
            score.gearValue -= needGearFire;

            ba_se.GetComponent<Shooter>().Cdelay -= 0.02f;

            canLeft.GetComponent<Shooter>().Cdelay -= 0.02f;
            canRight.GetComponent<Shooter>().Cdelay -= 0.02f;
            turretObject.GetComponent<Shooter>().Cdelay -= 0.02f;
            turretObject2.GetComponent<Shooter>().Cdelay -= 0.02f;

            needGearFire *= 2;
        }
    }

    public void doubleCannon()
    {
        if (score.gearValue < 20)
        {
            Debug.Log("기어가 부족합니다");
        }
        else if (score.gearValue >= 20)
        {
            score.gearValue -= 20;
            //ba_se.SetActive(false);
            cannon.SetActive(true);
            cannonButton.gameObject.SetActive(false);
        }
    }
    public void trippleCannnon()
    {
        if (score.gearValue < 30)
        {
            Debug.Log("기어가 부족합니다");
        }
        else if(score.gearValue >= 30)
        {
            score.gearValue -= 30;
            ba_se.SetActive(true);

            nCanButton.interactable = false;
        }
    }

    public void turret()
    {
        if (score.gearValue < 30)
        {
            Debug.Log("기어가 부족합니다");
        }
        else if (score.gearValue >= 30)
        {
            score.gearValue -= 30;
            turretObject.SetActive(true);
            turretButton.gameObject.SetActive(false);
        
        }
    }
    public void doubleTurret()
    {
        if (score.gearValue < 30)
        {
            Debug.Log("기어가 부족합니다");
        }
        else if (score.gearValue >= 30)
        {
            score.gearValue -= 30;
            turretObject2.SetActive(true);

            nTurretButton.interactable = false;
        }
    }
}
