using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingMenuCOntroler : MonoBehaviour
{
    [SerializeField] MenuControler menuControler;
    public void OnGameSettingBtnClick()
    {
        SoundManager.getInstance().PlaySFXEnemy("btn_click");
        menuControler.ChangeToGameSettingMenu();
    }

    public void OnSoundSettingBtnClick()
    {
        SoundManager.getInstance().PlaySFXEnemy("btn_click");
        menuControler.ChangeToSoundSettingMenu();
    }

    public void OnExitSettingMenu()
    {
        SoundManager.getInstance().PlaySFXEnemy("btn_click");
        menuControler.BackToMainMenu();
    }
}
