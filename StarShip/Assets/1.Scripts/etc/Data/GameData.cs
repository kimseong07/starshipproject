using System.Collections;
using System;
using UnityEngine;

[Serializable]
public class GameData
{
    public float _maxhp = 100;
    public float _hp = 100;

    public int _gearValue = 100;

    public int _enemyValue = 10;

    public int _needGear = 1;
    public int _needGearFire = 1;

    public float _cdelay = 0.5f;

    public float _repairHp = 100;

    public bool _doublecan = false;
    public bool _trippleCan = false;
    public bool _tur = false;
    public bool _doubleTur = false;
}

