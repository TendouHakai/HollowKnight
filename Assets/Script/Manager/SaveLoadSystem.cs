using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
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
    public static void SaveHollowShadeData(Vector3 pos, int sceneNumber)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/HollowShadeData.fun";
        FileStream stream = new FileStream(path, FileMode.Create);

        HollowShadeData data = new HollowShadeData(pos, sceneNumber);

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
    public static void SaveSettingData(int volumeSFX, int volumeMusic)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/SettingData.fun";
        FileStream stream = new FileStream(path, FileMode.Create);

        SettingData data = new SettingData(volumeSFX, volumeMusic);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static SettingData LoadSettingData()
    {
        string path = Application.persistentDataPath + "/SettingData.fun";
        Debug.Log(path);

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SettingData data = formatter.Deserialize(stream) as SettingData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.Log("Save file not found in " + path);
            return null;
        }
    }
}
