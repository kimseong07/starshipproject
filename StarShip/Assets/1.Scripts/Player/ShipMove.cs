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

    DataController dataController;

    public Vector3 mousePos { get; private set; }

    void Start()
    {
        score = FindObjectOfType<ScoreManager>();
        rb = GetComponent<Rigidbody2D>();
        dataController = FindObjectOfType<DataController>();

        isShop = false;

        maxhp = dataController.gameData._maxhp;
        hp = dataController.gameData._hp;
        score.gearValue = dataController.gameData._gearValue;

        hpbar.value = hp / maxhp;
    }

    private void FixedUpdate()
    {
        if (isShop == false)
        {
            speed = Input.GetAxis("Vertical") * accelPower;
            Rotate();
        }
        
        rb.rotation += rotateAmount * rotatePower * rb.velocity.magnitude;
        rb.AddRelativeForce(Vector2.up * speed);
        rb.AddRelativeForce(Vector2.right * direction);

        rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -3, 3), Mathf.Clamp(rb.velocity.y, -3, 3));

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

            dataController.gameData._hp = hp;
            dataController.gameData._maxhp = maxhp;
        }
        else if(collision.gameObject.tag == "Gear")
        {
            score.gearValue++;

            dataController.gameData._gearValue = score.gearValue;
            
            collision.gameObject.SetActive(false);
        }
    }
}
