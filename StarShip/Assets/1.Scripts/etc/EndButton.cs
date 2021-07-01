using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class EndButton : MonoBehaviour
{
    DataController dataController;

    private void Start()
    {
        dataController = FindObjectOfType<DataController>();
    }
    public void Retry()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void End()
    {
        dataController.SaveGameData();
        Application.Quit();
    }
}
