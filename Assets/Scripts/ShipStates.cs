using UnityEngine;

[System.Serializable]
public class ShipStates
{
    [Range(1, 5)]
    public int maxHealth = 3;
    [HideInInspector]
    public int currentHealth;
    [HideInInspector]
    public int maxLifes;
    [HideInInspector]
    public int currentLifes = 3;

    public float shipSpeed;
    public float fireRate;
}
