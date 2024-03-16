using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerLogic : MonoBehaviour
{
    [Header("Scripts")]
    private Unit PlayerUnitScript;
    private CutSceneMangaer CutSceneLevel;
    private EnemyMovement EnemyScript;

    [Header("Serilized Fields")]
    [SerializeField] List<GameObject> Hearts;
    [SerializeField] Animator UIAnimator;
    [SerializeField] GameObject CheckPointPos;


    [Header("Components")]
    private Rigidbody2D PlayerRb;
    private Animator PlayerAnimator;
   
    [Header("Publics")]
    public float knockbackForce;
    public bool PlayerDead;
    public int ShroomCount = 0;

    [Header("Hearts Management")]
    private int HeartsCount = 0;
    private int currhealth;

    [Header("Booleans")]
    private bool BookCollected;
    public bool CheckPointHit;
    public bool IsGotHit;

    [Header("Essentials")]
    private float CutsceneTimer = 0.5f;
    private Vector2 CollisionCheck;


    public void Start()
    {

        BookShouldBeCollected(); // Checking If Current Scene in Base Level So he Player Can Pass the Portal without taking any Books;

        PlayerUnitScript = GetComponent<Unit>();
        PlayerAnimator = GetComponentInChildren<Animator>();
        PlayerRb = GetComponent<Rigidbody2D>();
        CutSceneLevel = FindObjectOfType<CutSceneMangaer>();

        currhealth = PlayerUnitScript.currHealth;

    }
    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Enemy"))
        {
            bool PlayerIsLowerThenEnemy = transform.position.y <= collision.gameObject.transform.position.y;


            if (PlayerIsLowerThenEnemy)
            {
                GameObject parentObject = collision.gameObject.transform.parent.gameObject;
                EnemyScript = parentObject.GetComponentInChildren<EnemyMovement>();
                if (!EnemyScript.isDead && !PlayerDead)
                {
                        CollisionCheck = collision.gameObject.transform.position;
                        KnockBack();
                        TakeDamage();
                }
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Book"))
        {
            Destroy(collision.gameObject);
            BookCollected = true;
        }

        if (BookCollected && SceneManager.GetActiveScene().buildIndex == 3)
        {
            UIAnimator.Play("UIDead");
            StartCoroutine(BossLevel());
        }

        if (collision.gameObject.CompareTag("Finish") && BookCollected)
        {
            CutSceneLevel.IsFinished = true;
        }

        if (collision.gameObject.CompareTag("CheckPoint"))
        {
            Animator SignAnimator = collision.gameObject.GetComponent<Animator>();
            CheckPointHit = true;
            SignAnimator.Play("CheckPointReached");
           
        }
    }
    public void TakeDamage()
    {
        StartCoroutine(GettingHitTimer());
        currhealth -= 1;
        PlayerAnimator.SetTrigger("IsGotHit");
        Hearts[HeartsCount].SetActive(false);
        HeartsCount += 1;
    }

    public void BookShouldBeCollected()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            BookCollected = true;
        }
    }

    public void KnockBack()
    {
        Vector3 collisionDirection = new Vector3(CollisionCheck.x, CollisionCheck.y, this.transform.position.z);


        //Knocking The Player In the Opposite direction of the enemy 
        Vector2 knockbackDirection = (transform.position - collisionDirection).normalized;

        Vector2 knockbackImpulse = (knockbackForce * knockbackDirection);
        PlayerRb.AddForce(knockbackImpulse, ForceMode2D.Impulse);



        // KnockingBack the Player Upwards
        Vector2 knockbackDirection2 = Vector2.up;

        Vector2 knockbackImpulse2 = (knockbackForce * knockbackDirection2);
        PlayerRb.AddForce(knockbackImpulse2, ForceMode2D.Impulse);
    }


   
  

    public void Die()
    {

        bool PlayerIsDead = currhealth <= 0 || PlayerDead;

        if (PlayerIsDead)
        {
            
            PlayerAnimator.Play("FinnDie");
            UIAnimator.Play("UIDead");
            PlayerDead = true;
            if (CutsceneTimer <= 0)
            {
                if (CheckPointHit)
                {
                    ResetAll();
                }
                else
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
           
        }
    }


    public IEnumerator GettingHitTimer()
    {
        IsGotHit = true;
        yield return new WaitForSeconds(0.5f);
        IsGotHit = false;
    }
   

    public void Update()
    {
        bool PlayerFellOffWorld = transform.position.y < -15f;

        if (currhealth <= 0)
        {
            Die();
            CutsceneTimer -= Time.deltaTime;
        }


        if (PlayerFellOffWorld)
        {
            if (CheckPointHit)
            {
                ResetAll();
            }

            else
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    
    public void ResetAll()
    {
        UIAnimator.Play("UIDead");

        gameObject.transform.position = CheckPointPos.transform.position;

        currhealth = 3;
        Hearts[0].SetActive(true);
        Hearts[1].SetActive(true);
        Hearts[2].SetActive(true);
        PlayerDead = false;
        HeartsCount = 0;
        PlayerAnimator.Play("FinnIdle");
    }


    public IEnumerator BossLevel()
    {
        yield return new WaitForSeconds(3f);
        int BossLevel = 4;
        SceneManager.LoadScene(BossLevel);

    }
}
