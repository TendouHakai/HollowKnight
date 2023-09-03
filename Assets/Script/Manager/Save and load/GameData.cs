using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HUDData
{
    public int MaxHP;
    public int currentHP;
    public int coin;
    public int soul;

    public HUDData(HUDManager manager)
    {
        MaxHP = manager.maxHealth;
        currentHP = manager.health;
        coin = manager.coin;
        soul = manager.soul;
    }
}

[System.Serializable]
public class PLayerData
{
    public float[] positionBench;
    public int sceneNumber;

    public PLayerData(Vector3 positionBench, int SceneNumber)
    {
        this.positionBench = new float[3];
        this.positionBench[0] = positionBench.x;
        this.positionBench[1] = positionBench.y;
        this.positionBench[2] = -0.01f;

        this.sceneNumber = SceneNumber;
    }
}

[System.Serializable]
public class HollowShadeData
{
    public float[] position;
    public int sceneNumber = -1;

    public int coin;
    public int soul;

    public HollowShadeData(Vector3 position, int sceneNumber, HUDManager manager)
    {
        this.position = new float[3];
        this.position[0] = position.x;
        this.position[1] = position.y;
        this.position[2] = -0.01f;

        this.sceneNumber = sceneNumber;

        coin = manager.coin;
        soul = manager.soul;
    }
}

[System.Serializable]
public class SettingData
{
    public int volumeSFX;
    public int volumeMusic;

    public SettingData(int volumeSFX, int volumeMusic)
    {
        this.volumeSFX = volumeSFX;
        this.volumeMusic = volumeMusic;
    }
}
