using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public GameObject enemyBullet;
    public float shootSpeed = 10f;
    void Update()
    {
        Shoot();
    }

    public void Shoot()
    {
        transform.Translate(Vector2.down * shootSpeed * Time.deltaTime);
    }


}
