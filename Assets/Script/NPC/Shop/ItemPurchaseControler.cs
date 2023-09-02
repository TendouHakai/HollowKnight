using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemPurchaseControler : MonoBehaviour
{
    public float height = 75;
    [SerializeField] Shop shopcontroler;

    [SerializeField] SelectAnimation selectAni;
    [SerializeField] GameObject selectItem;

    [Header("----------Item Content----------")]
    ItemConfig item;
    [SerializeField] TextMeshProUGUI NameText;
    [SerializeField] Image image;
    [SerializeField] TextMeshProUGUI geocountText;
    [SerializeField] List<GameObject> button;
    int index = 0;

    private void Start()
    {
        height = Screen.height* height/1080f;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            if(index > 0)
            {
                selectAni.runAnimation(DIRECT.up, button[index].transform.position.y + height);
                index--;
            }
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if(index < button.Count - 1)
            {
                selectAni.runAnimation(DIRECT.down, button[index].transform.position.y - height);
                index++;
            }
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            shopcontroler.ItemDisplayW.SetActive(true);
            this.gameObject.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            onClickBtn();
        }
    }

    public void loadItem(int ID)
    {
        item = ItemConfigs.getInstance().getConfig(ID);

        NameText.text = item.Name;
        image.sprite = item.img;
        geocountText.text = item.geoCount.ToString();
    }

    public void onClickBtn()
    {
        if(index == 0)
        {
            HUDManager.getInstance().addCoin(-item.geoCount);

            //Debug.Log(UIManager.getInstance().Inventory.GetComponent<InventoryManager>().add);

            UIManager.getInstance().Inventory.GetComponent<InventoryManager>().addItemShop(item.ID);
            shopcontroler.ItemDisplayW.GetComponent<ItemDisplayControler>().removeItem();
            shopcontroler.ItemPurchasedW.SetActive(true);
            this.gameObject.SetActive(false);
        }
        else
        {
            shopcontroler.ItemDisplayW.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }
}
