using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using DG.Tweening;

public class RepairShip : MonoBehaviour
{
    public float hp;

    public float rotateSpeed;

    public CinemachineVirtualCamera cvCam;

    public CinemachineCameraOffset cCm;

    public GameObject followCam;

    public GameObject player;

    public GameObject canvas;

    public GameObject pausePanel;

    public Transform pop;

    public Transform upgradePop;
    
    Sequence seq1, seq2, seq3, seq4;

    ShipMove ship;

    DataController dataController;

    private bool gamePause;

    private void Start()
    {
        ship = FindObjectOfType<ShipMove>();
        dataController = FindObjectOfType<DataController>();

        seq1.Append(pop.DOLocalMoveX(960, 1f));
        seq3.Append(upgradePop.DOLocalMoveX(960, 1f));

        hp = dataController.gameData._repairHp;

        gamePause = false;
    }

    void Update()
    {
        float z = rotateSpeed * Time.deltaTime;
        transform.Rotate(new Vector3(0, 0, z));

        if(hp <= 0)
        {
            this.gameObject.SetActive(false);
        }

        if(gamePause)
        {
            Time.timeScale = 0;
            gamePause = true;
            return;
        }
        else if(!gamePause)
        {
            Time.timeScale = 1;
            gamePause = false;
            return;
        }
    }

    public void Shop()
    {
        ship.isShop = true;
        ship.gameObject.transform.position = new Vector3(0, 0, 0);
        ship.gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
        ship.rb.velocity *= 0;
        ship.speed *= 0;

        DOTween.To(() => cvCam.m_Lens.OrthographicSize, value => cvCam.m_Lens.OrthographicSize = value, 1f, 1f);
        DOTween.To(() => cCm.m_Offset.x, value => cCm.m_Offset.x = value, 1f, 1f);

        OpenPopup();
    }

    public void ExitShop()
    {
        ship.isShop = false;
        ship.gameObject.transform.position = new Vector3(0, 5, 0);
        ship.gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);

        ClosePopup();
    }

    public void OpenPopup()
    {
        pop.localScale = Vector3.zero;
        pop.gameObject.SetActive(true);

        if (seq1 != null) seq1.Kill();
        if (seq2 != null) seq2.Kill();

        seq1 = DOTween.Sequence();
        seq1.Append(pop.DOScale(new Vector3(1f, 1f, 1f), 0f));
        seq1.Append(pop.DOLocalMoveX(320, 1f));
    }

    public void ClosePopup()
    {
        if (seq1 != null) seq1.Kill();
        seq1 = DOTween.Sequence();
        seq1.Append(pop.DOLocalMoveX(960, 1f));
        seq1.AppendCallback(() => { pop.gameObject.SetActive(false); });
    }

    public void CloseUpgrade()
    {
        if (seq3 != null) seq3.Kill();
        seq3 = DOTween.Sequence();
        seq3.Append(upgradePop.DOLocalMoveX(960, 1f));
        seq3.AppendCallback(() => { upgradePop.gameObject.SetActive(false); });
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyBullet")
        {
            Bullet bullet = collision.GetComponent<Bullet>();
            hp -= bullet.damage;

            collision.gameObject.SetActive(false);

            dataController.gameData._repairHp = hp;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            DOTween.To(() => cvCam.m_Lens.OrthographicSize, value => cvCam.m_Lens.OrthographicSize = value, 5f, 1f);
            DOTween.To(() => cCm.m_Offset.x, value => cCm.m_Offset.x = value, 0f, 1f);

            followCam.transform.localRotation = Quaternion.Euler(0, 0, 0);

            ClosePopup();
            CloseUpgrade();
        }
    }

    public void Upgrade()
    {
        if (seq1 != null) seq1.Kill();
        seq1 = DOTween.Sequence();
        seq1.Append(pop.DOLocalMoveX(960, 1f));
        seq1.AppendCallback(() => { pop.gameObject.SetActive(false); });

        upgradePop.localScale = Vector3.zero;
        upgradePop.gameObject.SetActive(true);

        StartCoroutine("OpenUpgrade");
    }

    public void Back()
    {
        if (seq3 != null) seq3.Kill();
        seq3 = DOTween.Sequence();
        seq3.Append(upgradePop.DOLocalMoveX(960, 1f));
        seq3.AppendCallback(() => { upgradePop.gameObject.SetActive(false); });

        StartCoroutine("OpenPop");
    }

    public void Pause()
    {
        gamePause = true;
        pausePanel.SetActive(true);
    }

    public void End()
    {
        Application.Quit();
    }
    public void Play()
    {
        gamePause = false;
        pausePanel.SetActive(false);
    }

    IEnumerator OpenUpgrade()
    {
        yield return new WaitForSeconds(1f);

        upgradePop.localScale = Vector3.zero;
        upgradePop.gameObject.SetActive(true);

        if (seq3 != null) seq3.Kill();
        if (seq3 != null) seq4.Kill();

        seq3 = DOTween.Sequence();
        seq3.Append(upgradePop.DOScale(new Vector3(1f, 1f, 1f), 0f));
        seq3.Append(upgradePop.DOLocalMoveX(320, 1f));

    }

    IEnumerator OpenPop()
    {
        yield return new WaitForSeconds(1f);

        OpenPopup();
    }


}
