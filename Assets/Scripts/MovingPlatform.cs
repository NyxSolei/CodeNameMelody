using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float moveSpeed = 3f; // speed of the platform
    public float moveDistance = 5f; // Distance the platform move from starting point
    public bool moveLeftFirst = true;

    private Vector2 startPos; // Starting position of the platform 
    private int direction = 1; // 1 for right, -1 for left


    void Start()
    {
        startPos = transform.position; // save the starting position

        if (moveLeftFirst)
        {
            direction = -1;
        }
    }

    void Update()
    {
        transform.Translate(Vector2.right * direction * moveSpeed * Time.deltaTime);

        if (Vector2.Distance(startPos, transform.position) >= moveDistance)
        {
            direction *= -1;
            startPos = transform.position;
        }
    }
    private void OnCollisionEnter2D(UnityEngine.Collision2D collision)
    {
        collision.transform.SetParent(transform);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.SetParent(null);
    }
}

