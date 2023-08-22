using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory", menuName = "Config/Inventory")]
public class InventoryConfig : ScriptableObject
{
    private static InventoryConfig instance;
    public static InventoryConfig getInstance()
    {
        if (instance == null)
        {
            instance = Resources.Load<InventoryConfig>("Configs/Inventory/Inventory");
        }
        return instance;
    }
    [Header("--------------ITEM EQUIPMENT--------------")]
    [SerializeField] public int MaskSharp_ID;
    [SerializeField] public int DreamNail_ID;
    [SerializeField] public int Nail_ID;
    [SerializeField] public int Spell_ID;

    [Header("--------------ITEM SHOP--------------")]
    [SerializeField] List<int> listInventoryItemShop;

    public List<int> getInventoryItemShops()
    {
        return listInventoryItemShop;
    }

    public void addItemShop(int id)
    {
        listInventoryItemShop.Add(id);
    }
}
