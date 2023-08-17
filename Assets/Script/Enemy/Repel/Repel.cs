using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Repel : MonoBehaviour
{
    [SerializeField] protected float speedStart;
    [SerializeField] protected float accelerationStart;

    [SerializeField] protected Enemy enemy;

    protected bool isrepel = false;
    protected bool isUP = false;

    void Start()
    {

    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (isrepel)
        {
            speedStart += accelerationStart * Time.deltaTime;

            if(isUP)
            {
                enemy.transform.position += new Vector3(0, speedStart, 0) * Time.deltaTime + 0.5f * new Vector3(0, accelerationStart, 0) * Time.deltaTime * Time.deltaTime;
            }
            else enemy.transform.position += new Vector3(speedStart, 0, 0) * Time.deltaTime + 0.5f * new Vector3(accelerationStart, 0, 0) * Time.deltaTime * Time.deltaTime;

            if (accelerationStart * speedStart > 0f)
            {
                stopRepel();
            }
        }
        
    }

    public virtual void repel(bool isRight, bool isUP = false)
    {
        startRepel();
        if (isUP)
        {
            repelUP();
        }
        else if(isRight)
        {
            speedStart = Mathf.Abs(speedStart);
            accelerationStart = -Mathf.Abs(accelerationStart);
        }
        else
        {
            speedStart = -Mathf.Abs(speedStart);
            accelerationStart = Mathf.Abs(accelerationStart);
        }
    }

    public virtual void repelUP()
    {
        isUP = true;

        speedStart = Mathf.Abs(speedStart);
        accelerationStart = -Mathf.Abs(accelerationStart);
    }

    public virtual void startRepel()
    {
        isrepel = true;
    }

    public virtual void stopRepel()
    {
        isrepel = false;
        isUP = false;
    }
}
