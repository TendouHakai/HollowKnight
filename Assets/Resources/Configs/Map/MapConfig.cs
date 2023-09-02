using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Map", menuName = "Config/Map")]
public class MapConfig : ScriptableObject
{
    private static MapConfig instance;
    public static MapConfig getInstance()
    {
        if (instance == null)
        {
            instance = Resources.Load<MapConfig>("Configs/Map/Map");
        }
        return instance;
    }

    [SerializeField] private List<AreaConfig> areaConfigs = new List<AreaConfig>();

    public AreaConfig GetAreaConfig(int idArea)
    {
        return areaConfigs.Find(x=>x.ID == idArea);
    }

    public List<AreaConfig> GetAreaConfigs()
    {
        return areaConfigs;
    }

    public bool addMarker(int idArea,int idMarker, Vector3 pos)
    {
        AreaConfig areaConfig = areaConfigs.Find(x=>x.ID == idArea);
        
        foreach(markerInArea area in areaConfig.Markers)
        {
            if(area.ID == idMarker && area.position == pos)
            {
                return false;
            }
        }
        areaConfig.Markers.Add(new markerInArea(idMarker, pos));
        return true;
    }

    public List<markerInArea> GetMarkerInArea(int idAreea)
    {
        return areaConfigs.Find(x=>x.ID ==idAreea).getMarkers();
    }

    public bool AreaIsHaveScene( int idArea, int idScene)
    {
        AreaConfig area = areaConfigs.Find(x=>x.ID==idArea);

        return area.isHaveScene(idScene);
    }
}

[System.Serializable]
public class AreaConfig
{
    public int ID;
    public string Name;
    public bool isUnlock;

    public List<int> SceneIDs;
    public List<markerInArea> Markers;

    public bool isHaveScene(int ID)
    {
        return SceneIDs.Contains(ID);
    }

    public List<markerInArea> getMarkers()
    {
        if(Markers == null)
        {
            Markers = new List<markerInArea>();
            MarkerDataInArea data = SaveLoadSystem.LoadMarkerMapData(ID);
            if(data != null)
            {
                for (int i = 0; i < data.MarkerIDs.Length; i++)
                {
                    Markers.Add(new markerInArea(data.MarkerIDs[i], new Vector3(data.MarkerPositions[i, 0], data.MarkerPositions[i, 1], data.MarkerPositions[i, 2])));
                }
            }
        }

        return Markers;
    }
}

[System.Serializable]
public class MarkerDataInArea
{
    public int[] MarkerIDs;
    public float[,] MarkerPositions;

    public MarkerDataInArea(AreaConfig config)
    {
        MarkerIDs = new int[config.Markers.Count];
        MarkerPositions = new float[config.Markers.Count,3];
        for(int i = 0; i < config.Markers.Count; i++)
        {
            MarkerIDs[i] = config.Markers[i].ID;
            MarkerPositions[i, 0] = config.Markers[i].position.x;
            MarkerPositions[i, 1] = config.Markers[i].position.y;
            MarkerPositions[i, 2] = config.Markers[i].position.z;
        }
    }
}

[System.Serializable]
public class markerInArea
{
    public int ID;
    public Vector3 position;

    public markerInArea(int id, Vector3 position)
    {
        this.ID = id;
        this.position = position;
    }
}