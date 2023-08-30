using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : BaseObject
{
    [Header("-------------TIMECOUNT---------------")]
    [SerializeField] float TimeDestroy;
    float timeStart = 0f;
    protected override void Update()
    {
        base.Update();
        if (timeStart > TimeDestroy)
        {
            Destroy(this.gameObject);
            timeStart = 0f;
        }
        else timeStart += Time.deltaTime;
    }

    public override void update(int state)
    {
        base.update(state);
    }
}
