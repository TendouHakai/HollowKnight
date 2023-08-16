using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepelGroundEnemy : Repel
{
    public override void startRepel()
    {
        base.startRepel();

        enemy.isMove = false;    
    }

    public override void stopRepel()
    {
        base.stopRepel(); 

        enemy.isMove = true;
        if(enemy.isDead== false)
            enemy.setState((int)STATE_HUSKBULLY.StopAttack);
    }
}
