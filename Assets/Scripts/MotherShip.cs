using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MotherShip : MonoBehaviour
{
    public int scoreValue;

    private const float MAX_LEFT = -5f;
    private float speed =5;

    void Update()
    {
        Move();
    }

    public void Move()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        if(transform.position.x <= MAX_LEFT)
        {
            Destroy(gameObject);
        }
    }
}
