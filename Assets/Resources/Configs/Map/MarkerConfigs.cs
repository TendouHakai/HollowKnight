using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Marker", menuName = "Config/Marker")]
public class MarkerConfigs : ScriptableObject
{
    private static MarkerConfigs instance;
    public static MarkerConfigs getInstance()
    {
        if (instance == null)
        {
            instance = Resources.Load<MarkerConfigs>("Configs/Map/Marker");
        }
        return instance;
    }

    [SerializeField] List<MarkerConfig> markerConfigs = new List<MarkerConfig>();

    public MarkerConfig getConfig(int ID)
    {
        return markerConfigs.Find(c => c.ID == ID);
    }

    public List<MarkerConfig> getListConfigs()
    {
        return markerConfigs;
    }
}

[System.Serializable]
public class MarkerConfig
{
    public int ID;
    public string Name;
    public Sprite img;

    public int IDItemNeed;
}
