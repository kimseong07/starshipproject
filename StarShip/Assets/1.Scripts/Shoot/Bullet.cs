using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 5.0f;
    public float lifeTime = 10.0f;
    public float cTime = 10.0f;

    private Rigidbody2D rigid;

    public float damage = 50f;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if (lifeTime <= 0)
        {
            gameObject.SetActive(false);
            lifeTime = cTime;
        }

        lifeTime = lifeTime - Time.deltaTime;

        rigid.velocity = transform.up * speed;
    }
}
