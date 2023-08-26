using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class MapCompassControler : MonoBehaviour
{
    [SerializeField] MapMenuControler MapControler;
    [SerializeField] List<MiniMapCompassControler> listMinimapCompass;
    int index = 0;

    [Header("----------------GUIDE------------------")]
    [SerializeField] GameObject GuideUI;

    [Header("----------------PLAYER-----------------")]
    [SerializeField] GameObject playerMarker;


    // Start is called before the first frame update
    void Start()
    {
        loadMap();
        int idScene = SceneManager.GetActiveScene().buildIndex;

        for (int i = 0; i < listMinimapCompass.Count; i++)
        {
            if (MapConfig.getInstance().AreaIsHaveScene(listMinimapCompass[i].ID, idScene))
            {
                playerMarker.transform.position = listMinimapCompass[i].transform.position;
                updateIndex(i);
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            selectMiniMap(DIRECT.up);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            selectMiniMap(DIRECT.down);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            selectMiniMap(DIRECT.left);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            selectMiniMap(DIRECT.right);
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            MapControler.changeMapExpand();
        }
    }

    public void updateCurrentMap(int idScene)
    {
        for (int i = 0; i < listMinimapCompass.Count; i++)
        {
            if (MapConfig.getInstance().AreaIsHaveScene(listMinimapCompass[i].ID, idScene))
            {
                playerMarker.transform.position = listMinimapCompass[i].transform.position;
                updateIndex(i);
                break;
            }
        }
    }

    public void loadMap()
    {
        for (int i = 0; i < listMinimapCompass.Count; i++)
        {
            if (!MapConfig.getInstance().GetAreaConfig(listMinimapCompass[i].ID).isUnlock)
            {
                listMinimapCompass[i].gameObject.SetActive(false);
            }
        }
    }

    public void updateIndex(int index)
    {
        Color color = Color.white;
        color.a = 100f/255f;
        listMinimapCompass[this.index].GetComponentInChildren<Image>().color = color;

        this.index = index;

        color.a = 255f/255f;
        listMinimapCompass[index].GetComponentInChildren<Image>().color = color;
    }

    public void selectMiniMap(DIRECT direct)
    {
        int minIndex = -1;
        float minDistanceX = 9999;
        float minDistanceY = 9999;

        List<MiniMapCompassControler> listMinimapCompassCurrent = new List<MiniMapCompassControler>();

        switch (direct)
        {
            case DIRECT.up:
                for (int i = 0; i < listMinimapCompass.Count; i++)
                {
                    if (listMinimapCompass[i].gameObject.activeInHierarchy)
                    {
                        if (i != index)
                        {
                            float distanceX = listMinimapCompass[i].transform.position.x - listMinimapCompass[index].transform.position.x;
                            float distanceY = listMinimapCompass[i].transform.position.y - listMinimapCompass[index].transform.position.y;

                            if (distanceY > 0f)
                            {
                                if (minDistanceX >= Mathf.Abs(distanceX))
                                {
                                    minDistanceX = Mathf.Abs(distanceX);
                                    if (minDistanceY > distanceY)
                                    {
                                        minDistanceY = distanceY;
                                        minIndex = i;
                                    }
                                    else minIndex = i;
                                }
                            }
                        }
                    }
                }
                break;
            case DIRECT.down:
                for (int i = 0; i < listMinimapCompass.Count; i++)
                {
                    if (listMinimapCompass[i].gameObject.activeInHierarchy)
                    {
                        if (i != index)
                        {
                            float distanceX = listMinimapCompass[i].transform.position.x - listMinimapCompass[index].transform.position.x;
                            float distanceY = listMinimapCompass[index].transform.position.y - listMinimapCompass[i].transform.position.y;

                            if (distanceY > 0f)
                            {
                                if (minDistanceX >= Mathf.Abs(distanceX))
                                {
                                    minDistanceX = Mathf.Abs(distanceX);
                                    if (minDistanceY > distanceY)
                                    {
                                        minDistanceY = distanceY;
                                        minIndex = i;
                                    }
                                    else minIndex = i;
                                }
                            }
                        }
                    }

                }
                break;
            case DIRECT.left:
                for (int i = 0; i < listMinimapCompass.Count; i++)
                {
                    if (listMinimapCompass[i].gameObject.activeInHierarchy)
                    {
                        if (i != index)
                        {
                            float distanceX = listMinimapCompass[index].transform.position.x - listMinimapCompass[i].transform.position.x;
                            float distanceY = listMinimapCompass[i].transform.position.y - listMinimapCompass[index].transform.position.y;

                            if (distanceX >= 0f)
                            {
                                if (minDistanceY >= Mathf.Abs(distanceY))
                                {
                                    minDistanceY = Mathf.Abs(distanceY);
                                    if (minDistanceX > distanceX)
                                    {
                                        minDistanceX = distanceX;
                                        minIndex = i;
                                    }
                                    else minIndex = i;
                                }
                            }
                        }
                    }    

                }
                break;
            case DIRECT.right:
                for (int i = 0; i < listMinimapCompass.Count; i++)
                {
                    if (listMinimapCompass[i].gameObject.activeInHierarchy)
                    {
                        if (i != index)
                        {
                            float distanceX = listMinimapCompass[i].transform.position.x - listMinimapCompass[index].transform.position.x;
                            float distanceY = listMinimapCompass[i].transform.position.y - listMinimapCompass[index].transform.position.y;

                            if (distanceX >= 0f)
                            {
                                if (minDistanceY > Mathf.Abs(distanceY))
                                {
                                    minDistanceY = Mathf.Abs(distanceY);
                                    if (minDistanceX > distanceX)
                                    {
                                        minDistanceX = distanceX;
                                        minIndex = i;
                                    }
                                    else minIndex = i;
                                }
                            }
                        }
                    }    
                }
                break;
        }

        if (minIndex != -1)
        {
            updateIndex(minIndex);
        }
        else
        {
            if(direct == DIRECT.right)
            {
                MapControler.changeToInventoryMenu();
            }
        }
    }
}
