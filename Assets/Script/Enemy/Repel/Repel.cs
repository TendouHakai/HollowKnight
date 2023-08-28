using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Repel : MonoBehaviour
{
    [SerializeField] protected float speedStart;
    [SerializeField] protected float accelerationStart;

    [SerializeField] protected BaseObject obj;

    protected bool isrepel = false;
    protected bool isUP = false;

    float speed;
    float a;

    void Start()
    {

    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (isrepel)
        {
            speed += a * Time.deltaTime;

            if(isUP)
            {
                obj.transform.position += new Vector3(0, speed, 0) * Time.deltaTime + 0.5f * new Vector3(0, a, 0) * Time.deltaTime * Time.deltaTime;
            }
            else obj.transform.position += new Vector3(speed, 0, 0) * Time.deltaTime + 0.5f * new Vector3(a, 0, 0) * Time.deltaTime * Time.deltaTime;

            if (a * speed > 0f)
            {
                stopRepel();
            }
        }
        
    }

    public virtual void repel(bool isRight, bool isUP = false, bool isPlayer = false)
    {
        if (isUP)
        {
            repelUP(isPlayer);
        }
        else if(isRight)
        {
            speed = Mathf.Abs(speedStart);
            a = -Mathf.Abs(accelerationStart);
        }
        else
        {
            speed = -Mathf.Abs(speedStart);
            a = Mathf.Abs(accelerationStart);
        }
        startRepel();
    }

    public virtual void repelUP(bool isplayer = false)
    {
        isUP = true;
        obj.rb.velocity = Vector2.zero;

        if(isplayer)
        {
            speed = -Mathf.Abs(speedStart);
            a = Mathf.Abs(accelerationStart);
        }
        else
        {
            speed = Mathf.Abs(speedStart);
            a = -Mathf.Abs(accelerationStart);
        }
    }

    public virtual void startRepel()
    {
        isrepel = true;
        obj.isMove = false;
    }

    public virtual void stopRepel()
    {
        isrepel = false;
        isUP = false;
        obj.isMove = true;
    }
}
