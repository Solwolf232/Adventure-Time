using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DialougeScript : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public PlayerMovement PlayerMovementScript;
    public GameObject Cam;
    public GameObject LuckyBlockLogicPos;
    public GameObject Enemy;
    public GameObject Portal;
    bool ShouldMove = true;
    bool ShouldMove2 = true;
    bool CanClick = true;

    public string[] lines;
    public float textSpeed;
    private int index;
    private bool isTyping = false;

    void Start()
    {
        // Start the dialogue when the script is enabled
        textComponent.text = string.Empty;
        StartDialogue();
    }

    public void Update()
    {
        // Skip to the next line if the player presses any key while the text is still typing

        if (isTyping && Input.GetMouseButton(0))
        {
            // Stop typing and display the full line of text
            StopAllCoroutines();
            textComponent.text = lines[index];
            isTyping = false;
        }


        if (index >= 3)
        {
            PlayerMovementScript.enabled = true;
        }
        else
            PlayerMovementScript.enabled = false;


    }

    public void LateUpdate()
    {
        if (index == 6 || index==8)
        PlayScene();
    }
    public void StartDialogue()
    {
        // Reset index and start typing the current line
        index = 0;
        StartCoroutine(TypeLine());
    }

    public IEnumerator TypeLine()
    {
        isTyping = true;
        textComponent.text = string.Empty;

        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed * Time.deltaTime);
        }

        isTyping = false; // Typing is complete

        // Automatically proceed to the next line after typing is done
    }

    public void NextLine()
    {

        if (CanClick)
        {
            index++;

            if (index < lines.Length)
            {
                // Start typing the next line
                StartCoroutine(TypeLine());
            }
            else
            {
                // End of dialogue
                Destroy(gameObject);
            }
        }
          
       
       
    }


    public void PlayScene()
    {
       
        if (index == 6 && ShouldMove==true)
        {
            CanClick = false;
            if (ShouldMove)
            {
                Cam.transform.position = Vector3.MoveTowards(Cam.transform.position, LuckyBlockLogicPos.transform.position, 18f * Time.deltaTime);
                CameraFollow CamFollowAc = Cam.GetComponent<CameraFollow>();
                CamFollowAc.enabled = false;
            }

           if(Vector3.Distance(Cam.transform.position,LuckyBlockLogicPos.transform.position) <= 1f)
            {
                ShouldMove = false;
                Invoke("CameraToPlace", 0.5f);
                
            }
        }
        if (index >= 8 && ShouldMove2 == true)
        {
            CanClick = false;
            if (ShouldMove2)
            {
                Cam.transform.position = Vector3.MoveTowards(Cam.transform.position, Portal.transform.position, 18f * Time.deltaTime);
                CameraFollow CamFollowAc = Cam.GetComponent<CameraFollow>();
                CamFollowAc.enabled = false;
            }
            if (Vector3.Distance(Cam.transform.position, Portal.transform.position) <= 1.3f)
            {
                ShouldMove2 = false;
                Invoke("ActiveTrue",0.2f);
               

               
                Invoke("CameraToPlace", 2.3f);
                
            }
          
        }
    }

    public void CameraToPlace()
    {
        CameraFollow CamFollowAc = Cam.GetComponent<CameraFollow>();
        CamFollowAc.enabled = true;
        StartCoroutine(CanClickNext());
    }
    public void ActiveTrue()
    {
        Enemy.SetActive(true);
        Portal.SetActive(true);
        Animator EnemyAnimator = Enemy.GetComponentInChildren<Animator>();
        Animator PortalAnimator = Portal.GetComponent<Animator>();

        EnemyAnimator.Play("EnemyIntro");
        PortalAnimator.Play("PortalIntro");
    }

    private IEnumerator CanClickNext()
    {
        yield return new WaitForSeconds(0.3f);
        CanClick = true;
    }
}
