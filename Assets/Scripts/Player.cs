using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public ShipStates shipStates;

    public GameObject bulletPrefab;
    public GameObject frindlyBullet;

    private const float MIN_X = -3.6f;
    private const float MAX_X = 3.6f;

    //private float cooldown = 0.5f;
    //public float moveSpeed;

    private bool isShooting;

    [SerializeField] private ObjectPool poolObject = null;

    private Vector2 offScreenPos = new Vector2(0, -20f);
    private Vector2 startPos = new Vector2(0, -5f);

    private void Start()
    {
        shipStates.currentHealth = shipStates.maxHealth;
        shipStates.currentLifes = shipStates.maxLifes;
        transform.position = startPos;

        Debug.Log("currentLife " + shipStates.currentLifes);
        Debug.Log("currentHealth " + shipStates.currentHealth);
    }

    void Update()
    {
        BulletMove();

        ShootInput();
    }

    public void BulletMove()
    {
        if(Input.GetKey(KeyCode.D) && bulletPrefab.transform.position.x < MAX_X)
        {
            bulletPrefab.transform.Translate(Vector2.right * shipStates.shipSpeed * Time.deltaTime);
        }
        else if(Input.GetKey(KeyCode.A) && bulletPrefab.transform.position.x > MIN_X)
        {
            bulletPrefab.transform.Translate(Vector2.left * shipStates.shipSpeed * Time.deltaTime);
        }
    }

    public IEnumerator Shoot()
    {
        isShooting = true;
        //Instantiate(frindlyBullet, transform.position, Quaternion.identity);
        GameObject obj = poolObject.GetPooledObject();
        obj.transform.position = gameObject.transform.position;
        yield return new WaitForSeconds(shipStates.fireRate);
        isShooting = false;
    }

    public void ShootInput()
    {
        if (Input.GetKey(KeyCode.Space) && !isShooting)
        {
            StartCoroutine(Shoot());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            collision.gameObject.SetActive(false);
            TakeDamage();
        }
    }

    public void TakeDamage()
    {
        shipStates.currentHealth--;

        Debug.Log("Currenthealth: " + shipStates.currentHealth);

        Debug.Log("Currentlifes: " + shipStates.currentLifes);


        if (shipStates.currentHealth <= 0)
        {
            shipStates.currentLifes--;

            if(shipStates.currentLifes <= 0)
            {
                StartCoroutine(Rewpawn());
            }
            else
            {
                Debug.Log("Game over");

                //Debug.Log("Respawn");
            }
        }
    }

    private IEnumerator Rewpawn()
    {
        transform.position = offScreenPos;

        yield return new WaitForSeconds(2);

        shipStates.currentHealth = shipStates.maxHealth;

        transform.position = startPos;
    }
}
