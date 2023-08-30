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
