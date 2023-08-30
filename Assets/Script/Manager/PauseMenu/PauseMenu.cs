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
        GameStateManager.getInstance().setState(Game_State.Play);
        manager.ClosePauseMenu();
    }

    public void OnOptionBtnClick()
    {
        SettingMenu.SetActive(true);
        pauseMenu.SetActive(false);
    }

    public void OnExitBtnClick()
    {
        pauseConfirmMenu.SetActive(true);
        pauseMenu.SetActive(false);
        SettingMenu.SetActive(false);
        gameSettingMenu.SetActive(false);   
        soundSettingMenu.SetActive(false);
    }

    public void OnYesBtn()
    {
        // save game data
        // save HUD data
        SaveLoadSystem.SaveHUDData(HUDManager.getInstance());


        GameStateManager.getInstance().setState(Game_State.BacktoMenu);
    }

    public void OnNoBtn()
    {
        GameStateManager.getInstance().setState(Game_State.BacktoMenu);
    }

    public void OnExitToPauseMenu()
    {
        pauseMenu.SetActive(true);
        SettingMenu.SetActive(false);
    }

    public void OnGameSettingBtnClick()
    {
        gameSettingMenu.SetActive(true);
        SettingMenu.SetActive(false);
    }

    public void OnsoundSettingBtnClick()
    {
        soundSettingMenu.SetActive(true);
        SettingMenu.SetActive(false);
    }

    // game setting and sound setting
    public void OnExitToSettingMenu()
    {
        SettingMenu.SetActive(true);
        gameSettingMenu.SetActive(false);
        soundSettingMenu.SetActive(false);
    }

}
