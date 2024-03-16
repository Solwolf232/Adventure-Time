using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class ScoreAndCoinsData : ScriptableObject
{
    [SerializeField]
    private int _ValueCoins;
    [SerializeField]
    private int _ValueScore;

    private PlayerLogic PlayerLogicScript;

    public void Awake()
    {
        PlayerLogicScript = FindObjectOfType<PlayerLogic>();
    }

    public void KeepCoinsAndScore()
    {
        if (PlayerLogicScript.PlayerDead)
        {
            _ValueCoins -= 35;
        }
    }


    public int Value
    {
        get { return _ValueCoins; } 
        set { _ValueCoins = value; }

      
    }

    public int ScoreValue
    {
        get { return _ValueScore; }
        set { _ValueScore = value; }
    }


}
