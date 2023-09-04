using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuControler : MonoBehaviour
{
    [SerializeField] MenuControler menuControler;
    public void onStartGameBtnClick()
    {
        SoundManager.getInstance().PlaySFXEnemy("btn_click");
        menuControler.startChangeScene();
    }

    public void onOptionsGameBtnClick()
    {
        SoundManager.getInstance().PlaySFXEnemy("btn_click");
        menuControler.changeToSettingMenu();
    }

    public void onIntroGameBtnClick()
    {
        SoundManager.getInstance().PlaySFXEnemy("btn_click");
    }

    public void onExitGameBtnClick()
    {
        SoundManager.getInstance().PlaySFXEnemy("btn_click");
        Application.Quit();
    }
}
