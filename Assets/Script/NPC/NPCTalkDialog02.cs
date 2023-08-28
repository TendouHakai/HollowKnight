using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class NPCTalkDialog02 : MonoBehaviour
{
    [SerializeField] protected GameObject dialogUI;
    [SerializeField] protected GameObject talkUI;
    [SerializeField] protected GameObject[] dialogObjs;
    [SerializeField] protected float timeNextDialog;
    protected int index = 0;
    protected bool PlayerIsInRange = false;

    protected bool startTalk = false;
    float timeStart = 0f;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject obj in dialogObjs)
        {
            obj.SetActive(false);
        }

        dialogUI.SetActive(false );
        talkUI.SetActive(false );
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerIsInRange && Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (index == 0)
            {
                talkUI.SetActive(false);
                dialogUI.SetActive(true);

                startTalk = true;
                timeStart = 0f;

                dialogObjs[index].SetActive(true);
                index++;
            }
            else nextTalk();
            
        }

        if (startTalk)
        {
            if (timeStart > timeNextDialog)
            {
                if(index <= dialogObjs.Length - 1)
                {
                    dialogObjs[index].SetActive(true);
                    index++;
                    timeStart = 0f;
                }
                else startTalk = false;
            }
            else timeStart += Time.deltaTime;
        }
    }
    
    public void nextTalk()
    {
        if (index <= dialogObjs.Length - 1)
        {
            dialogObjs[index].SetActive(true);
            index++;
            timeStart = 0f;
        }
        else endTalk();
    }

    public void endTalk()
    {
        startTalk = false;
        index = 0;
        foreach (GameObject obj in dialogObjs)
        {
            obj.SetActive(false);
        }
        dialogUI.SetActive(false);
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerIsInRange = true;
            talkUI.SetActive(true);
        }
    }

    protected void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerIsInRange = false;
            talkUI.SetActive(false);
            endTalk();
        }
    }
}
