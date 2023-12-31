using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using UnityEngine;

public static class SaveLoadSystem
{
    public static void SaveHUDData(HUDManager manager)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/HUDData.fun";
        FileStream stream = new FileStream(path, FileMode.Create);

        HUDData data = new HUDData(manager);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static HUDData LoadHUDData()
    {
        string path = Application.persistentDataPath + "/HUDData.fun";
        Debug.Log(path);

        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            HUDData data = formatter.Deserialize(stream) as HUDData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.Log("Save file not found in "+ path);
            return null;
        }
    }

    // Player data
    public static void SavePlayerData(Vector3 pos, int sceneNumber)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/PlayerData.fun";
        FileStream stream = new FileStream(path, FileMode.Create);

        PLayerData data = new PLayerData(pos, sceneNumber);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PLayerData LoadPlayerData()
    {
        string path = Application.persistentDataPath + "/PlayerData.fun";
        Debug.Log(path);

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PLayerData data = formatter.Deserialize(stream) as PLayerData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.Log("Save file not found in " + path);
            return null;
        }
    }

    // save hollowshade data
    public static void SaveHollowShadeData(Vector3 pos, int sceneNumber, HUDManager manager)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/HollowShadeData.fun";
        FileStream stream = new FileStream(path, FileMode.Create);

        HollowShadeData data = new HollowShadeData(pos, sceneNumber, manager);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static HollowShadeData LoadHollowShadeData()
    {
        string path = Application.persistentDataPath + "/HollowShadeData.fun";
        Debug.Log(path);

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            HollowShadeData data = formatter.Deserialize(stream) as HollowShadeData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.Log("Save file not found in " + path);
            return null;
        }
    }

    public static void deleteHollowShadeData()
    {
        string path = Application.persistentDataPath + "/HollowShadeData.fun";
        if(File.Exists(path))
        {
            File.Delete(path);
        }
        else
        {
            Debug.Log("Delete file not found in " + path);
        }
    }

    // save setting data
    public static void SaveSoundSettingData(int volumeSFX, int volumeMusic)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/SoundSettingData.fun";
        FileStream stream = new FileStream(path, FileMode.Create);

        SoundSettingData data = new SoundSettingData(volumeSFX, volumeMusic);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static SoundSettingData LoadSoundSettingData()
    {
        string path = Application.persistentDataPath + "/SoundSettingData.fun";
        Debug.Log(path);

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SoundSettingData data = formatter.Deserialize(stream) as SoundSettingData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.Log("Save file not found in " + path);
            return null;
        }
    }

    // save setting GameSetting Data
    public static void SaveGameSettingData(Resolution resolution)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/GameSettingData.fun";
        FileStream stream = new FileStream(path, FileMode.Create);

        GameSettingData data = new GameSettingData(resolution);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static GameSettingData LoadGameSettingData()
    {
        string path = Application.persistentDataPath + "/GameSettingData.fun";
        Debug.Log(path);

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            GameSettingData data = formatter.Deserialize(stream) as GameSettingData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.Log("Save file not found in " + path);
            return null;
        }
    }

    // save inventory data
    public static void saveInventoryData(InventoryConfig config)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/InventoryData.fun";
        FileStream stream = new FileStream(path, FileMode.Create);

        InventoryData data = new InventoryData(config);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static InventoryData LoadInventoryData()
    {
        string path = Application.persistentDataPath + "/InventoryData.fun";
        Debug.Log(path);

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            InventoryData data = formatter.Deserialize(stream) as InventoryData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.Log("Save file not found in " + path);
            return null;
        }
    }

    // save markerMap
    public static void saveMarkerMapData(AreaConfig config)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/Marker"+config.ID+".fun";
        FileStream stream = new FileStream(path, FileMode.Create);

        MarkerDataInArea data = new MarkerDataInArea(config);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static void saveAllMarkerMapData(List<AreaConfig> listArea)
    {
        foreach (AreaConfig config in listArea)
        {
            saveMarkerMapData(config);
        }
    }

    public static MarkerDataInArea LoadMarkerMapData(int ID)
    {
        string path = Application.persistentDataPath + "/Marker" + ID + ".fun";
        Debug.Log(path);

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            MarkerDataInArea data = formatter.Deserialize(stream) as MarkerDataInArea;
            stream.Close();
            return data;
        }
        else
        {
            Debug.Log("Save file not found in " + path);
            return null;
        }
    }

    public static void saveAllData()
    {
        SaveLoadSystem.SaveHUDData(HUDManager.getInstance());
        SaveLoadSystem.SaveSoundSettingData(SoundManager.getInstance().getVolumeSFX(), SoundManager.getInstance().getVolumeMusic());
        SaveLoadSystem.saveInventoryData(InventoryConfig.getInstance());
        SaveLoadSystem.saveAllMarkerMapData(MapConfig.getInstance().GetAreaConfigs());
    }
}
