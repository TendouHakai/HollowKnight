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
}
