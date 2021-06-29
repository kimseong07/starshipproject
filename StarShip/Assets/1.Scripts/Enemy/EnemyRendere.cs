using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRendere : MonoBehaviour
{
    public static readonly string[] staticDirections = { "Static N", "Static NW", "Static W", "Static SW", "Static S", "Static SE", "Static E", "Static NE" };
    public static readonly string[] runDirections = { "Run N", "Run NW", "Run W", "Run SW", "Run S", "Run SE", "Run E", "Run NE" };

    public int lastDirection;

    EnemyHealth enemyh;
    SpriteRenderer sprite;
    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        enemyh = FindObjectOfType<EnemyHealth>();
    }
    private void Update()
    {
        if(enemyh.isDie == true)
        {
            Color color = sprite.color;
            color.a = 0f;
            sprite.color = color;
        }
        else
        {
            Color color = sprite.color;
            color.a = 1f;
            sprite.color = color;
        }
    }
    public void SetDirection(Vector2 direction)
    {
        string[] directionArray = null;

        if (direction.magnitude < .01f)
        {
            directionArray = staticDirections;
        }
        else
        {
            directionArray = runDirections;
            lastDirection = DirectionToIndex(direction, 8);
        }
    }
    public static int DirectionToIndex(Vector2 dir, int sliceCount)
    {
        Vector2 normDir = dir.normalized;

        float step = 360f / sliceCount;

        float halfstep = step / 2;

        float angle = Vector2.SignedAngle(Vector2.up, normDir);
        angle += halfstep;

        if (angle < 0)
        {
            angle += 360;
        }

        float stepCount = angle / step;
        return Mathf.FloorToInt(stepCount);
    }
}
