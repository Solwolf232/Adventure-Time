using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TimerScript : MonoBehaviour
{
    private float Timer = 0f;
    private TextMeshProUGUI TimerText;
    private bool GoodToGo;
    void Start()
    {
        Invoke("StartTimer", 0.65f);
        Timer = 0f;
        TimerText = GetComponent<TextMeshProUGUI>();

    }

    // Update is called once per frame
    void Update()
    {



        if (GoodToGo)
        {
            Timer += Time.deltaTime;


            int integerTimer = Mathf.FloorToInt(Timer);
            TimerText.text = integerTimer.ToString();
        }
       
    }


    public void StartTimer()
    {
        GoodToGo = true;
    }
}
