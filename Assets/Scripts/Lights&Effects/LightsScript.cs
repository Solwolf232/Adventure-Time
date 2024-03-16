using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsScript : MonoBehaviour
{
    [SerializeField] GameObject LuckyBlockLight;

  
    void Update()
    {
        StartCoroutine(TurnOnOff());
    }

    private IEnumerator TurnOnOff()
    {
        LuckyBlockLight.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        LuckyBlockLight.SetActive(false);
    }
}
