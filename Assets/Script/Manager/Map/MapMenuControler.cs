using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MapMenuControler : MonoBehaviour
{
    [SerializeField] RectTransform scrollViewTranform;
    [Header("-------------MENU--------------")]
    public MapExpandControler mapExpand;
    [SerializeField] GameObject GuideUIExpand;
    public MapCompassControler mapCompass;
    [SerializeField] GameObject GuideUICompass;

    [Header("-------------IMENU CONTROLER------------")]
    [SerializeField] IMenuManager inventoryManager;

    private void Start()
    {
        mapExpand.gameObject.SetActive(false);
        mapCompass.gameObject.SetActive(true);
    }

    public Vector2 getSizeScrollView()
    {
        return scrollViewTranform.sizeDelta;
    }

    public void changeMapExpand()
    {
        mapCompass.gameObject.SetActive(false);
        mapExpand.gameObject.SetActive(true);
        GuideUIExpand.SetActive(true);
        GuideUICompass.SetActive(false);
        mapExpand.Ani.Play("MapExpand_ZOOMOUT");
    }

    public void changeMapCompass()
    {
        mapExpand.gameObject.SetActive(false);
        mapCompass.gameObject.SetActive(true);
        GuideUICompass.SetActive(true);
        GuideUIExpand.SetActive(false);
    }

    public void addMarker(int ID, Vector3 pos)
    {
        mapExpand.addMarker(ID, pos);
    }

    public void changeToInventoryMenu()
    {
        inventoryManager.changeToInventory();
    }

    public void updateCurrentMap(int idscene)
    {
        mapExpand.updateCurrentMap(idscene);
        mapCompass.updateCurrentMap(idscene);
    }
}
