using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBird : MonoBehaviour
{
   public GameObject SpawnPoint;
   public GameObject BirdPrefab;

    private int count = 1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && count==1)
        {
            count++;
            Instantiate(BirdPrefab, SpawnPoint.transform.position, Quaternion.identity);
        }
    }
}
