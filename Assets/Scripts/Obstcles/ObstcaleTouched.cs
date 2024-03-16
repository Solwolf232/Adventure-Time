using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstcaleTouched : MonoBehaviour
{

    [Header("Scripts")]
    private PlayerLogic PlayerLogicScript;

    [Header("Components")]
    [SerializeField] private Animator PlayerAnimator;

    public void Start()
    {
        PlayerLogicScript = FindObjectOfType<PlayerLogic>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("Player") && !PlayerLogicScript.PlayerDead)
        {
            SetTakingDamage();   
        }
    }

    public void SetTakingDamage()
    {
        PlayerAnimator.Play("FinnHit");
        PlayerLogicScript.TakeDamage();
    }
}
