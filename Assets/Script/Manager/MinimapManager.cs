using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class MinimapManager : MonoBehaviour
{
    private static MinimapManager instance;

    public static MinimapManager getInstance()
    {
        if (instance == null)
        {
            instance = GameObject.FindObjectOfType<MinimapManager>();
        }
        return instance;
    }

    private void Awake()
    {
        instance = this;
    }

    [SerializeField] public MiniMapControler MinimapUI;
    [SerializeField] GameObject NoMapUI;

    public bool isNoMap = false;
    // Start is called before the first frame update
    void Start()
    {
        MinimapUI.gameObject.SetActive(false);
        NoMapUI.SetActive(false);   

        if(UIManager.getInstance() != null)
        {
            UIManager.getInstance().updateCurrentMap();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            UIManager.getInstance().closeAllMenu();

            if(isNoMap)
            {
                NoMapUI.SetActive(true);
            }
            else MinimapUI.gameObject.SetActive(true);
        }

        if(Input.GetKeyUp(KeyCode.Tab))
        {
            if(isNoMap)
            {
                NoMapUI.SetActive(false);
            }
            else MinimapUI.gameObject.SetActive(false);
        }
    }

    public void addMarker(int id, Vector3 pos)
    {
        MinimapUI.addMarker(id, pos);
    }

    public void changeToNoMapUI()
    {
        NoMapUI.SetActive(true);
        MinimapUI.gameObject.SetActive(false);
    }

    public void changeToMinimapUI()
    {
        MinimapUI.gameObject.SetActive(true);
        NoMapUI.SetActive(false);
    }

    public void setPlayer(Player player)
    {
        MinimapUI.setPlayer(player);
    }
}
