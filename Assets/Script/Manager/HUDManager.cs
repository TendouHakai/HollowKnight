using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    private static HUDManager instance;

    public static HUDManager getInstance()
    {
        if (instance == null)
        {
            instance = GameObject.FindObjectOfType<HUDManager>();
        }
        return instance;
    }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        coinText.text = coin.ToString();
    }

    [Header("----------Soul----------")]    
    [SerializeField] public int soul;
    [SerializeField] Animator SoulAni;

    [Header("----------Health----------")]
    [SerializeField] public int maxHealth;
    [SerializeField] public int health;
    [SerializeField] List<Animator> healthList;

    [Header("----------Coin----------")]
    [SerializeField] public int coin;
    [SerializeField] Animator coinAni;
    [SerializeField] TextMeshProUGUI coinText;

    [Header("----------Player----------")]
    [SerializeField] Player player;
    public bool isGetFocus = false;

    // Soul
    public void upSoul()
    {
        if (soul == 4) return;
        switch(soul)
        {
            case 0:
                SoulAni.Play("Soul_UpToQuater");
                break;
            case 1:
                SoulAni.Play("Soul_UpToHalf");
                break;
            case 2:
                SoulAni.Play("Soul_UpTo3Quater");
                break;
            case 3:
                SoulAni.Play("Soul_FULL");
                break;
        }

        soul += 1;
    }

    public void downSoul()
    {
        if (soul == 0) return;
        switch (soul)
        {
            case 4:
                SoulAni.Play("Soul_DownTo3Quater");
                break;
            case 3:
                SoulAni.Play("Soul_DownToHalf");
                break;
            case 2:
                SoulAni.Play("Soul_DownToQuater");
                break;
            case 1:
                SoulAni.Play("Soul_DownToEmpty");
                break;
        }

        soul -= 1;
    }

    public bool isEnoughSoul()
    {
        if (soul > 1)
            return true;
        return false;
    }

    // health
    public void healthDown(int healthCount)
    {
        for(int i = 0; i < healthCount; i++)
        {
            healthList[health - i - 1].Play("Health_BREAK");
        }

        health -= healthCount;
    }

    public void healthUp()
    {
        healthList[health].Play("Health_REFILL");

        health += 1;
    }

    public bool isMaxHealth()
    {
        if(health >= maxHealth)
        {
            return true;
        }
        return false;
    }

    // Coin
    public void addCoin(int coin)
    {
        this.coin += coin;
        coinText.text = this.coin.ToString();

        coinAni.Play("Coin_GET");
    }
}
