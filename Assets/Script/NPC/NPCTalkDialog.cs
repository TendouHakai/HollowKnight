using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPCTalkDialog : MonoBehaviour
{
    [SerializeField] protected GameObject dialogUI;
    [SerializeField] protected GameObject talkUI;
    [SerializeField] protected TextMeshProUGUI dialogText;
    [SerializeField] protected string[] dialogTexts;
    [SerializeField] protected float wordSpeed;
    protected int index = 0;
    protected bool PlayerIsInRange = false;

    [SerializeField] BaseObject NPCObject;

    protected void Update()
    {
        if (PlayerIsInRange && Input.GetKeyDown(KeyCode.UpArrow))
        {
            if(index != 0)
            {
                NextLine();
            }
            else
            {
                talkUI.SetActive(false);
                dialogUI.SetActive(true);

                NPCObject.setState((int)GameConstant.NPC_STATE_TALK);

                dialogText.text = "";
                StartCoroutine(Typing());
                index++;
            }
        }
    }

    public void NextLine()
    {
        if(index < dialogTexts.Length)
        {
            dialogText.text = "";
            StartCoroutine(Typing());
            index++;
        }
        else ZeroText();
    }

    public void ZeroText()
    {
        dialogText.text = "";
        index = 0;

        dialogUI.SetActive(false);
        NPCObject.setState((int)GameConstant.NPC_STATE_END_TALK);
    }

    IEnumerator Typing ()
    {
        foreach (char letter in dialogTexts[index].ToCharArray())
        {
            dialogText.text += letter; 
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            PlayerIsInRange = true;
            talkUI.SetActive(true);
        }
    }

    protected void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            PlayerIsInRange = false;
            talkUI.SetActive(false);
            ZeroText();
        }
    }

}
