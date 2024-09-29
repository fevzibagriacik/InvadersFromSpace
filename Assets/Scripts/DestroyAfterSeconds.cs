using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterSeconds : MonoBehaviour
{
    public float bulletDeactivePosition;
    private void Update()
    {
        Deactive();
    }

    public void Deactive()
    {
        if(transform.position.y > bulletDeactivePosition || transform.position.y < -bulletDeactivePosition)
        {
            gameObject.SetActive(false);
        }
    }
}
