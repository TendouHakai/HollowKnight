using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuskBully : Enemy
{
    [Header("----------Attack----------")]
    [SerializeField] protected float rangeAttack;
    protected float timeAttack;
    protected float timeAttackStart;

    [Header("----------Speed----------")]
    [SerializeField] protected float speed_WALK;
    [SerializeField] protected float speed_RUN;

    [Header("----------Collision----------")]
    [SerializeField] protected GameObject collision;

    protected override void Start()
    {
        base.Start();

        Speed = speed_WALK;
        timeAttackStart = 0f;

        timeAttack = rangeAttack *1f / speed_RUN;
    }

    protected override void Update()
    {
        if (isDead) return;
        base.Update();

        ani.SetFloat("SpeedEnemy", Mathf.Abs(velocity.x));
    }

    public override void setState(int state)
    {
        Vector3 temp = velocity;
        if(isDead) return;
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
                //ani.SetTrigger("Attack");
                ani.Play("HuskBully_ANTICIPATE");
                break;
            case (int)STATE_HUSKBULLY.StopAttack:
                ani.SetTrigger("StopAttack");
                break;
            case (int)STATE_HUSKBULLY.Die:
                ani.Play("HuskBully_DEADinAIR");
                collision.SetActive(false);
                this.transform.Find("BoxColiderMoving").GetComponent<BoxCollider2D>().size = new Vector2(1,1);
                temp.x = 0;
                break;
        }
        velocity = temp;
        base.setState(state);
    }

    public override void setTarget(Transform target)
    {
        base.setTarget(target);
        float x = target.position.x - transform.position.x;
        Vector3 temp = velocity;
        temp.x = x > 0f ? 1f : -1f;
        isRight = x > 0f ? true : false;
        velocity = temp;
    }

    public void StartAttack()
    {
        isMove = true;
        Speed = speed_RUN;
        timeAttackStart = 0f;
    }

    public void finishAttack()
    {
        Speed = speed_WALK;
        isAttack = false;
        isTurn = false;
    }

    public override void Attack()
    {
        if (isMove == false) return;
        if (timeAttackStart > timeAttack)
        {
            setState((int)STATE_HUSKBULLY.StopAttack);
            timeAttackStart = 0f;
        }
        else timeAttackStart += Time.deltaTime;
    }

    // dead
    public override void Dead()
    {
        setState((int)STATE_HUSKBULLY.Die);
        base.Dead();
    }
}

public enum STATE_HUSKBULLY
{
    IDLE = 1,
    Walk = 2,
    Turn = GameConstant.ENEMYGROUND_STATE_DETECT_WALL,
    Turn2 = GameConstant.ENEMYGROUND_STATE_NOT_DETECT_GROUND,
    Attack = GameConstant.ENEMYGROUND_STATE_DETECT_PLAYER,
    StopAttack = GameConstant.ENEMYGROUND_STATE_NOT_DETECT_PLAYER,
    Die = 6,
}
