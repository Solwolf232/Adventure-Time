using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowAble : MonoBehaviour
{
    private ColliderDetector PrizesDector;

    public GameObject projectilePrefab; // The prefab of the projectile you want to shoot
    public Transform spawnPoint; // The point where the projectile will be spawned
    private Transform Player;

    private float ThrowTimer=0.2f;

    public void Start()
    {
        Player = GetComponent<Transform>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.GetComponent<ColliderDetector>() != null)
        {
            PrizesDector = collision.gameObject.GetComponent<ColliderDetector>();
        }

    }

    void Update()
    {
        ThrowTimer -= Time.deltaTime;
        // Check if the "L" key is pressed
        if (PrizesDector != null)
        {
            if (PrizesDector.CanShootStars)
            {
                if (Input.GetKeyDown(KeyCode.L))
                {

                    ShootProjectile();
                }
            }
              
            
        }
        
    }

    void ShootProjectile()
    {
        Vector2 direction = (Player.position - spawnPoint.position).normalized;

        // Flip the direction vector to shoot in the correct direction
        direction *= -1f;

        // Instantiate a new projectile at the spawn point
        GameObject newProjectile = Instantiate(projectilePrefab, spawnPoint.position, Quaternion.identity);

        // Add force to the projectile in the calculated direction
        Rigidbody2D rb = newProjectile.GetComponent<Rigidbody2D>();
        rb.AddForce(direction * 20f, ForceMode2D.Impulse);
    }


}
