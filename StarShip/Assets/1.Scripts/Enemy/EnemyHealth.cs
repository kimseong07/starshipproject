using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum State
{
    Idle,
    Chase,
    Die
}
public class EnemyHealth : MonoBehaviour
{
    RepairShip repShip;
    ShipMove player;
    Rigidbody2D rbody;
    BoxCollider2D box;

    public State state = State.Idle;

    public GameObject ERend;

    public string[] bullet;

    int dir = 0;

    public string[] gear;

    EnemyRendere enemyRenderer;

    public float movementSpeed = 1f;
    public float maxHp = 50f;
    public float hp = 50f;

    public float sightRange;
    public bool bPlayerInSightRange;

    public GameObject explosionEffect;
    public GameObject explosionEffect2;
    public GameObject explosionEffect3;

    private float delay = 1.5f;

    private ObjectManager objectManager;

    Spawner spawn;
    DataController dataController;

    public bool isDie;

    private void Start()
    {
        bullet = new string[] { "BulletEnemy" };
        gear = new string[] { "Gear" };
        player = FindObjectOfType<ShipMove>();
        repShip = FindObjectOfType<RepairShip>();
        spawn = FindObjectOfType<Spawner>();
        dataController = FindObjectOfType<DataController>();

        rbody = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
        enemyRenderer = GetComponentInChildren<EnemyRendere>();

        objectManager = FindObjectOfType<ObjectManager>();

        state = State.Idle;

        delay = 1.5f;
    }

    private void FixedUpdate()
    {
        bPlayerInSightRange = Physics.CheckSphere(transform.position, sightRange);
        
        if(bPlayerInSightRange)
        {
            state = State.Chase;
        }

        if (hp <= 0)
        {
            GameObject gir = objectManager.MakeObj(gear[0]);
            gir.transform.position = gameObject.transform.position;

            state = State.Die;
        }

        if (isDie == false)
        {
            if (state == State.Idle)
            {
                Idle();
            }

            if (state == State.Chase)
            {
                Chase();
            }
        }

        if (state == State.Die)
        {
            EnemyDie();
            box.offset = new Vector2(1280,720);
            isDie = true;
        }

        switch (enemyRenderer.lastDirection)
        {
            case 0:
                dir = 0;
                break;
            case 1:
                dir = 45;
                break;
            case 2:
                dir = 90;
                break;
            case 3:
                dir = 135;
                break;
            case 4:
                dir = 180;
                break;
            case 5:
                dir = 225;
                break;
            case 6:
                dir = 270;
                break;
            case 7:
                dir = 315;
                break;
            default:
                break;
        }

        ERend.transform.rotation = Quaternion.Euler(0, 0, dir);

        delay -= Time.deltaTime;
    }

    void EnemyDie()
    {
        spawn.enemyCount--;
        spawn.maxEnemy--;

        dataController.gameData._enemyValue = spawn.maxEnemy;

        hp = maxHp;

        state = State.Idle;

        int random = Random.Range(0, 2);

        switch (random)
        {
            case 0:
                Instantiate(explosionEffect, transform);
                break;
            case 1:
                Instantiate(explosionEffect2, transform);
                break;
            case 2:
                Instantiate(explosionEffect3, transform);
                break;
            default:
                break;
        }

        

        StartCoroutine(setTime());

    }

    private void Attack()
    {
        if (delay < 0)
        {
            GameObject bul = objectManager.MakeObj(bullet[0]);
            bul.transform.rotation = Quaternion.Euler(0, 0, dir);
            bul.transform.position = transform.position;

            delay = 1.5f;
        }

        
    }
    private void Chase()
    {
        Vector2 currentPos = rbody.position;

        Vector2 inputVector = new Vector2(player.transform.position.x - currentPos.x, player.transform.position.y - currentPos.y);

        inputVector = Vector2.ClampMagnitude(inputVector, 1);

        Vector2 movement = inputVector * movementSpeed;
        Vector2 newPos = currentPos + movement * Time.fixedDeltaTime;
        enemyRenderer.SetDirection(movement);
        rbody.MovePosition(newPos);

        Attack();
    }

    private void Idle()
    {
        Vector2 currentPos = rbody.position;

        Vector2 inputVector = new Vector2(repShip.transform.position.x - currentPos.x, repShip.transform.position.y - currentPos.y);

        inputVector = Vector2.ClampMagnitude(inputVector, 1);

        Vector2 movement = inputVector * movementSpeed;
        Vector2 newPos = currentPos + movement * Time.fixedDeltaTime;
        enemyRenderer.SetDirection(movement);
        rbody.MovePosition(newPos);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Bullet bullet = collision.GetComponent<Bullet>();
            hp -= bullet.damage;

            collision.gameObject.SetActive(false);
        }

    }

    IEnumerator setTime()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
        isDie = false;
        box.offset = new Vector2(0, 1.3f);
    }
}
