using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    public GameObject BackImg;

    private MeshRenderer render;

    public float speed;
    private Vector2 offset;

    public Rigidbody2D player;
    // Start is called before the first frame update
    void Start()
    {
        render = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        offset = player.transform.position * speed;

        render.material.mainTextureOffset = offset;

        BackImg.transform.position = player.transform.position;
    }
}
