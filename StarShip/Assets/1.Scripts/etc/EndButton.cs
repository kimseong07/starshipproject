using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

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
       
        Application.Quit();
    }
}
