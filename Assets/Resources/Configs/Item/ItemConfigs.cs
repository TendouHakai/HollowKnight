using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemConfig", menuName = "Config/Item")]
public class ItemConfigs : ScriptableObject
{
    private static ItemConfigs instance;
    public static ItemConfigs getInstance()
    {
        if (instance == null)
        {
            instance = Resources.Load<ItemConfigs>("Configs/Item/ItemConfig");
        }
        return instance;
    }

    [SerializeField] private List<ItemConfig> configs = new List<ItemConfig>();
    public ItemConfig getConfig(int ID)
    {
        return configs.Find(c => c.ID == ID);
    }

    public List<ItemConfig> getListConfigs()
    {
        return configs;
    }
}

[System.Serializable]
public class ItemConfig
{
    public int ID;

    public string Name;
    public Sprite img;

    [TextArea(10,6)]
    public string Description;
    public int geoCount;
}
