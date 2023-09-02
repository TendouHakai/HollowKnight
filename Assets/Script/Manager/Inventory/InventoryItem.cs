using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    [SerializeField] List<Item> items = new List<Item>();
    [SerializeField] RectTransform selectUI;
    [SerializeField] SelectZoomAnimation selectAni;

    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI keyText;
    [SerializeField] TextMeshProUGUI operationText;
    [SerializeField] TextMeshProUGUI decriptionText;

    [Header("-------------ITEM EQUIPMENT------------")]
    [SerializeField] Item MaskSharp;
    [SerializeField] Item DreamNail;
    [SerializeField] Item Nail;
    [SerializeField] Item Spell;
    [SerializeField] Item Geo;

    [Header("-------------ITEM SHOP------------")]
    [SerializeField] Item itemFrefabs;
    [SerializeField] GameObject inventoryShop;

    [Header("_____________IMENU CONTROLER---------")]
    [SerializeField] IMenuManager inventoryManager;

    int index = 0;

    public void Start()
    {
        if(items.Count == 0)
            loadDataInventoryItem();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            selectItem(DIRECT.up);
        }
        else if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            selectItem(DIRECT.down);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            selectItem(DIRECT.left);
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            selectItem(DIRECT.right);
        }
    }

    public void addItemShop(int ID)
    {
        if (items.Count == 0)
            loadDataInventoryItem();
        Item item = Instantiate(itemFrefabs, transform.position, Quaternion.identity);
        item.ID = ID;
        item.gameObject.transform.parent = inventoryShop.transform;
        items.Add(item);

        InventoryConfig.getInstance().addItemShop(ID);
    }

    public void loadDataInventoryItem()
    {
        items.Clear();
        loadItemEquipment();
        loadItemShop();

        selectUI.position = items[index].transform.position;
    }

    public void loadItemEquipment()
    {
        // load item equipment
        MaskSharp.loadItem(InventoryConfig.getInstance().MaskSharp_ID);
        DreamNail.loadItem(InventoryConfig.getInstance().DreamNail_ID);
        Nail.loadItem(InventoryConfig.getInstance().Nail_ID);
        Spell.loadItem(InventoryConfig.getInstance().Spell_ID);
        Geo.GetComponentInChildren<TextMeshProUGUI>().text = HUDManager.getInstance().coin.ToString();

        items.Add(MaskSharp);
        items.Add(DreamNail);
        items.Add(Nail);
        items.Add(Spell);
        items.Add(Geo);
    }

    public void loadItemShop()
    {
        // load item shop
        List<int> listIDItemShop = InventoryConfig.getInstance().getInventoryItemShops();
        foreach (int i in listIDItemShop)
        {
            Item item = Instantiate(itemFrefabs, transform.position, Quaternion.identity);
            item.ID = i;
            item.gameObject.transform.parent = inventoryShop.transform;
            items.Add(item);
        }
    }

    public void selectItem(DIRECT direct)
    {
        // Tìm item gần nhất theo hướng chỉ định // gần nhất theo chiều dọc lẫn chiều ngang => phải xét cả hai chiều
        int minIndex = -1;
        float minDistanceX = 9999;
        float minDistanceY = 9999;
        switch (direct)
        {
            case DIRECT.up:
                for(int i = 0; i < items.Count; i++)
                {
                    if (i != index)
                    {
                        float distanceX = items[i].transform.position.x - items[index].transform.position.x;
                        float distanceY = items[i].transform.position.y - items[index].transform.position.y;

                        if (distanceY > 0f)
                        {
                            if (minDistanceX >= Mathf.Abs(distanceX))
                            {
                                minDistanceX = Mathf.Abs(distanceX);
                                if (minDistanceY > distanceY)
                                {
                                    minDistanceY = distanceY;
                                    minIndex = i;
                                }
                                else minIndex = i;
                            }
                        }
                    }
                }
                break;
            case DIRECT.down:
                for (int i = 0; i < items.Count; i++)
                {
                    if(i != index)
                    {
                        float distanceX = items[i].transform.position.x - items[index].transform.position.x;
                        float distanceY = items[index].transform.position.y - items[i].transform.position.y;

                        if (distanceY > 0f)
                        {
                            if (minDistanceX >= Mathf.Abs(distanceX))
                            {
                                minDistanceX = Mathf.Abs(distanceX);
                                if (minDistanceY > distanceY)
                                {
                                    minDistanceY = distanceY;
                                    minIndex = i;
                                }
                                else minIndex = i;
                            }
                        }
                    }
                }
                break;
            case DIRECT.left:
                for (int i = 0; i < items.Count; i++)
                {
                    if (i != index)
                    {
                        float distanceX = items[index].transform.position.x - items[i].transform.position.x;
                        float distanceY = items[i].transform.position.y - items[index].transform.position.y;

                        if (distanceX >= 0f)
                        {
                            if (minDistanceY >= Mathf.Abs(distanceY))
                            {
                                minDistanceY = Mathf.Abs(distanceY);
                                if (minDistanceX > distanceX)
                                {
                                    minDistanceX = distanceX;
                                    minIndex = i;
                                }
                                else minIndex = i;
                            }
                        }
                    }
                }
                break;
            case DIRECT.right:
                for (int i = 0; i < items.Count; i++)
                {
                    if(i != index)
                    {
                        float distanceX = items[i].transform.position.x - items[index].transform.position.x;
                        float distanceY = items[i].transform.position.y - items[index].transform.position.y;

                        if (distanceX >= 0f)
                        {
                            if (minDistanceY > Mathf.Abs(distanceY))
                            {
                                minDistanceY = Mathf.Abs(distanceY);
                                if (minDistanceX > distanceX)
                                {
                                    minDistanceX = distanceX;
                                    minIndex = i;
                                }
                                else minIndex = i;
                            }
                        }
                    }
                }
                break;
        }

        if(minIndex != -1)
        {
            Debug.Log (minIndex.ToString ());
            index = minIndex;

            if (items[index].isEmpty == false)
            {
                ItemConfig item = ItemConfigs.getInstance().getConfig(items[index].ID);
                nameText.text = item.Name;
                decriptionText.text = item.Description;
            }

            selectAni.runAni(items[index].transform.position, items[index].GetComponent<RectTransform>().sizeDelta);
        }
        else
        {
            if (direct == DIRECT.left)
            {
                inventoryManager.changeToMap();
                return;
            }
        }
    }
}
