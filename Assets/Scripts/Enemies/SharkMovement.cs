using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SharkMovement : MonoBehaviour
{
    [Header ("Components")]
    private Rigidbody2D SharkRb;
    [SerializeField] Transform SpriteTm;

   

    [Header("Points Shark Should GoTo")]
    public Transform PointA;
    public Transform PointB;
    Vector3 targetPosition;
    private bool movingToA;

    [Header("SettingVaribales")]
    public float sharkKbForce;
    public float JumpSpeed;

    [Header("Scripts")]
    private PlayerLogic PlayerLogicScript;


    void Start()
    {
        SharkRb = GetComponent<Rigidbody2D>();
        movingToA = true; // Move to Point A First
    }

    void Update()
    {
        CheckTargetPos(); // Check What Point Shark is Moving To

        bool DistanceDidntReached = Vector3.Distance(transform.position, targetPosition) >= 0.25f;

        if (DistanceDidntReached)
        {
            SharkRb.velocity = (targetPosition - transform.position).normalized * JumpSpeed;
            ChangeRotation();
        }
        else 
        {
            // Switch direction and update the sprite scale
            movingToA = !movingToA;
            SharkRb.velocity = Vector2.zero;
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Player Is Dead 

            PlayerLogicScript = collision.gameObject.GetComponent<PlayerLogic>();

            SpawnPlayer();
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(PointA.position, 0.3f); //Draw Sphere In Point A

        Gizmos.DrawWireSphere(PointB.position, 0.3f); //Draw Sphere In Point B

        Gizmos.DrawLine(PointA.position, PointB.position); // Draw A Line Between Them.
    }

    public void CheckTargetPos()
    {
         targetPosition = movingToA ? PointA.position : PointB.position;
    }
    public void ChangeRotation()
    {
        if (movingToA)
        {
            SpriteTm.localScale = new Vector3(4, 4, 1);
        }
        else
        {
            SpriteTm.localScale = new Vector3(-4, 4, 1);
        }
    }

    public void SpawnPlayer()
    {
        if (PlayerLogicScript.CheckPointHit)
        {
            PlayerLogicScript.ResetAll();
        }
        else
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}


