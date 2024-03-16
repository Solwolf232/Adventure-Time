using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UiLogic : MonoBehaviour
{
    [Header("Ui")]
    [SerializeField] public TMP_Text CoinsText;
    [SerializeField] public TMP_Text ScoreText;

    [Header("Animators")]
    public Animator CoinsAnimator;

    [Header ("Counters")]
    public int CoinsCounter = 0;


   
}
