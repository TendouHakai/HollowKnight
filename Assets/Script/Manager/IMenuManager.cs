using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IMenuManager : MonoBehaviour
{
    [SerializeField] public InventoryItem inventoryItemMenu;
    [SerializeField] public MapMenuControler mapMenu;
    [SerializeField] Animator Ani;

    [Header("-----------NAME MENU-------------")]
    [SerializeField] TextMeshProUGUI currentNameMenuText;
    [SerializeField] TextMeshProUGUI leftNameMenuText;
    [SerializeField] TextMeshProUGUI rightNameMenuText;

    IMENU_MENU currentMenu = IMENU_MENU.Inventory;

    private void Start()
    {
        inventoryItemMenu.gameObject.SetActive(true);
        mapMenu.gameObject.SetActive(false);
    }

    private void Update()
    {
        
    }

    public void addItemShop(int id)
    {
        inventoryItemMenu.addItemShop(id);
    }

    public void changeToMap()
    {
        Ani.Play("ChangeInventoryToMap");

        currentMenu = IMENU_MENU.Map;

        inventoryItemMenu.gameObject.SetActive(false);
        mapMenu.gameObject.SetActive(true);

        currentNameMenuText.text = "Bản đồ";
        rightNameMenuText.text = "Kho đồ";
        leftNameMenuText.gameObject.SetActive(false);
    }

    public void changeToInventory()
    {
        Ani.Play("ChangeMapToInventory");

        currentMenu = IMENU_MENU.Inventory;

        mapMenu.gameObject.SetActive(false);
        inventoryItemMenu.gameObject.SetActive(true);

        currentNameMenuText.text = "Kho đồ";
        leftNameMenuText.gameObject.SetActive(true);
        leftNameMenuText.text = "Bản đồ";
        rightNameMenuText.gameObject.SetActive(true);
        rightNameMenuText.text = "Bùa chú";
    }

    public void OnEnable()
    {
        if(currentMenu == IMENU_MENU.Inventory)
        {
            Ani.Play("IMenuStart");
        }
        else if(currentMenu == IMENU_MENU.Map)
        {
            Ani.Play("IMenuMap");
        }
    }
}

public enum IMENU_MENU
{
    Inventory=1,
    Map=2,
}
