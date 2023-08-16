using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crawlid : Enemy
{
    [Header("----------Collision----------")]
    [SerializeField] GameObject collision;

    public override void setState(int state)
    {
        Vector3 temp = velocity;
        switch (state)
        {
            case (int)STATE_CRAWLID.Walk:
                break;
            case (int)STATE_CRAWLID.Turn1:
                isTurn = true;
                temp.x = -velocity.x;
                isMove = false;
                ani.SetTrigger("Turn");
                break;
            case (int)STATE_CRAWLID.Turn2:
                isTurn = true;
                temp.x = -velocity.x;
                isMove = false;
                ani.SetTrigger("Turn");
                break;
            case (int)STATE_CRAWLID.Die:
                ani.Play("Crawlid_DEAD_IN_AIR");
                collision.SetActive(false);
                temp.x = 0;
                break;
        }
        velocity = temp;    
        base.setState(state);
    }

    public override void Dead()
    {
        base.Dead();
        setState((int)STATE_CRAWLID.Die);
    }
}

public enum STATE_CRAWLID
{
    Walk = 1,
    Turn1 = GameConstant.ENEMYGROUND_STATE_DETECT_WALL,
    Turn2 = GameConstant.ENEMYGROUND_STATE_NOT_DETECT_GROUND,
    Die = 2,
}
