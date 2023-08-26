using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public GameObject Map;

    void Start()
    {
        GuideMenu.SetActive(false);
        HUD.SetActive(true);
        Inventory.SetActive(false);

        updateCurrentMap();
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

    public void closeAllMenu()
    {
        Inventory.SetActive(false );
    }

    // Map
    public void addMarker(int id, Vector3 pos)
    {
        Map.GetComponent<MapMenuControler>().addMarker(id, pos);
    }

    public void updateCurrentMap()
    {
        Map.GetComponent<MapMenuControler>().updateCurrentMap(SceneManager.GetActiveScene().buildIndex);
    }
}
