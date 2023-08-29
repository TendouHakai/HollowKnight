using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuskHornhead : HuskBully
{
    public override void setState(int state)
    {
        if(isDead) return;
        Vector3 temp = velocity;
        switch (state)
        {
            case (int)STATE_HUSKBULLY.IDLE:

                break;
            case (int)STATE_HUSKBULLY.Walk:
                Speed = speed_WALK;
                break;
            case (int)STATE_HUSKBULLY.Turn:
                isTurn = true;
                temp.x = -velocity.x;
                isMove = false;
                ani.SetTrigger("Turn");
                break;
            case (int)STATE_HUSKBULLY.Turn2:
                isTurn = true;
                temp.x = -velocity.x;
                isMove = false;
                ani.SetTrigger("Turn");
                break;
            case (int)STATE_HUSKBULLY.Attack:
                isAttack = true;
                isMove = false;
                ani.SetTrigger("Attack");
                break;
            case (int)STATE_HUSKBULLY.StopAttack:
                isAttackSkill = false;
                ani.Play("HuskHornhead_END_ATTACK");
                break;
            case (int)STATE_HUSKBULLY.Die:
                ani.Play("HuskHornhead_DEAD_IN_AIR");
                collision.SetActive(false);
                this.transform.Find("BoxColisionMoving").GetComponent<BoxCollider2D>().size = new Vector2(1, 1);
                temp.x = 0;

                isDead = true;
                break;
        }
        velocity = temp;
    }
}
