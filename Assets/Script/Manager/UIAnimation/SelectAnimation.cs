using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectAnimation : MonoBehaviour
{
    [SerializeField] GameObject UI;

    [SerializeField] float speed;
    [SerializeField] float acceleration;
    [SerializeField] DIRECT direct;

    [SerializeField] float DesPosition;
    Vector3 velocity;

    bool isRun;
    // Start is called before the first frame update
    void Start()
    {
        isRun = false;
        setDirect(direct);
    }

    // Update is called once per frame
    void Update()
    {
        if(isRun)
        {
            Vector3 pos = UI.transform.position;
            switch (direct)
            {
                case DIRECT.up:
                    if (UI.transform.position.y < DesPosition)
                    {
                        pos.y = DesPosition;
                        UI.transform.position = pos;

                        isRun = false;
                        return;
                    }
                    break;
                case DIRECT.down:
                    if (UI.transform.position.y > DesPosition)
                    {
                        pos.y = DesPosition;
                        UI.transform.position = pos;

                        isRun = false;
                        return;
                    }
                    break;
                case DIRECT.left:
                    if (UI.transform.position.x < DesPosition)
                    {
                        pos.x = DesPosition;
                        UI.transform.position = pos;

                        isRun = false;
                        return;
                    }
                    break;
                case DIRECT.right:
                    if (UI.transform.position.x > DesPosition)
                    {
                        pos.x = DesPosition;
                        UI.transform.position = pos;

                        isRun = false;
                        return;
                    }
                    break;
            }
            UI.transform.position += speed * velocity;
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
