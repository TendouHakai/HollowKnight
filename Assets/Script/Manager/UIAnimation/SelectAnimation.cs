using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectAnimation : MonoBehaviour
{
    [SerializeField] protected RectTransform UI;

    [SerializeField] protected DIRECT direct;
    [SerializeField] protected float speed;
    
    [SerializeField] protected float DesPosition;
    protected Vector3 velocity;

    protected bool isRun;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        isRun = false;
        setDirect(direct);
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        Debug.Log("current: " + UI.position);
        if(isRun)
        {
            Vector3 pos = UI.position;
            switch (direct)
            {
                case DIRECT.up:
                    if (UI.position.y < DesPosition)
                    {
                        pos.y = DesPosition;
                        UI.position = pos;

                        isRun = false;
                        return;
                    }
                    break;
                case DIRECT.down:
                    if (UI.position.y > DesPosition)
                    {
                        pos.y = DesPosition;
                        UI.position = pos;
                        Debug.Log("finish:"+ UI.position);

                        isRun = false;
                        return;
                    }
                    break;
                case DIRECT.left:
                    if (UI.position.x < DesPosition)
                    {
                        pos.x = DesPosition;
                        UI.position = pos;

                        isRun = false;
                        return;
                    }
                    break;
                case DIRECT.right:
                    if (UI.position.x > DesPosition)
                    {
                        pos.x = DesPosition;
                        UI.position = pos;

                        isRun = false;
                        return;
                    }
                    break;
            }
            UI.position += speed * velocity;
        }
    }

    public void setDirect(DIRECT direct)
    {
        switch (direct)
        {
            case DIRECT.up:
                velocity = new Vector3 (0, -1, 0);
                break;
            case DIRECT.down:
                velocity = new Vector3(0, 1, 0);
                break;
            case DIRECT.left:
                velocity = new Vector3(-1, 0, 0);
                break;
            case DIRECT.right:
                velocity = new Vector3(1, 0, 0);
                break;
        }
        this.direct = direct;
    }

    public void runAnimation(DIRECT direct, float DesPosition)
    {
        Debug.Log(UI.position);
        Debug.Log(DesPosition); 
        setDirect(direct);
        this.DesPosition = DesPosition;

        isRun = true;
    }
}

public enum DIRECT
{
    up = 1,
    down = 2,
    left = 3,
    right = 4,
}
