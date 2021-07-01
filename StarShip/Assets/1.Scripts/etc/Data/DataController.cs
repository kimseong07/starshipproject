using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataController : MonoBehaviour
{
    static GameObject _container;
    static GameObject Container
    {
        get
        { 
            return _container;
        } 
    }

    static DataController _instance;

    public static DataController Instance
    {
        get 
        { if (!_instance)
            {
                _container = new GameObject(); _container.name = "DataController";
                _instance = _container.AddComponent(typeof(DataController)) as DataController; 
                DontDestroyOnLoad(_container);
            }
            return _instance;
        } 
    }

    public string GameDataFileName = "StarShip.json";
    public GameData _gameData;
    public GameData gameData
    { 
        get
        { 
            if (_gameData == null)
            {
                LoadGameData(); 
                SaveGameData();
            }
            return _gameData;
        } 
    }

    private void Awake()
    {
        LoadGameData();
        SaveGameData();
    }

    public void LoadGameData()
    {
        string filepath = Application.persistentDataPath + GameDataFileName;
        if (File.Exists(filepath))
        {
            Debug.Log("�ҷ����� ����");
            string FromJsonData = File.ReadAllText(filepath);
            _gameData = JsonUtility.FromJson<GameData>(FromJsonData);
        }
        else 
        {
            Debug.Log("���ο� ���� ����");
            _gameData = new GameData();
        }
    }

    public void SaveGameData()
    {
        string ToJsonData = JsonUtility.ToJson(gameData);
        string filePath = Application.persistentDataPath + GameDataFileName;
        File.WriteAllText(filePath, ToJsonData);
        Debug.Log("����Ϸ�");
    }

    private void OnApplicationQuit()
    {
        SaveGameData();
    }
}
