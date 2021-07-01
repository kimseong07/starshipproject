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
    public GameObject HTPPanel2;
    public GameObject Fade;

    public Image fade;

    public float PanelY;
    public float PanelX;

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
        Fade.transform.position = new Vector2(700,320);
        fadeTime = true;
    }

    public void HowToPlay()
    {
        startButton.transform.DOLocalMoveY(-550, 1);
        HTPButton.transform.DOLocalMoveY(-550, 1);
        HTPPanel.transform.DOLocalMoveY(PanelY, 1);

    }
    public void BeforEx()
    {
        HTPPanel.transform.DOLocalMoveX(0, 1);
    }
    public void NextEx()
    {
        HTPPanel.transform.DOLocalMoveX(-1290,1);
    }

    public void Back()
    {
        startButton.transform.DOLocalMoveY(-150, 1);
        HTPButton.transform.DOLocalMoveY(-250, 1);
        HTPPanel.transform.DOLocalMoveY(-1080, 1);
    }

}
