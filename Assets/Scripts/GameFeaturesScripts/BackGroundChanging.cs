using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundChanging : MonoBehaviour
{
    [SerializeField] GameObject BackGround1;
    [SerializeField] GameObject BackGround2;
    [SerializeField] GameObject Background3;
    [SerializeField] GameObject PlayerLight;
    [SerializeField] GameObject MountLight;
    private Animator CaveAnimator;


    public void Start()
    {
        CaveAnimator = BackGround1.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Mount"))
        {

            StartCoroutine(MakeAChange());
            
        }
    }


    public IEnumerator MakeAChange()
    {
        CaveAnimator.Play("CaveFadeOut");
        yield return new WaitForSeconds(2f);
        BackGround1.SetActive(false);
        PlayerLight.SetActive(false);
        MountLight.SetActive(false);
        Background3.SetActive(true);
        BackGround2.SetActive(true);
        
        

    }
}
