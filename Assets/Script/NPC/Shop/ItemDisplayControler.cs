using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemDisplayControler : MonoBehaviour
{
    [SerializeField] Shop shopcontroler;

    [SerializeField] GameObject listItemUI;
    [SerializeField] List<Item> listItems;

    [SerializeField] SelectAnimation selectAni;
    [SerializeField] Animator selectItemAni;

    [Header("----------Item Content----------")]
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI descriptionText;

    protected int index;
    float Height = 140f;
    // Start is called before the first frame update
    void Start()
    {
        List<int> listItemPurchased = InventoryConfig.getInstance().getInventoryItemShops();

        for(int i = 0; i< listItems.Count; i++)
        {
            Debug.Log(listItems[i].ID);
            if (listItemPurchased.Contains(listItems[i].ID))
            {
                Destroy(listItems[i].gameObject);
                listItems.RemoveAt(i);

                i--;
            }
        }

        Height = Screen.height * Height / 1080f;

        loadContent(listItems[0].ID);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Up();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Down();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            shopcontroler.endInteract();
        }

        if (Input.GetKeyDown(KeyCode.Space) && listItems.Count != 0)
        {
            //InventoryManager.getInstance().addItemShop(listItems[index].ID);
            ItemConfig item = ItemConfigs.getInstance().getConfig(listItems[index].ID);
            if (item.geoCount > HUDManager.getInstance().coin)
            {
                selectItemAni.Play("BuyItemFail");
            }
            else
            {
                shopcontroler.ItemPurchaseW.SetActive(true);
                shopcontroler.ItemPurchaseW.GetComponent<ItemPurchaseControler>().loadItem(item.ID);
                this.gameObject.SetActive(false);
            }
        }
    }

    public void Up()
    {
        if (index > 0)
        {
            //listItemUI.transform.position -= new Vector3(0, Height,0);
            selectAni.runAnimation(DIRECT.up, listItemUI.transform.position.y - Height);
            index--;

            loadContent(listItems[index].ID);
        }
    }

    public void Down()
    {
        if (index < listItems.Count - 1)
        {
            //listItemUI.transform.position += new Vector3(0, Height, 0);
            selectAni.runAnimation(DIRECT.down, listItemUI.transform.position.y + Height);
            index++;

            loadContent(listItems[index].ID);
        }
    }

    public void loadContent(int ID)
    {
        ItemConfig item = ItemConfigs.getInstance().getConfig(ID);

        nameText.text = item.Name;
        descriptionText.text = item.Description;
    }

    public void removeItem()
    {
        Destroy(listItems[index].gameObject);
        listItems.RemoveAt(index);
        if (listItems.Count == 0)
            index = 0;
        else if (index >= listItems.Count)
            index = listItems.Count - 1;
    }
}
