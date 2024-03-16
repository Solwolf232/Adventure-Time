using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class ButtosLogic : MonoBehaviour
{
    public List<TextMeshProUGUI> Quests;
    private int count = 0;
    [SerializeField] ScoreAndCoinsData ScoreAndCoins;
    [SerializeField] PlayerLogic PlayerLogicScript;
    [SerializeField] GameObject Pallete;
    public void Start()
    {
        
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            Quests[0].text = "Collect 100 Coins";
            Quests[1].text = "Kill 5 Shrooms";
            Quests[2].text = "Get 500 Score";
        }
        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
           Quests[0].text = "Collect 350 Coins";
            Quests[1].text = "Kill 7 Evil Shrooms";
           Quests[2].text = "Get 1500 Score";
        }

        for (int i = 0; i < Quests.Count; i++)
        {
            Quests[i].enabled = false;
        }
        PlayerLogicScript = FindAnyObjectByType<PlayerLogic>();
    }

    public void OnQuestsClick()
    {
        if(count % 2 == 0)
        {
            Pallete.SetActive(true);
            
            for (int i = 0; i < Quests.Count; i++)
            {
                Quests[i].enabled = true;
            }
            count++;
        }
        else
        {
            Pallete.SetActive(false);

            for (int i = 0; i < Quests.Count; i++)
            {
                Quests[i].enabled = false;
            }
            count++;
        }


    }

    public void Update()
    {
        CheckQuests();
    }

    public void CheckQuests()
    {
        if(SceneManager.GetActiveScene().buildIndex == 2)
        {
            if (ScoreAndCoins.Value >= 100)
            {
                Quests[0].color = Color.green;
            }

            if (PlayerLogicScript.ShroomCount > 5)
            {
                Quests[1].color = Color.green;
            }

            if (SceneManager.GetActiveScene().buildIndex > 2)
            {
                Quests[2].color = Color.green;
            }

        }

        if(SceneManager.GetActiveScene().buildIndex == 3)
        {

        }
    }
       
}
