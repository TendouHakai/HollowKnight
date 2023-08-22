using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] public InventoryItem inventoryItem;

    public void addItemShop(int id)
    {
        inventoryItem.addItemShop(id);
    }
}
