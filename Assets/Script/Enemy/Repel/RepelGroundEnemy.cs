using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepelGroundEnemy : Repel
{
    public override void stopRepel()
    {
        base.stopRepel(); 
        if(obj.isDead== false)
            obj.setState((int)STATE_HUSKBULLY.StopAttack);
    }
}
