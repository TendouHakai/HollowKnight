using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingMenuCOntroler : MonoBehaviour
{
    [SerializeField] MenuControler menuControler;
    public void OnGameSettingBtnClick()
    {
        menuControler.ChangeToGameSettingMenu();
    }

    public void OnSoundSettingBtnClick()
    {
        menuControler.ChangeToSoundSettingMenu();
    }

    public void OnExitSettingMenu()
    {
        menuControler.BackToMainMenu();
    }
}
