using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderDetectorCoins : MonoBehaviour
{
    [Header("Scripts")]
    public UiLogic UiLogicScript;
    public ColliderDetector PrizesDetector;
    [SerializeField] ScoreAndCoinsData ScoreAndCoins;

    [Header("Vars")]
    public int Coins = 0;
    public int Score = 0;


    [Header("Particles")]
    public GameObject Vfx;


    public void Awake()
    {
        UiLogicScript = FindObjectOfType<UiLogic>();

        SetUpUi();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        bool ColldierDetectorFound = collision.gameObject.GetComponent<ColliderDetector>() != null;
        bool CollidedWithCoin = collision.gameObject.CompareTag("Coin");

        if (ColldierDetectorFound)
        {
            PrizesDetector = collision.gameObject.GetComponent<ColliderDetector>();
        }

        if (CollidedWithCoin)
        {
            AddUpScore();

            Instantiate(Vfx, collision.gameObject.transform.position, Quaternion.identity);

            bool CoinsMultiplyX1 = PrizesDetector == null || !PrizesDetector.DoubeCoinsIsOn;
            bool CoinsMultiplyX2 = PrizesDetector != null && PrizesDetector.DoubeCoinsIsOn;

            if (CoinsMultiplyX1)
            {
                SetUpCoinsX1();

                Destroy(collision.gameObject);

                AnimationPlay();               
            }
            if(CoinsMultiplyX2)
            {
                SetUpCoinsX1();

                Destroy(collision.gameObject);
              
                AnimationPlay();
            }

        }
       
    }
   
    public void AnimationPlay()
    {
        UiLogicScript.CoinsAnimator.Play("TextBigger");
    }

    public void AddUpScore()
    {
        ScoreAndCoins.ScoreValue += 10;
    }
    public void SetUpUi()
    {
        UiLogicScript.CoinsText.text = "X " + ScoreAndCoins.Value.ToString();
        UiLogicScript.ScoreText.text = ScoreAndCoins.ScoreValue.ToString();
    }

    public void SetUpCoinsX1()
    {
        ScoreAndCoins.Value++;
        UiLogicScript.CoinsText.text = "X " + ScoreAndCoins.Value.ToString();
        UiLogicScript.ScoreText.text = ScoreAndCoins.ScoreValue.ToString();
    }
    public void SetUpCoinsX2()
    {
        ScoreAndCoins.Value+=2;
        UiLogicScript.CoinsText.text = "X " + ScoreAndCoins.Value.ToString();
        UiLogicScript.ScoreText.text = ScoreAndCoins.ScoreValue.ToString();
    }


}
