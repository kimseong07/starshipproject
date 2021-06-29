using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public float rotateSpeed = 6f;

    private ShipMove shipMove;

    // Start is called before the first frame update
    void Start()
    {
        shipMove = FindObjectOfType<ShipMove>();
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        Vector3 target = shipMove.mousePos;
        target.z = 0;
        Vector3 v = target - transform.position;

        float degree = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
        float rot = Mathf.LerpAngle(transform.eulerAngles.z, degree - 90, Time.deltaTime * rotateSpeed);
        transform.eulerAngles = new Vector3(0, 0, rot);
    }
}
