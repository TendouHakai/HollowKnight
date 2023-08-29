using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;
    public const int MaxVolume = 10;

    public static SoundManager getInstance()
    {
        if (instance == null)
        {
            instance = GameObject.FindObjectOfType<SoundManager>();
        }
        return instance;
    }

    private void Awake()
    {
        instance = this;

        DontDestroyOnLoad(gameObject);
    }

    [SerializeField] private AudioSource sourceMusic;
    [SerializeField] private AudioSource sourceSFXPlayer;
    [SerializeField] private AudioSource sourceSFXEnemy;

    public void PlayMusic(string ID)
    {
        sourceMusic.clip = SoundConfigs.getInstance().getConfig(ID).clip;
        sourceMusic.loop = true;
        sourceMusic.Play();
    }

    public void StopMusic()
    {
        sourceMusic.Stop();
    }

    public void PlaySFXPlayer(string ID)
    {
        sourceSFXPlayer.clip = SoundConfigs.getInstance().getConfig(ID).clip;
        sourceSFXPlayer.loop = false;
        sourceSFXPlayer.Play();
    }

    public void PlaySFXPlayerLoop(string ID)
    {
        sourceSFXPlayer.clip = SoundConfigs.getInstance().getConfig(ID).clip;
        sourceSFXPlayer.loop = true;
        sourceSFXPlayer.Play();
    }

    public void StopSFXPlayer()
    {
        sourceSFXPlayer.Stop();
    }

    public void PlaySFXEnemy(string ID)
    {
        sourceSFXEnemy.clip = SoundConfigs.getInstance().getConfig(ID).clip;
        sourceSFXEnemy.loop = false;
        sourceSFXEnemy.Play();
    }

    public void setVolumeSFX(int volume)
    {
        sourceSFXEnemy.volume = volume*1.0f/MaxVolume;
        sourceSFXPlayer.volume = volume*1.0f/MaxVolume;
    }

    public int getVolumeSFX()
    {
        return Convert.ToInt32(sourceSFXEnemy.volume * MaxVolume);
    }

    public void setVolumeMusic(int volume)
    {
        sourceMusic.volume = volume*1.0f/MaxVolume;
    }

    public int getVolumeMusic()
    {
        return Convert.ToInt32(sourceMusic.volume * MaxVolume);
    }
}
