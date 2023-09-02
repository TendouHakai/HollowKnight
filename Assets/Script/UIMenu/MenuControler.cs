using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class MenuControler : MonoBehaviour
{
    [Header("----------------MENU----------------------")]
    [SerializeField] GameObject MenuContain;
    [SerializeField] GameObject MainMenu;
    [SerializeField] GameObject SettingMenu;
    [SerializeField] GameObject GameSettingMenu;
    [SerializeField] GameObject SoundSettingMenu;

    [Header("---------------TIMELINE-------------------")]
    [SerializeField] PlayableDirector TimeLine;

    [Header("----------------CHANGE SCEME ENEMY---------------")]
    [SerializeField] Animator Ani;
    [SerializeField] int numberSceneStart;
    float timeStart = 0f;
    float timeChange = 1.3f;
    bool isChange = false;
    bool isStart = true;
    bool isEndTimeLine;
    // Start is called before the first frame update
    void Start()
    {
        isEndTimeLine = true;
        TimeLine.gameObject.SetActive(true);
        MenuContain.SetActive(false);

        MainMenu.SetActive(true);
        SettingMenu.SetActive(false);
        GameSettingMenu.SetActive(false);
        SoundSettingMenu.SetActive(false);

        SoundManager.getInstance().PlayMusic("DirtMouth_Music");

        Screen.SetResolution(1366, 768, true);
    }

    // Update is called once per frame
    void Update()
    {
        // end TimeLine
        if (isEndTimeLine)
        {
            if (timeStart > TimeLine.duration - 0.5f)
            {
                TimeLine.gameObject.SetActive(false);
                MenuContain.SetActive(true);

                timeStart = 0f;
                isEndTimeLine = false;
            }
            else timeStart += Time.deltaTime;
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            TimeLine.gameObject.SetActive(false);
            MenuContain.SetActive(true);

            timeStart = 0f;
            isEndTimeLine = false;
        }

        // change scene
        if (isChange)
        {
            if (timeStart > timeChange)
            {
                PLayerData data = SaveLoadSystem.LoadPlayerData();
                if(data != null)
                {
                    SceneManager.LoadScene(data.sceneNumber);
                }
                else SceneManager.LoadScene(numberSceneStart);

                timeStart = 0f;
                isChange = false;
            }
            else timeStart += Time.deltaTime;
        }

        //// start menu
        //if (isStart)
        //{
        //    if (timeStart > timeChange)
        //    {
        //        Ani.gameObject.SetActive(false);

        //        timeStart = 0f;
        //        isStart = false;
        //    }
        //    else timeStart += Time.deltaTime;
        //}
    }

    public void startChangeScene()
    {
        timeStart = 0f;
        isChange = true;
        Ani.gameObject.SetActive(true);
        Ani.Play("Sceneloader_START_CHANGE_SCENE");
    }

    public void changeToSettingMenu()
    {
        SettingMenu.SetActive(true);
        MainMenu.SetActive(false);
    }

    public void BackToMainMenu()
    {
        MainMenu.SetActive(true);
        SettingMenu.SetActive(false);
    }

    public void ChangeToGameSettingMenu()
    {
        GameSettingMenu.SetActive(true);
        SettingMenu.SetActive(false) ;
    }

    public void ChangeToSoundSettingMenu()
    {
        SoundSettingMenu.SetActive(true);
        SettingMenu.SetActive(false);
    }

    public void BackToSettingMenu()
    {
        // save soundSetting
        SaveLoadSystem.SaveSettingData(SoundManager.getInstance().getVolumeSFX(), SoundManager.getInstance().getVolumeMusic());

        SettingMenu.SetActive(true);
        SoundSettingMenu.SetActive(false);
        GameSettingMenu.SetActive(false);
    }
}
