using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vengefly : Enemy
{
    [Header("----------Move----------")]
    [SerializeField] float timeToChangeDirect;
    [SerializeField] float speed_ATTACK;
    float timeMoveStart;

    [Header("----------Collision----------")]
    [SerializeField] protected GameObject collision;
    [SerializeField] protected float RangeRepel;
    protected Vector3 startPositon;

    protected override void Start()
    {
        base.Start();

        timeMoveStart = 0f;
    }

    public override void setState(int state)
    {
        Vector3 temp = velocity;
        switch (state)
        {
            case (int)STATE_VENGEFLY.TurnX:
                isTurn = true;
                temp.x = - temp.x;
                velocity = temp;
                ani.SetTrigger("Turn");
                break;
            case (int)STATE_VENGEFLY.TurnY:
                temp.y = -temp.y;
                velocity = temp;
                break;
            case (int)STATE_VENGEFLY.Attack:
                Speed = speed_ATTACK;
                isAttack = true;
                isMove = false;
                ani.SetTrigger("Attack");
                break;
            case (int)STATE_VENGEFLY.Die:
                collision.SetActive(false);
                isMove = false;
                rb.gravityScale = 3.5f;
                ani.Play("Vengefly_DEAD");
                break;
        }
        base.setState(state);
    }

    public override void Move()
    { 
        if(Vector3.Distance(startPositon, transform.position) > RangeRepel && startPositon != Vector3.zero)
        {
            rb.velocity = Vector3.zero;
            startPositon = Vector3.zero;
        }

        if(isAttack == false)
        {
            Vector3 temp = velocity;
            if (timeMoveStart > timeToChangeDirect)
            {
                Debug.Log("change");
                temp.x = Random.Range(-1f, 1f);
                temp.y = Random.Range(-1f, 1f);

                if (velocity.x * temp.x < 0f)
                {
                    setState((int)STATE_VENGEFLY.TurnX);
                }
                else isRight = velocity.x > 0f ? true : false;

                velocity = temp;
                timeMoveStart = 0f;
            }
            else timeMoveStart += Time.deltaTime;
        }
        base.Move();
    }

    public override void Attack()
    {
        Vector3 temp = Target.position - transform.position;
        isRight = velocity.x > 0f ? true : false;
        flip();
        velocity = temp.normalized;
    }

    public void StartAttack()
    {
        isMove = true;
    }

    // take damage
    public override void takeDamage(float damage)
    {
        base.takeDamage(damage);

        if(isDead == false)
        {
            //startPositon = transform.position;
        }
    }

    // dead
    public override void Dead()
    {
        base.Dead();
        setState((int)STATE_VENGEFLY.Die);
    }
}

public enum STATE_VENGEFLY
{
    TurnX=GameConstant.ENEMYFLY_STATE_TURNX,
    TurnY=GameConstant.ENEMYFLY_STATE_TURNY,
    Die = 1,
    Attack = GameConstant.ENEMYGROUND_STATE_DETECT_PLAYER,
}
