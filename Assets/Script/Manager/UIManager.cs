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
    public GameObject HUD;
    public GameObject Inventory;

    void Start()
    {
        GuideMenu.SetActive(false);
        HUD.SetActive(true);
        Inventory.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.I) && !Inventory.activeInHierarchy)
        {
            Inventory.SetActive(true);
            PlayerControl.getInstance().isInteract = true;
        }

        if(Input.GetKeyUp(KeyCode.Escape))
        {
            if (Inventory.activeInHierarchy)
            {
                Inventory.SetActive(false);
                PlayerControl.getInstance().isInteract = false;
            }
        }
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
