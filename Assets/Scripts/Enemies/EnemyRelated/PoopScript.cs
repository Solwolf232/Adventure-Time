using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
public class PoopScript : MonoBehaviour
{
    private PlayerLogic PlayerLogicScript;
    [SerializeField] GameObject VfxSpawnPoint;
    [SerializeField] GameObject Vfx;



    private float Timer;
    private float PoopTiming = 6f;
    private Rigidbody2D PoopRb;

    private Animator PoopAnimator;

    private void Start()
    {
        PoopRb = GetComponent<Rigidbody2D>();

        PoopAnimator = GetComponent<Animator>();

        Timer = PoopTiming;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerLogicScript = collision.gameObject.GetComponent<PlayerLogic>();
            
            Instantiate(Vfx,VfxSpawnPoint.transform.position,Quaternion.identity);
            PlayerLogicScript.KnockBack();
            PlayerLogicScript.TakeDamage();
            Destroy(gameObject);
        }

       
    }

    public void Update()
    {
        if(gameObject != null)
        {
            Timer -= Time.deltaTime;
        }
        if(Timer <= 0)
        {
            PoopAnimator.Play("PoopFade");
            Destroy(gameObject, 0.5f);
        }
    }
}
