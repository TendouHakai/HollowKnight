using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[System.Serializable]
public class InventoryData
{
    //ITEM EQUIPMENT
    public int MaskSharp_ID = -1;
    public int DreamNail_ID = -1;
    public int Nail_ID = -1;
    public int Spell_ID = -1;

    //ITEM SHOP
    public int[] listInventoryItemShop;

    public InventoryData(InventoryConfig data)
    {
        MaskSharp_ID = data.MaskSharp_ID;
        DreamNail_ID = data.DreamNail_ID;
        Nail_ID = data.Nail_ID;
        Spell_ID = data.Spell_ID;

        listInventoryItemShop = new int[data.getInventoryItemShops().Count];
        for(int i = 0; i <  listInventoryItemShop.Length; i++)
        {
            listInventoryItemShop[i] = data.getInventoryItemShops()[i];
        }
    }
}

public class InventoryConfig : MonoBehaviour
{
    private static InventoryConfig instance;
    public static InventoryConfig getInstance()
    {
        if (instance == null)
        {
            instance = GameObject.FindObjectOfType<InventoryConfig>();
        }
        return instance;
    }

    private void Awake()
    {
        instance = this;
    }

    [Header("--------------ITEM EQUIPMENT--------------")]
    [SerializeField] public int MaskSharp_ID = -1;
    [SerializeField] public int DreamNail_ID = -1;
    [SerializeField] public int Nail_ID = -1;
    [SerializeField] public int Spell_ID = -1;

    [Header("--------------ITEM SHOP--------------")]
    [SerializeField] List<int> listInventoryItemShop = new List<int>();

    private void Start()
    {
        LoadData();
    }

    public void LoadData()
    {
        InventoryData data = SaveLoadSystem.LoadInventoryData();

        if (data != null)
        {
            MaskSharp_ID = data.MaskSharp_ID;
            DreamNail_ID = data.DreamNail_ID;
            Nail_ID = data.Nail_ID;
            Spell_ID = data.Spell_ID;

            listInventoryItemShop.Clear();
            for (int i = 0; i < data.listInventoryItemShop.Length; i++)
            {
                listInventoryItemShop.Add(data.listInventoryItemShop[i]);   
            }
        }
    }

    public List<int> getInventoryItemShops()
    {
        return listInventoryItemShop;
    }

    public void addItemShop(int id)
    {
        listInventoryItemShop.Add(id);
    }

    public bool IsInInventory(int ID)
    {
        return listInventoryItemShop.Contains(ID);
    }
}
