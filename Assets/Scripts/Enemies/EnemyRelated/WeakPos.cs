using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakPos : MonoBehaviour
{
    public float knockbackForce;
    public bool CanGetHit=true;
    private Rigidbody2D PlayerRb;

    public void Start()
    {
        PlayerRb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && transform.position.y > collision.gameObject.transform.position.y+0.3f)
        {
            CanGetHit = false;
            GameObject parentObject = collision.gameObject.transform.parent.gameObject;

            // Check if the parent GameObject has the EnemyMovement script
            EnemyMovement EnemyScript = parentObject.GetComponentInChildren<EnemyMovement>();

            // Calculate knockback direction based on the relative velocity
            Vector2 relativeVelocity = collision.relativeVelocity.normalized;
            Vector2 knockbackDirection = new Vector2(relativeVelocity.x, Mathf.Max(relativeVelocity.y, 0.45f)); // Only consider positive Y velocity

            // Apply knockback force in both X and Y directions
            Vector2 knockbackImpulse = knockbackDirection * knockbackForce;

            // Apply the knockback force to the player's Rigidbody
            PlayerRb.AddForce(knockbackImpulse, ForceMode2D.Impulse);

            EnemyScript.EnemyDie();
        }
    }
}
