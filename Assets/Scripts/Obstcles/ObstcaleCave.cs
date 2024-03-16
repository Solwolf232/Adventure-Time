using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstcaleCave : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody2D ObstcaleRb;

    [Header("Particle Effects")]
    public GameObject VfxSpawnPoint;
    public GameObject Vfx;
    
    [Header("SettingSpeed")]
    public float fallSpeed;
   
    public void Start()
    {
        ObstcaleRb = GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        bool ObstcaleShouldFall = ObstcaleRb.constraints != RigidbodyConstraints2D.FreezePositionY;

        if (ObstcaleShouldFall)
        {
            ObstcaleRb.velocity = new Vector2(0f, -fallSpeed);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SpawnParticles();
            
            ObstcaleRb.constraints = RigidbodyConstraints2D.None;

            ObstcaleRb.WakeUp();
        }
    }
    
    public void SpawnParticles()
    {
        Instantiate(Vfx, VfxSpawnPoint.transform.position, Quaternion.identity);
    }
}
