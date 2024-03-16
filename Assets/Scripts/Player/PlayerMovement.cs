using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [Header("Scripts")]
    private PlayerLogic PlayerLogicScript;
    private CutSceneMangaer CutSceneLevel;

    [Header("Public Variables")]
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public float coyoteTime = 0.2f;
    public float jumpBufferTime = 0.1f;

    [Header("Ground Check")]
    public Transform groundCheck;
    private bool isGrounded = false;

    [Header("Jumping Variables")]
    private bool isJumping;
    private float coyoteTimer;
    private float jumpBufferTimer;
    public string jumpButton = "Jump";


    [Header("MoveAndRun")]
    private float moveInput;
    private float moveSpeedIncreaseTimer = 0.5f;
    private float moveSpeedIncrease;


    [Header("Booleans")]
    public bool PlayerCanMove;


    [Header("Components")]
    private Rigidbody2D PlayerRb;
    private Transform playerTransform;


    [Header("Seriliezed Fields")]
    [SerializeField] private Animator PlayerAnimator;
    [SerializeField] private Transform ThrowAbleTransform;

 




    void Start()
    {

        PlayerCanMove=true;


        PlayerRb = GetComponent<Rigidbody2D>();
        PlayerLogicScript = GetComponent<PlayerLogic>();
        CutSceneLevel = FindObjectOfType<CutSceneMangaer>();
        playerTransform = GetComponent<Transform>();

        moveSpeedIncrease = moveSpeed;
        playerTransform = transform;
    }

    void Update()
    {
           bool CanPlay = !PlayerLogicScript.PlayerDead && !CutSceneLevel.IsFinished && PlayerCanMove;

            if (CanPlay)
            {
                SetAnimations();
                moveSpeedIncreaseTimer -= Time.deltaTime;
                isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, LayerMask.GetMask("Ground"));

                moveInput = Input.GetAxis("Horizontal");

                HandleInputs();

                bool isJumpButtonDown = Input.GetButtonDown(jumpButton);

                IncreaseMoveSpeed();
                JumpBuffering();
                JumpCoyoteTime();

            // Jumping logic

                bool CanJump = (jumpBufferTimer > 0 && coyoteTimer > 0) || (isGrounded && isJumpButtonDown);

                if (CanJump)
                {
                    isJumping = true;
                }



                // Jumping
            if (isJumping)
            {
                Jump();
            }

            }
            else
            {
                DontJump();
            }
        

    }


    public void HandleInputs()
    {
        if (moveInput < 0)
        {
            playerTransform.localScale = new Vector3(-1, 1, 1); // Flip to the left
            PlayerRb.velocity = new Vector2((moveInput * (moveSpeed + moveSpeedIncrease)), PlayerRb.velocity.y);


        }
        else if (moveInput > 0)
        {
            playerTransform.localScale = new Vector3(1, 1, 1); // Unflip to the right
            PlayerRb.velocity = new Vector2((moveInput * (moveSpeed + moveSpeedIncrease)), PlayerRb.velocity.y);
        }

        if (moveInput == 0)
        {

            moveSpeedIncrease = 0f;
            PlayerRb.velocity = new Vector2(0, PlayerRb.velocity.y);
        }
    }

    public void IncreaseMoveSpeed()
    {
        if (moveSpeedIncreaseTimer <= 0)
        {
            moveSpeedIncrease += 0.1f;
            moveSpeedIncreaseTimer = 0.5f;
        }
    }


    #region Jumping Logic
    public void Jump()
    {
        PlayerRb.velocity = new Vector2(PlayerRb.velocity.x, 0f); // Reset vertical velocity before jumping
        PlayerRb.AddForce((Vector2.up * jumpForce), ForceMode2D.Impulse);
        isJumping = false;
        coyoteTimer = 0;
    }
    public void DontJump()
    {
        PlayerRb.velocity = new Vector2(0, 0);
        PlayerAnimator.SetBool("IsJumping", false);
        PlayerAnimator.SetTrigger("IsIdle");
    }

    public void JumpBuffering()
    {
        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferTimer = jumpBufferTime;
        }
        else
        {
            jumpBufferTimer -= Time.deltaTime;
        }
    }

    public void JumpCoyoteTime()
    {
        if (isGrounded)
        {
            coyoteTimer = coyoteTime;
        }
        else
        {
            coyoteTimer -= Time.deltaTime;
        }
    }

    #endregion
    public void SetAnimations()
    {

        bool IsRunning = moveInput > 0 || moveInput < 0;
        bool IsJumpAble = !isGrounded && !PlayerLogicScript.PlayerDead;
        bool LevelFinsihed = CutSceneLevel.IsFinished;

        if (IsJumpAble)
            PlayerAnimator.SetBool("IsJumping", true);


        else
            PlayerAnimator.SetBool("IsJumping", false);


        if (!LevelFinsihed)
        {
            if (IsRunning)
                PlayerAnimator.SetTrigger("IsRunning");


            else
                PlayerAnimator.SetTrigger("IsIdle");

        }
    }

 }

