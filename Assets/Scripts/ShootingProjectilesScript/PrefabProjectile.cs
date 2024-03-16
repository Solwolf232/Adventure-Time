using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabProjectile : MonoBehaviour
{
    float CountTime = 3;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        CountTime -= Time.deltaTime;

        if (CountTime <= 0)
            Destroy(gameObject);
    }


   
}
