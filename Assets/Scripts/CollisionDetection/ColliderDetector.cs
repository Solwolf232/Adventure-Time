using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ColliderDetector : MonoBehaviour
{
    #region Scripts
    [Header("PrizesCollision")]
    private LuckyBlockLogic LuckyBlockScript;
    [SerializeField] PlayerMovement PlayerScript;
    #endregion


    public bool DoubeCoinsIsOn=true;
    public bool CanCollect=true;
    public bool CanShootStars;
    private Rigidbody2D ColliderRb;



    public void Start()
    {
        LuckyBlockScript = GetComponentInParent<LuckyBlockLogic>();
        ColliderRb = GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (CanCollect)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                switch (LuckyBlockScript.RandomPrize)
                {
                    case 0:
                        CanCollect = false;
                        Debug.Log("Star");
                        StartCoroutine(ShootingStars());
                        Destroy(LuckyBlockScript.Prizes[0]);
                        Destroy(gameObject, 8.1f);
                        break;
                    case 1:
                        CanCollect = false;
                        Debug.Log("Doube");
                        StartCoroutine(DoubleCoins());
                        Destroy(LuckyBlockScript.Prizes[1]);
                        Destroy(gameObject, 15.2f);

                        break;
                    case 2:
                        CanCollect = false;
                        Debug.Log("Sandals");
                        StartCoroutine(FasterPrize());
                        Destroy(LuckyBlockScript.Prizes[2]);
                        Destroy(gameObject, 6.65f);
                        break;
                }
            }
        }
       

    }


 

    public void Update()
    {
        if (LuckyBlockScript.ShouldMove)
        {
            Invoke("StartMoving", 0.85f);
        }


    }

    private IEnumerator FasterPrize()
    {
        float NormalSpeed = PlayerScript.moveSpeed;
        Debug.Log("Speed");
        PlayerScript.moveSpeed += 2.5f;


        yield return new WaitForSecondsRealtime(6.5f);

        PlayerScript.moveSpeed = NormalSpeed;
        Debug.Log("NoSpeed");
    }

    public void StartMoving()
    {
        transform.position = Vector3.MoveTowards(transform.position, PlayerScript.transform.position, 3 * Time.deltaTime);
    }

    private IEnumerator DoubleCoins()
    {
        DoubeCoinsIsOn = true;
        yield return new WaitForSecondsRealtime(15F);
        DoubeCoinsIsOn = false;
        Debug.Log("2x Is Over");

    }

    private IEnumerator ShootingStars()
    {
        CanShootStars = true;
        yield return new WaitForSecondsRealtime(8f);
        CanShootStars = false;
       Debug.Log("Over");

    }

  

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 0.5f);
    }
}
