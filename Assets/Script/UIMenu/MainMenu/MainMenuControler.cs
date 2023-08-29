using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuControler : MonoBehaviour
{
    [SerializeField] MenuControler menuControler;
    public void onStartGameBtnClick()
    {
        menuControler.startChangeScene();
    }

    public void onOptionsGameBtnClick()
    {
        menuControler.changeToSettingMenu();
    }

    public void onIntroGameBtnClick()
    {

    }

    public void onExitGameBtnClick()
    {
        Application.Quit();
    }
}
