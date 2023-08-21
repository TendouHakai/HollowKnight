using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public const float Height = 140;
    [SerializeField] GameObject talkUI;
    [SerializeField] GameObject shopUI;
    [SerializeField] GameObject listItemUI;
    //[SerializeField] Item itemFrefabs;
    [SerializeField] Item[] listItems;

    [SerializeField] SelectAnimation selectAni;

    [Header("----------Item Content----------")]
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI descriptionText;

    protected int index;
    protected bool PlayerIsInRange = false;

    private void Start()
    {
        PlayerIsInRange = true;

        loadContent(listItems[0].ID);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow) && PlayerIsInRange)
        {
            if(shopUI.activeInHierarchy)
            {
                Up();
            }
            else
            {
                talkUI.SetActive(false);
                shopUI.SetActive(true);
            }
        }

        if(Input.GetKeyDown(KeyCode.DownArrow) && shopUI.activeInHierarchy)
        {
            Down();
        }
    }

    public void Up()
    {
        if(index != 0)
        {
            //listItemUI.transform.position -= new Vector3(0, Height,0);
            selectAni.runAnimation(DIRECT.up, listItemUI.transform.position.y - Height);
            index--;

            loadContent(listItems[index].ID);
        }
    }

    public void Down()
    {
        if(index < listItems.Length)
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
            shopUI.SetActive(false);
            talkUI.SetActive(false);
        }
    }
}
