using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrindlyBullet : MonoBehaviour
{
    public float shootSpeed;
    void Update()
    {
        Shoot();
    }

    public void Shoot()
    {
        transform.Translate(Vector2.up * shootSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            gameObject.SetActive(false);
            collision.gameObject.SetActive(false);
        }

        if (collision.gameObject.CompareTag("Alien"))
        {
            collision.gameObject.GetComponent<Alien>().Kill();
            gameObject.SetActive(false); 
        }
    }
}
