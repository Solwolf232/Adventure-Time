using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Scripts")]
    private PlayerLogic PlayerLogicScript;

    [Header("Particle System")]
    [SerializeField] GameObject Vfx;
    [SerializeField] GameObject VfxSpawnPoint;

    [Header("Animators")]
    [SerializeField] Animator EnemyAnimator;
    [SerializeField] Animator ReactionAnim;
  

    [Header("PointsEnemyGoesTo")]
    public Transform pointA;
    public Transform pointB;
    private Vector3 targetPosition;

    [Header("Rotation")]
    private float direction = 1.0f; // 1.0f for moving right, -1.0f for moving left
    private bool SeenPlayer; // Apply AI if Enemy Saw The Player


    [Header("Booleans")]
    public bool isDead;

    [Header("Movement")]
    public float movementSpeed = 2.0f; // Adjust this to control the speed of the enemy.

    [Header("Compenents")]
    private Transform playerTransform; // Store a reference to the player's transform


    void Start()
    {
        targetPosition = pointA.position; // Start by moving towards pointA

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; // Find the player by tag
        PlayerLogicScript = FindAnyObjectByType<PlayerLogic>();
    }

    void Update()
    {
        EnemyLogic();
    }


    

   
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            EnemyDie();
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            SeenPlayer = true;
        }

        if (collision.gameObject.CompareTag("Boundrie") && SeenPlayer)
        {
            bool playerIsOnRight = playerTransform.position.x > transform.position.x;

            SeenPlayer = false;

            targetPosition= pointA.position;

            FlipEnemy(!playerIsOnRight);

            PlayAnimations();
        }

    }

    private void EnemyLogic()
    {
        if (!isDead)
        {
            bool playerIsOnRight = playerTransform.position.x > transform.position.x;

            if (SeenPlayer)
            {

                MoveTowardsPlayer();

                bool ShouldFlipEnemy = (playerIsOnRight && direction < 0) || (!playerIsOnRight && direction > 0);

                if (ShouldFlipEnemy)
                {
                    FlipEnemy(playerIsOnRight); // Flip the enemy based on player's position
                }

                PlayAnimations();

            }

            else if (!SeenPlayer)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, movementSpeed * Time.deltaTime); //Move Towards Target Pos

            }

            // Check if we've reached the target position

            bool ReachedTarget = Vector3.Distance(transform.position, targetPosition) < 0.1f;

            if (ReachedTarget)
            {
                // Change direction and target based on current direction
                ChangeDirection();
            }
        }

        CheckWhatEnemyDied();

    }

    void FlipEnemy(bool isFacingLeft)
    {
        Vector3 newScale = transform.localScale;
        newScale.x = isFacingLeft ? -1 : 1;
        transform.localScale = newScale;
        
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointA.position, 0.5f);

        Gizmos.DrawWireSphere(pointB.position, 0.5f);

        Gizmos.DrawLine(pointA.position, pointB.position);
    }

    public void EnemyDie()
    {
        if(PlayerLogicScript != null)
        {
            PlayerLogicScript.ShroomCount++;
        }

        EnemyAnimator.SetTrigger("IsHit");
        isDead = true;
        EnemyAnimator.SetBool("IsDead", true);
        Destroy(gameObject, 1.1f);
    }
    

    public void ChangeDirection()
    {
        if (direction > 0.0f)
        {
            direction = -1.0f;
            targetPosition = pointA.position;
            FlipEnemy(false); // Flip the enemy to face left
        }
        else
        {
            direction = 1.0f;
            targetPosition = pointB.position;
            FlipEnemy(true); // Flip the enemy to face right
        }
    }

    public void CheckWhatEnemyDied()
    {
        bool EvilMushroomDied = isDead && this.name.Equals("EvilMushroomParent");
        bool NormalMushroomDied = isDead && this.name.Equals("EnemyParent");

        if (EvilMushroomDied)
        {
            SpawnParticles();
            this.name = "Died";
        }
        if (NormalMushroomDied)
        {
            SpawnParticles();
            this.name = "Died";
        }

    }

    public void MoveTowardsPlayer()
    {
        float increasedSpeed = movementSpeed * 1.5f; //Increase The Speed of the Enemy

        targetPosition.x = playerTransform.position.x;
        targetPosition.y = transform.position.y;// Update the target position to the player's position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, increasedSpeed * Time.deltaTime);
    }


    public void PlayAnimations()
    {
        EnemyAnimator.Play("EnemyAttack");
        ReactionAnim.Play("ReactionAnimation");
    }

    public void SpawnParticles()
    {
        Instantiate(Vfx, VfxSpawnPoint.transform.position, Quaternion.identity);
    }
}



