using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtFalling : MonoBehaviour
{
    private Rigidbody2D ObstacleRb;
    private bool ObjectHasFallen;
    private Transform UsualSpot;
    private Transform Pos;
    public float SmoothSpeed;
    private bool ShouldBack;

    public GameObject vfxSpawnPoint;
    public GameObject Vfx;

    public void Start()
    {
        ObstacleRb = GetComponent<Rigidbody2D>();
        Pos = transform;
        UsualSpot = new GameObject("UsualSpot").transform; // Create a new Transform for UsualSpot
        UsualSpot.position = Pos.position;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !ObjectHasFallen || collision.gameObject.CompareTag("Mount") && !ObjectHasFallen)
        {
            Instantiate(Vfx, vfxSpawnPoint.transform.position, Quaternion.identity);
            ObjectFalling();
            

        }
    }

    public void Update()
    {
        if (ObjectHasFallen && ShouldBack)
        {
            ObstacleRb.constraints = RigidbodyConstraints2D.FreezeAll;
            Pos.position = Vector3.MoveTowards(transform.position, UsualSpot.position, SmoothSpeed * Time.deltaTime);

            if (Pos.position == UsualSpot.position)
            {
                ShouldBack = false;
                ObjectHasFallen = false;
                ObstacleRb.constraints = RigidbodyConstraints2D.FreezeAll;
            }
        }
    }

    public void ObjectFalling()
    {
        ObstacleRb.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
        ObjectHasFallen = true;
        StartCoroutine(TimerCount());
    }

    public IEnumerator TimerCount()
    {
        yield return new WaitForSeconds(2.5f);
        ShouldBack = true;
    }


}
