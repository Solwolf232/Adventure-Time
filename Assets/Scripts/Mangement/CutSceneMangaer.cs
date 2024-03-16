using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class CutSceneMangaer : MonoBehaviour
{
    [Header("Animators")]
    public Animator PlayerAnim;
    public Animator CanvasAnim;

    [Header("Texts")]
    public TextMeshProUGUI CreditsTxt;

    public bool IsFinished;
    int SceneCount = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsFinished == true)
        {
            NextScene();
        }
    }


    public void OnPlayButton()
    {
        CanvasAnim.Play("MenuOut");

        StartCoroutine(PlayScene());
    }

    public void OnCreditsButton()
    {
        CreditsTxt.text = "Yair Weizman";
    }

    public void OnQuitButtion()
    {
        Application.Quit();
    }
    public void InvokeUi()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    public IEnumerator PlayScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        yield return new WaitForSeconds(100f);
    }

    public void NextScene()
    {
        SceneCount++;
        if (SceneCount == 1)
        {
            PlayerAnim.SetBool("IsJumping", false);
            PlayerAnim.SetTrigger("FinishedLevel");
            Invoke("InvokeUi", 0.8f);
        }
    }
}
