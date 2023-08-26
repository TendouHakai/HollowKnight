using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapExpandControler : MonoBehaviour
{
    [SerializeField] MapMenuControler MapControler;
    [SerializeField] List<MiniMapExpandControler> listMinimapExpand;
    [SerializeField] int currentSceneindex;

    [Header("----------------GUIDE------------------")]
    [SerializeField] GameObject GuideUI;
    [SerializeField] GameObject topArrowUI;
    [SerializeField] GameObject bottomtArrowUI;
    [SerializeField] GameObject leftArrowUI;
    [SerializeField] GameObject rightArrowUI;

    [Header("---------------STEP--------------------")]
    [SerializeField] float step;

    [Header("--------------ANIMATION----------------")]
    [SerializeField] public Animator Ani;

    // limit point 
    float left = 0;
    float right = 0;
    float top = 0;
    float bottom = 0;

    void Start()
    {
        loadMap();
        calculateLimitPoint();
        if(right == 0f) rightArrowUI.SetActive(false);
        if(left == 0f) leftArrowUI.SetActive(false);
        if(top == 0f) topArrowUI.SetActive(false);
        if(bottom == 0f) bottomtArrowUI.SetActive(false);

        int idScene = SceneManager.GetActiveScene().buildIndex;
        for (int i = 0; i < listMinimapExpand.Count; i++)
        {
            if (MapConfig.getInstance().AreaIsHaveScene(listMinimapExpand[i].ID, idScene))
            {
                listMinimapExpand[i].setPLayerisIn(true);
                currentSceneindex = i;
                break;
            }
        }

    }

    void Update()
    {
        // xu ly di chuyen map lon
        Vector3 temp = transform.localPosition;
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            temp.y += step;
            
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            temp.y -= step;
            
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            temp.x -= step;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if(temp.x >= right)
            {
                MapControler.changeToInventoryMenu();
                return;
            }
            temp.x += step;
            
        }

        if (temp.y >= top)
        {
            temp.y = top;
            topArrowUI.SetActive(false);
        }
        else topArrowUI.SetActive(true);

        if (temp.y <= bottom)
        {
            temp.y = bottom;
            bottomtArrowUI.SetActive(false);
        }
        else bottomtArrowUI.SetActive(true);

        if (temp.x <= left)
        {
            temp.x = left;
            leftArrowUI.SetActive(false);
        }
        else leftArrowUI.SetActive(true);

        if (temp.x >= right)
        {
            temp.x = right;
            rightArrowUI.SetActive(false);
        }
        else rightArrowUI.SetActive(true);

        transform.localPosition = temp;

        // xu ly su kien
        if (Input.GetKeyDown(KeyCode.A))
        {
            Ani.Play("MapExpand_ZOOMIN");
        }

    }

    public void updateCurrentMap(int idScene)
    {
        listMinimapExpand[currentSceneindex].setPLayerisIn(false);
        for (int i = 0; i < listMinimapExpand.Count; i++)
        {
            if (MapConfig.getInstance().AreaIsHaveScene(listMinimapExpand[i].ID, idScene))
            {
                listMinimapExpand[i].setPLayerisIn(true);

                listMinimapExpand[i].updateMapCoordinates();

                currentSceneindex = i;
                break;
            }
        }
    }

    public void addMarker(int ID, Vector3 pos)
    {
        listMinimapExpand[currentSceneindex].addMarker(ID, pos);
    }

    public void loadMap()
    {
        for(int i =0; i < listMinimapExpand.Count; i++)
        {
            if (!MapConfig.getInstance().GetAreaConfig(listMinimapExpand[i].ID).isUnlock)
            {
                listMinimapExpand[i].gameObject.SetActive(false);
            }
        }
    }

    public void calculateLimitPoint()
    {
        float leftL = 0, rightL = 0, topL = 0, bottomL = 0;

        // tính các điểm giới hạn trên map
        for(int i =0; i< listMinimapExpand.Count; i++)
        {
            if (listMinimapExpand[i].gameObject.activeInHierarchy)
            {
                leftL = Mathf.Min(listMinimapExpand[i].transform.localPosition.x - listMinimapExpand[i].GetComponent<RectTransform>().sizeDelta.x / 2, leftL);
                rightL = Mathf.Max(listMinimapExpand[i].transform.localPosition.x + listMinimapExpand[i].GetComponent<RectTransform>().sizeDelta.x / 2, rightL);
                topL = Mathf.Max(listMinimapExpand[i].transform.localPosition.y + listMinimapExpand[i].GetComponent<RectTransform>().sizeDelta.y / 2, topL);
                bottomL = Mathf.Min(listMinimapExpand[i].transform.localPosition.y - listMinimapExpand[i].GetComponent<RectTransform>().sizeDelta.y / 2, bottomL);
            }
        }

        // tính lại các điểm giới sau khi trừ đi offset và kích thước viewport

        left = MapControler.getSizeScrollView().x/2 - rightL > 0f? 0f: MapControler.getSizeScrollView().x/2 - rightL;
        right = -MapControler.getSizeScrollView().x/2 - leftL < 0f? 0f: -MapControler.getSizeScrollView().x/2 - leftL;
        top = -MapControler.getSizeScrollView().y/2 - bottomL < 0f? 0f: -MapControler.getSizeScrollView().y/2 - bottomL;
        bottom = MapControler.getSizeScrollView().y/2 - topL > 0f? 0f: MapControler.getSizeScrollView().y/2 - topL;

        left += transform.localPosition.x;
        right += transform.localPosition.x;
        top += transform.localPosition.y;
        bottom += transform.localPosition.y;
    }

    public void endChangeMapCompass()
    {
        GuideUI.SetActive(false);
        MapControler.changeMapCompass();
    }
}
