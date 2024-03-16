using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountScript : MonoBehaviour
{
    [Header("Scripts")]
    private PlayerMovement PlayerMovementScript;

    [Header("Components")]
    private Rigidbody2D MountRb;
    private Transform MountTransform;
    private CapsuleCollider2D PlayerCollider;
    private Animator EnemyAnimator;
    private BoxCollider2D MountBoxCollider;

    [Header("GroundCheck")]
    public Transform groundCheck;
    bool IsGrounded;

    [Header("Setters")]
    public float MountSpeed;
    private float moveInput;
    public float MountJumpForce;

    [Header("Booleans")]
    private bool PlayerOnMount;
    private bool DidntHitCheckPoint;

    [Header("Jumping")]
    private float jumpBufferTimer;
    public float jumpBufferTime = 0.1f;

    [Header("Jumping Mechanichs")]
    private float coyoteTimer;
    public float coyoteTime = 0.2f;


    [Header("Offsets")]
    private Vector3 playerMountOffset;
    public float yOffset = 0.5f;



    private void OnCollisionEnter2D(Collision2D collision)
    {
        bool PlayerShouldBeOnMount = collision.gameObject.CompareTag("Player") && !DidntHitCheckPoint;

        if (PlayerShouldBeOnMount)
        {
            bool PlayerHasController = collision.gameObject.GetComponent<PlayerMovement>() != null;

            if (PlayerHasController)
            {
                PlayerMovementScript = collision.gameObject.GetComponent<PlayerMovement>();
                GetComponents();
            }
            PlayerIsOnMount();

            CalculateOffset();

            // Apply a small vertical offset to the player's position to prevent sticking inside the collider
        }

        bool TouchedEnemy = (collision.gameObject.CompareTag("Enemy"));

        if (TouchedEnemy)
        {
            EnemyMovement EnemyScript = collision.gameObject.GetComponent<EnemyMovement>();
           if(EnemyScript != null)
            {
                EnemyScript.EnemyDie();
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool HittedCheckPoint = collision.gameObject.CompareTag("CheckPoint");

        if (HittedCheckPoint)
        {
            GetOffMount();
        }
    }

    void Start()
    {
        MountRb = GetComponent<Rigidbody2D>();
        MountTransform = GetComponent<Transform>();
        MountBoxCollider = GetComponent<BoxCollider2D>();
        EnemyAnimator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (PlayerOnMount && !DidntHitCheckPoint)
        {
            PlacePlayerOnMount();

            IsGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, LayerMask.GetMask("Ground"));

            moveInput = 1;

            MountRb.velocity = new Vector2((moveInput * MountSpeed), MountRb.velocity.y);

            HandleInputs();

            JumpBuffering();

            CoytoeTime();

            // Jumping logic
            JumpingLogic();

            // Apply the offset to the player's position
            PlayerMovementScript.transform.position = MountBoxCollider.transform.position + playerMountOffset;
        }

        else if(!PlayerOnMount && DidntHitCheckPoint)
        {
            MountChilling();
        }
    }

    public void PlayerIsOnMount()
    {
        PlayerMovementScript.PlayerCanMove = false;
        PlayerOnMount = true;
    }

    public void CalculateOffset()
    {
        Vector3 mountSize = MountBoxCollider.size;
        Vector3 playerSize = PlayerMovementScript.GetComponent<CapsuleCollider2D>().size;

        playerMountOffset = new Vector3(0, mountSize.y / 2 + playerSize.y / 2 + yOffset, 0);
    }
    public void GetOffMount()
    {
        DidntHitCheckPoint = true;
        PlayerMovementScript.PlayerCanMove = true;
        PlayerCollider.enabled = true;
        PlayerOnMount = false;
        moveInput = 0f;
        Renderer MountSprite = GetComponentInChildren<Renderer>();
        MountSprite.sortingOrder = 99;
    }

    public void GetComponents()
    {
        PlayerCollider = PlayerMovementScript.GetComponent<CapsuleCollider2D>();
        MountBoxCollider = GetComponent<BoxCollider2D>(); // Move this here to avoid frequent calls
    }

    public void HandleInputs()
    {
        if (moveInput > 0)
        {
            MountTransform.localScale = new Vector3(-1, 1, 1); // Unflip to the right
            PlayerMovementScript.transform.localScale = new Vector3(1, 1, 1);
            EnemyAnimator.Play("EnemyAttack");
        }

        if (moveInput == 0)
        {
            MountRb.velocity = new Vector2(0, MountRb.velocity.y);
            EnemyAnimator.Play("EnemyIdle");
        }
    }

    public void JumpBuffering()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            jumpBufferTimer = jumpBufferTime;
        }
        else
        {
            jumpBufferTimer -= Time.deltaTime;
        }

    }

    public void CoytoeTime()
    {
        if (IsGrounded)
        {
            coyoteTimer = coyoteTime;
        }
        else
        {
            coyoteTimer -= Time.deltaTime;
        }
    }

    public void JumpingLogic()
    {
        if ((jumpBufferTimer > 0 || coyoteTimer > 0) && Input.GetKey(KeyCode.Space))
        {
            MountRb.velocity = new Vector2(MountRb.velocity.x, 0f); // Reset vertical velocity before jumping
            MountRb.AddForce(Vector2.up * MountJumpForce, ForceMode2D.Impulse);
        }
    }

    public void PlacePlayerOnMount()
    {
        PlayerCollider.enabled = false;
        PlayerMovementScript.transform.position = Vector3.Lerp(PlayerMovementScript.transform.position, MountBoxCollider.transform.position + playerMountOffset, Time.fixedDeltaTime * 10f);
    }

    public void MountChilling()
    {
        moveInput = 0f;
        MountRb.velocity = new Vector2(0, MountRb.velocity.y);
        EnemyAnimator.Play("EnemyIdle");
        MountRb.isKinematic = true;
        CapsuleCollider2D MountCollider = GetComponent<CapsuleCollider2D>();
        MountCollider.enabled = false;
        MountBoxCollider.enabled = false;
    }
}