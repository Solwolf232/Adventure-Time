using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuckyBlockLogic : MonoBehaviour
{
    [Header("Serlized Fields")]
    [SerializeField] public List<GameObject> Prizes;
    [SerializeField] Animator PrizesAnimator;
    [SerializeField] Animator ColliderAnimator;

    [Header("Lucky Block Logic")]
    public int RandomPrize; // Random Prize 
    private bool Broke; //Lucky Block Broke
    public bool CanCollect=true;

    [Header("Components")]
    private Transform Playertransform;

    [Header("Booleans")]
    public bool ShouldMove; // Prize Should Move Towards Player



    public void Start()
    {
        RandomPrize = -1; // Setting The Random Prize To "False";
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Playertransform = collision.gameObject.GetComponent<Transform>();
        Broke = true;

        if (collision.gameObject.CompareTag("Player") && CanCollect)
        {
            ShouldMove = true;
            bool PlayerIsLowerThenBlock = Playertransform.position.y < transform.position.y;
            RandomPrize = Random.Range(0, 3);


            if (PlayerIsLowerThenBlock)
            {
                RandomizePrizes();
            }
        }
    }

    public void Update()
    {

        if (Broke)
        {
            PrizesAnimator.SetTrigger("LuckyBlockBroke");
            Broke = false;
        }
    }



    public void RandomizePrizes()
    {
        switch (RandomPrize)
        {

            case 0:
                StarPrize();
                break;
            case 1:
                DoubleCoinsPrize();

                break;
            case 2:
                SpeedPrize();

                break;
        }
    }

    public void StarPrize()
    {
        ColliderAnimator.Play("Star");
        CanCollect = false;
        Prizes[RandomPrize].SetActive(true);
    }
    public void DoubleCoinsPrize()
    {
        ColliderAnimator.Play("Leaf");

        CanCollect = false;
        Prizes[RandomPrize].SetActive(true);
    }
    public void SpeedPrize()
    {
        ColliderAnimator.Play("Sandals");
        Prizes[RandomPrize].SetActive(true);
        CanCollect = false;
    }
}
