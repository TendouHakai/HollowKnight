using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectZoomAnimation : MonoBehaviour
{
    [SerializeField] protected RectTransform UI;

    Vector3 speed;
    protected Vector3 DesPosition;

    
    Vector2 DesSize;
    Vector2 speedZoom;

    public bool isRun;
    public float timeRun = 0f;
    float timeStart = 0f;

    protected void Start()
    {
        isRun = false;
    }

    protected void Update()
    {
        if(isRun)
        {
            if (timeStart > timeRun)
            {
                UI.position = DesPosition;
                UI.sizeDelta = DesSize;


                isRun = false;
                timeStart = 0f;
            }
            else
            {
                timeStart += Time.deltaTime;
                UI.position += speed * Time.deltaTime;
                UI.sizeDelta += speedZoom * Time.deltaTime;  
            }
        }
    }

    public void runAni(Vector3 DesPosition, Vector2 DesSize)
    {
        this.DesPosition = DesPosition;
        this.DesSize = DesSize;

        Vector3 tempP = DesPosition - UI.position;  
        speed = tempP / timeRun;


        Vector2 tempS = DesSize - UI.sizeDelta;
        speedZoom = tempS / timeRun;

        timeStart = 0f;
        isRun = true;
    }
}
