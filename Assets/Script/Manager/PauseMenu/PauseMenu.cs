using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] UIManager manager;

    [Header("---------------MENU------------")]
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject pauseConfirmMenu;
    [SerializeField] GameObject SettingMenu;
    [SerializeField] GameObject gameSettingMenu;
    [SerializeField] GameObject soundSettingMenu;

    private void Start()
    {
        pauseMenu.SetActive(true);
        pauseConfirmMenu.SetActive(false);
        SettingMenu.SetActive(false);
        gameSettingMenu.SetActive(false);
        soundSettingMenu.SetActive(false);
    }

    public void OnResumeBtnClick()
    {
        SoundManager.getInstance().PlaySFXEnemy("btn_click");
        GameStateManager.getInstance().setState(Game_State.Play);
        manager.ClosePauseMenu();
    }

    public void OnOptionBtnClick()
    {
        SoundManager.getInstance().PlaySFXEnemy("btn_click");
        SettingMenu.SetActive(true);
        pauseMenu.SetActive(false);
    }

    public void OnExitBtnClick()
    {
        SoundManager.getInstance().PlaySFXEnemy("btn_click");
        pauseConfirmMenu.SetActive(true);
        pauseMenu.SetActive(false);
        SettingMenu.SetActive(false);
        gameSettingMenu.SetActive(false);   
        soundSettingMenu.SetActive(false);
    }

    public void OnYesBtn()
    {
        // save game data
        SoundManager.getInstance().PlaySFXEnemy("btn_click");
        SaveLoadSystem.saveAllData();

        GameStateManager.getInstance().setState(Game_State.BacktoMenu);
    }

    public void OnNoBtn()
    {
        SoundManager.getInstance().PlaySFXEnemy("btn_click");
        GameStateManager.getInstance().setState(Game_State.BacktoMenu);
    }

    public void OnExitToPauseMenu()
    {
        SoundManager.getInstance().PlaySFXEnemy("btn_click");
        pauseMenu.SetActive(true);
        SettingMenu.SetActive(false);
    }

    public void OnGameSettingBtnClick()
    {
        SoundManager.getInstance().PlaySFXEnemy("btn_click");
        gameSettingMenu.SetActive(true);
        SettingMenu.SetActive(false);
    }

    public void OnsoundSettingBtnClick()
    {
        SoundManager.getInstance().PlaySFXEnemy("btn_click");
        soundSettingMenu.SetActive(true);
        SettingMenu.SetActive(false);
    }

    // game setting and sound setting
    public void OnExitToSettingMenu()
    {
        SoundManager.getInstance().PlaySFXEnemy("btn_click");
        SettingMenu.SetActive(true);
        gameSettingMenu.SetActive(false);
        soundSettingMenu.SetActive(false);
    }

}
