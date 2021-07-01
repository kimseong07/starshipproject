using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public string[] bulletprefabs;
    public GameObject pos;

    public ObjectManager objectManager;

    public float Cdelay = 0.5f;
    private float delay;

    [Header("Audio clips")]
    public AudioClip fireSound;

    private AudioSource audioSource;

    DataController dataController;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        dataController = FindObjectOfType<DataController>();

        bulletprefabs = new string[] { "BulletPlayer" };
        delay = 0;

        Cdelay = dataController.gameData._cdelay;
    }
    void Update()
    {
        if (delay <= 0)
        {   
            if (Input.GetMouseButtonDown(0))
            {
                audioSource.clip = fireSound;
                audioSource.Play();

                GameObject bullet = objectManager.MakeObj(bulletprefabs[0]);
                bullet.transform.position = pos.transform.position;
                bullet.transform.rotation = pos.transform.rotation;

                delay = Cdelay;

                dataController.gameData._cdelay = Cdelay;
            }
        }

        delay -= Time.deltaTime;
    }
}
