using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SoundBtn : Button
{
    [SerializeField] Slider slider;
    [SerializeField] TextMeshProUGUI textVolume;
    [SerializeField] bool isChangeSFX;
    [SerializeField] bool isChangeMusic;

    public override void Start()
    {
        base.Start();
        slider.maxValue = SoundManager.MaxVolume;
        if (isChangeSFX)
        {
            slider.value = SoundManager.getInstance().getVolumeSFX();
            textVolume.text = slider.value.ToString();
        }
        else
        {
            slider.value = SoundManager.getInstance().getVolumeMusic();
            textVolume.text = slider.value.ToString();
        }
    }

    public void OnChangeValueSlider()
    {
        textVolume.text = slider.value.ToString();

        if (isChangeSFX )
        {
            SoundManager.getInstance().setVolumeSFX((int)slider.value);
        }
        else
        {
            SoundManager.getInstance().setVolumeMusic((int)slider.value);
        }
    }
}
