using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearMove : MonoBehaviour
{
    ShipMove player;
    Rigidbody2D rbody;

    public float speed = 5.0f;

    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<ShipMove>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 currentPos = rbody.position;

        Vector2 inputVector = new Vector2(player.transform.position.x - currentPos.x, player.transform.position.y - currentPos.y);

        inputVector = Vector2.ClampMagnitude(inputVector, 1);

        Vector2 movement = inputVector * speed;
        Vector2 newPos = currentPos + movement * Time.fixedDeltaTime;
        rbody.MovePosition(newPos);
    }
}
