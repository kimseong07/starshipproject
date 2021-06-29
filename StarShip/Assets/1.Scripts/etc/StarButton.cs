using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class StarButton : MonoBehaviour
{
    public GameObject startButton;
    public GameObject HTPButton;
    public GameObject HTPPanel;
    public GameObject Fade;

    public Image fade;

    public float PanelY;

    private bool fadeTime;
    public float time;
    void Start()
    {
        fadeTime = false;
    }

    void Update()
    {
        if(fadeTime == true)
        {
            Color color = fade.color;
            color.a += Time.deltaTime;
            fade.color = color;

            time -= Time.deltaTime;

            if (time <= 0)
            {
                SceneManager.LoadScene("MainScene");
            }
        }
    }

    public void NextScene()
    {
        Fade.transform.position = new Vector2(640,360);
        fadeTime = true;
    }

    public void HowToPlay()
    {
        startButton.transform.DOMoveY(-50, 1);
        HTPButton.transform.DOMoveY(-50, 1);
        HTPPanel.transform.DOMoveY(PanelY, 1);

    }

    public void Back()
    {
        startButton.transform.DOMoveY(250, 1);
        HTPButton.transform.DOMoveY(130, 1);
        HTPPanel.transform.DOMoveY(-PanelY, 1);
    }

}
