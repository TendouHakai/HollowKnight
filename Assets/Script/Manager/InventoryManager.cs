using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] public InventoryItem inventoryItemMenu;
    [SerializeField] public MapMenuControler mapMenu;
    [SerializeField] Animator Ani;

    [Header("-----------NAME MENU-------------")]
    [SerializeField] TextMeshProUGUI currentNameMenuText;
    [SerializeField] TextMeshProUGUI leftNameMenuText;
    [SerializeField] TextMeshProUGUI rightNameMenuText;

    private void Start()
    {
        inventoryItemMenu.gameObject.SetActive(true);
        mapMenu.gameObject.SetActive(false);
    }

    public void addItemShop(int id)
    {
        inventoryItemMenu.addItemShop(id);
    }

    public void changeToMap()
    {
        Ani.Play("ChangeInventoryToMap");
        inventoryItemMenu.gameObject.SetActive(false);
        mapMenu.gameObject.SetActive(true);

        currentNameMenuText.text = "Bản đồ";
        rightNameMenuText.text = "Kho đồ";
        leftNameMenuText.gameObject.SetActive(false);
    }

    public void changeToInventory()
    {
        Ani.Play("ChangeMapToInventory");
        mapMenu.gameObject.SetActive(false);
        inventoryItemMenu.gameObject.SetActive(true);

        currentNameMenuText.text = "Kho đồ";
        leftNameMenuText.gameObject.SetActive(true);
        leftNameMenuText.text = "Bản đồ";
        rightNameMenuText.gameObject.SetActive(true);
        rightNameMenuText.text = "Bùa chú";
    }
}
