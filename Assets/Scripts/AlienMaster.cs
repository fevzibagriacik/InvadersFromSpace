using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AlienMaster : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject motherShipPrefab;

    [SerializeField] private ObjectPool objectPool = null;

    private Vector3 hDistance = new Vector3(0.05f, 0, 0);
    private Vector3 vDistance = new Vector3 (0, 0.20f, 0);
    private Vector3 motherShipPosition = new Vector3(3.72f, 3.45f, 0);

    private const float MAX_X = 3.5f;
    private const float MIN_X = -3.5f;
    private const float MAX_MOVE_SPEED = 0.02f;
    private const float MOTHERSHIP_MIN = 15;
    private const float MOTHERSHIP_MAX = 60;

    private bool movingRight;

    private float moveTimer = 0.01f;
    private float moveTime = 0.003f;
    private float motherShipTimer = 1;

    private float shootTimer = 3f;
    private const float shootTime = 3f;

    public static List<GameObject> allAliens = new List<GameObject>();
    void Start()
    {
        foreach(GameObject go in GameObject.FindGameObjectsWithTag("Alien"))
        {
            allAliens.Add(go);
        }
    }

    void Update()
    {
        MoveTimer();

        ShootTimer();

        MotherShipTimer();
    }
    
    public void spawnMotherShip()
    {
        Instantiate(motherShipPrefab, motherShipPosition, Quaternion.identity);
        motherShipTimer = Random.Range(MOTHERSHIP_MIN, MOTHERSHIP_MAX);
    }

    public void MotherShipTimer()
    {
        if(motherShipTimer <= 0)
        {
            spawnMotherShip();
        }
        motherShipTimer -= Time.deltaTime;
    }

    public void MoveEnemies()
    {
        int hitMax = 0;

        if(allAliens.Count > 0 )
        {
            for (int i = 0; i < allAliens.Count; i++)
            {
                if (movingRight)
                {
                    allAliens[i].transform.position += hDistance;
                }
                else
                {
                    allAliens[i].transform.position -= hDistance;
                }

                if (allAliens[i].transform.position.x > MAX_X || allAliens[i].transform.position.x < MIN_X)
                {
                    hitMax++;
                }
            }

            if (hitMax > 0)
            {
                for (int i = 0; i < allAliens.Count; i++)
                {
                    allAliens[i].transform.position -= vDistance;
                }

                movingRight = !movingRight;
            }

            moveTimer = GetMoveSpeed();
        }
    }

    public void MoveTimer()
    {
        if(moveTimer <= 0)
        {
            MoveEnemies();
        }

        moveTimer -= Time.deltaTime;
    }

    public float GetMoveSpeed()
    {
        float f = allAliens.Count * moveTime;

        if(f < MAX_MOVE_SPEED)
        {
            return MAX_MOVE_SPEED;
        }
        else
        {
            return f;
        }
    }

    public void Shoot()
    {
        Vector2 pos = allAliens[Random.Range(0, allAliens.Count)].transform.position;

        GameObject obj = objectPool.GetPooledObject();
        obj.transform.position = pos;

        shootTimer = shootTime;
    }

    public void ShootTimer()
    {
        if(shootTimer <= 0)
        {
            Shoot();
        }
        shootTimer -= Time.deltaTime;
    }
}
