using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShipMove : MonoBehaviour
{
    public GameObject followCam;

    public Rigidbody2D rb;

    public Slider hpbar;

    private ScoreManager score;

    public float accelPower = 1f;

    public float rotatePower = 1f;

    public float rotateAmount, speed, direction;

    public float maxhp = 50f;
    public float hp = 50f;

    public bool isShop;

    //Vector2 movement = new Vector2();

    public Vector3 mousePos { get; private set; }

    void Start()
    {
        score = FindObjectOfType<ScoreManager>();
        rb = GetComponent<Rigidbody2D>();

        isShop = false;

        hp = maxhp; 

        hpbar.value = hp / maxhp;
    }

    private void FixedUpdate()
    {
        /*
        float horizon = Input.GetAxis("Horizontal");

        rotateAmount = -Input.GetAxis("Horizontal");

        if(rb.rotation <= 90 && rb.rotation > 0)
        {
            movement.x = horizon;
            movement.y = horizon;
        }
        else if(rb.rotation <= 180 && rb.rotation > 0)
        {
            movement.x = -horizon;
            movement.y = horizon;
        }
        else if(rb.rotation <= 0 && rb.rotation >= -90)
        {
            movement.x = horizon;
            movement.y = -horizon;
        } 
        else if(rb.rotation <= 0 && rb.rotation >= -180)
        {
            movement.x = -horizon;
            movement.y = -horizon;
        }
        else if(rb.rotation > 180)
        {
            rb.rotation = -180;
        }
        else if (rb.rotation < -180)
        {
            rb.rotation = 180;
        }

        if(gameObject.transform.localPosition.y > 5)
        {
            Vector2 vec = gameObject.transform.localPosition;
            vec.y = 5;
            gameObject.transform.localPosition = vec;
            if(gameObject.transform.localPosition.y <= 5)
            {
                Vector2 vecx = gameObject.transform.localPosition;
                vecx.x = 0;
                gameObject.transform.localPosition = vecx;
            }
        }
        else if (gameObject.transform.localPosition.y < -5)
        {
            Vector2 vec = gameObject.transform.localPosition;
            vec.y = -5;
            gameObject.transform.localPosition = vec;
            if (gameObject.transform.localPosition.y >= -5)
            {
                Vector2 vecx = gameObject.transform.localPosition;
                vecx.x = 0;
                gameObject.transform.localPosition = vecx;
            }
        }

        else if(gameObject.transform.localPosition.x > 5)
        {
            Vector2 vec = gameObject.transform.localPosition;
            vec.x = 5;
            gameObject.transform.localPosition = vec;
            if (gameObject.transform.localPosition.x <= 5)
            {
                Vector2 vecx = gameObject.transform.localPosition;
                vecx.y = 0;
                gameObject.transform.localPosition = vecx;
            }
        }
        else if (gameObject.transform.localPosition.x < -5)
        {
            Vector2 vec = gameObject.transform.localPosition;
            vec.x = -5;
            gameObject.transform.localPosition = vec;
            if (gameObject.transform.localPosition.x >= -5)
            {
                Vector2 vecx = gameObject.transform.localPosition;
                vecx.y = 0;
                gameObject.transform.localPosition = vecx;
            }
        }
        if (isShop == false)
        {
            rb.rotation += rotateAmount * rotatePower;

            rb.velocity = movement * speed;
        }
        */

        //rotateAmount = -Input.GetAxis("Horizontal");

        
        if (isShop == false)
        {
            speed = Input.GetAxis("Vertical") * accelPower;
            Rotate();
        }
        //direction = Input.GetAxis("Horizontal") * accelPower;

        //direction = Mathf.Sign(Vector2.Dot(rb.velocity, rb.GetRelativeVector(Vector2.up)));
        rb.rotation += rotateAmount * rotatePower * rb.velocity.magnitude; // * direction;
        rb.AddRelativeForce(Vector2.up * speed);
        rb.AddRelativeForce(Vector2.right * direction);

        rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -3, 3), Mathf.Clamp(rb.velocity.y, -3, 3));
        
        //rb.AddRelativeForce(-Vector2.right * rb.velocity.magnitude * rotateAmount / 2);

        //followCam.transform.localRotation = gameObject.transform.localRotation;

        hpbar.value = hp / maxhp;

        if (hp <= 0)
        {
            SceneManager.LoadScene("EndScene");
        }

        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        float depth = Camera.main.farClipPlane;
        if (Physics.Raycast(camRay, out hit, depth))
        {
            mousePos = hit.point;
        }

        if(Input.GetKey(KeyCode.Space))
        {
            rb.velocity *= 0;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(mousePos, 0.5f);
    }

    void Rotate()
    {
        Vector3 target = mousePos;
        target.z = 0;
        Vector3 v = target - transform.position;

        float degree = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
        float rot = Mathf.LerpAngle(transform.eulerAngles.z, degree - 90, Time.deltaTime * 3);
        transform.eulerAngles = new Vector3(0, 0, rot);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "EnemyBullet")
        {
            Bullet bullet = collision.GetComponent<Bullet>();
            hp -= bullet.damage;

            collision.gameObject.SetActive(false);
        }
        else if(collision.gameObject.tag == "Gear")
        {
            score.gearValue++;
            collision.gameObject.SetActive(false);
        }
    }
}
