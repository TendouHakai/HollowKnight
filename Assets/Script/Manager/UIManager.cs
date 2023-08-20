using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;

    public static UIManager getInstance()
    {
        if (instance == null)
        {
            instance = GameObject.FindObjectOfType<UIManager>();
        }
        return instance;
    }

    private void Awake()
    {
        instance = this;

        DontDestroyOnLoad(gameObject);
    }

    [Header("----------MENU----------")]
    public GameObject GuideMenu;

    void Start()
    {
        GuideMenu.SetActive(false);
    }

    public void OpenGuideMenu()
    {
        GuideMenu.SetActive(true);
    }

    public void CloseGuideMenu()
    {
        GuideMenu.SetActive(false);
    }
}
