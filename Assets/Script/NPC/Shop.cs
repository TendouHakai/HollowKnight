using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] GameObject talkUI;
    [SerializeField] GameObject shopUI;

    [Header("----------Window----------")]
    [SerializeField] public GameObject ItemDisplayW;
    [SerializeField] public GameObject ItemPurchaseW;
    [SerializeField] public GameObject ItemPurchasedW;

    protected int index;
    public bool PlayerIsInRange = false;

    private void Start()
    {
        shopUI.SetActive(false);
        PlayerIsInRange = true;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow) && PlayerIsInRange)
        {
            if(shopUI.activeInHierarchy)
            {
               // Up();
            }
            else
            {
                talkUI.SetActive(false);
                shopUI.SetActive(true);

                PlayerControl.getInstance().isInteract = true;

                ItemDisplayW.SetActive(true); ItemPurchaseW.SetActive(false); ItemPurchasedW.SetActive(false);
            }
        }

    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && PlayerControl.getInstance().isInteract == false)
        {
            PlayerIsInRange = true;
            talkUI.SetActive(true);
        }
    }

    protected void OnTriggerExit2D(Collider2D collision)    
    {
        if (collision.tag == "Player")
        {
            PlayerControl.getInstance().isInteract = false;
            PlayerIsInRange = false;
            shopUI.SetActive(false);
            talkUI.SetActive(false);
        }
    }

    public void endInteract()
    {
        PlayerControl.getInstance().isInteract = false;
        shopUI.SetActive(false);
        talkUI.SetActive(true);
    }
}
