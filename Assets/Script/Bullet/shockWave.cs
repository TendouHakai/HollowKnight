using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shockWave : BaseObject
{
    public float acceleration;
    public float MaxSpeed;

    protected override void Start()
    {
        base.Start();
        if(isRight)
        {
            acceleration = Mathf.Abs(acceleration);
        }
        else acceleration = -Mathf.Abs(acceleration);

        flip();
    }

    public override void Move()
    {
        Speed = Speed + acceleration*Time.deltaTime;
        if(Mathf.Abs(Speed) > MaxSpeed ) Speed = isRight ? MaxSpeed : -MaxSpeed;

        transform.position += new Vector3(Speed, 0,0) * Time.deltaTime + 0.5f * new Vector3(acceleration,0,0) * Time.deltaTime * Time.deltaTime;
    }
}
