using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdEnemy : MonoBehaviour
{
    [SerializeField] Transform PointA;

    private Vector3 Targetpos;
    private Transform BirdPos;
    
    private Rigidbody2D BirdRb;
    public float BirdmoveSpeed;

    private bool IsPointA;

    private Animator BirdAnimator;

    private float PoopTimer;
    public float PoopTimerTime;
    public GameObject SpawnPointPos;
    public GameObject PoopPrefab;

    void Start()
    {
        BirdPos = GetComponent<Transform>();
        BirdRb = GetComponent<Rigidbody2D>();
        BirdAnimator = GetComponentInChildren<Animator>();

        Targetpos = PointA.position;
        IsPointA = true;

        
        PoopTimer = PoopTimerTime;
    }

    // Update is called once per frame
    void Update()
    {

        BirdRb.velocity = (Targetpos - transform.position).normalized * BirdmoveSpeed;

        PoopTimer -= Time.deltaTime;
        if(PoopTimer <= 0)
        {
            Instantiate(PoopPrefab, SpawnPointPos.transform.position, Quaternion.identity);
            PoopTimer = PoopTimerTime;
        }

        if (Vector3.Distance(BirdPos.position,Targetpos) <= 0.1f && IsPointA)
        {

            IsPointA = false;
            BirdAnimator.Play("BirdFade");

            Destroy(gameObject,0.5f);
        }
      



    }


  

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(PointA.position, 0.3f);


    }
}
