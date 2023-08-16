using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pedestal : MonoBehaviour
{
    [SerializeField] GameObject KeyPopUp;

    public bool isInRange;

    private void Start()
    {
        isInRange = false;
        KeyPopUp.SetActive(false);
    }

    private void Update()
    {
        if (isInRange)
        {
            if(Input.GetKeyDown(KeyCode.UpArrow))
            {
                UIManager.getInstance().OpenGuideMenu();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if(isInRange == false)
            {
                isInRange = true;
                KeyPopUp.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
           isInRange = false;
            KeyPopUp.SetActive(false);
        }
    }
}
