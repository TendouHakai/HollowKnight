using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class HuskGuard : Boss
{
    [Header("----------sleep----------")]
    [SerializeField] Vector3 SleepPosition;
    [SerializeField] protected float rangeSleep;

    [Header("----------Speed----------")]
    [SerializeField] protected float speed_WALK;
    [SerializeField] protected float speed_RUN;

    protected override void Start()
    {
        base.Start();

        SleepPosition = transform.position;

        State = (int)STATE_HUSKGUARD.SLEEP;
    }
        
    protected override void Update()
    {
        if(isDead) return;
        base.Update();
        ani.SetFloat("SpeedEnemy", Mathf.Abs(velocity.x * Speed));
    }

    public override void setState(int state)
    {
        if(isDead) return;
        Vector3 temp = velocity;
        if (this.State == state) return;
        switch (state)
        {
            case (int)STATE_HUSKGUARD.IDLE:
                temp.x = 0;
                isMove = false;
                isTurn = false; 
                ani.Play("HuskGuardian_IDLE");
                break;
            case (int)STATE_HUSKGUARD.WALK:
                Speed = speed_WALK;
                break;
            case (int)STATE_HUSKGUARD.RUN:
                Speed = speed_RUN;
                break;
            case (int)STATE_HUSKGUARD.COMBAT:
                if (this.State == (int)STATE_HUSKGUARD.SLEEP)
                {
                    setState((int)STATE_HUSKGUARD.WAKE);
                    return;
                }
                else if (this.State == (int)STATE_HUSKGUARD.WAKE) return;

                Speed = speed_RUN;
                isCombat = true;
                isMove = true;
                coliderCombat.size = new Vector2(rangeCombat, coliderCombat.bounds.size.y) ;
                break;
            case (int)STATE_HUSKGUARD.STOP_COMBAT:
                if (isCombat == false) return;
                isCombat = false;
                isTurn = true;
                Speed = speed_WALK;

                break;
            case (int)STATE_HUSKGUARD.SLEEP:
                break;
            case (int)STATE_HUSKGUARD.WAKE:
                ani.Play("HuskGuardian_WAKE");
                SoundManager.getInstance().PlayMusic("Boss_battle_music");
                break;
            default:
                return;
        }
        velocity = temp;
        base.setState(state);
    }

    public override void setTarget(Transform target)
    {
        base.setTarget(target);
        updateDirect();
    }

    public void updateDirect()
    {
        if (isTurn) return;
        float x = 0;

        if (isCombat){
            x = Target.position.x - transform.position.x;
        }
        else
        {
            x = SleepPosition.x - transform.position.x;
        }
        Vector3 temp = velocity;
        temp.x = x > 0f ? 1f : -1f;
        isRight = x > 0f ? true : false;
        velocity = temp;
    }

    public override void Move()
    {
        updateDirect();

        if (isCombat)
        {

        }
        else
        {
            if (Mathf.Abs(SleepPosition.x - transform.position.x) <= 0.1f && this.State != (int)STATE_HUSKGUARD.SLEEP && this.State != (int)STATE_HUSKGUARD.WAKE)
            {
                setState((int)STATE_HUSKGUARD.IDLE);
            }
        }
        base.Move();
    }

    // death
    public override void Dead()
    {
        base.Dead();
        SoundManager.getInstance().PlayMusic("AbyssMusic01");
        ani.Play("HuskGuardian_DEAD_IN_AIR");
    }

    // WAKE 
    public void finishWake()
    {
        setState((int)STATE_HUSKGUARD.IDLE);
    }

    // function for attack skill
    public void AniEventAttack()
    {
        currentAttackSkill.aniEvent();
    }

    public void stopAttack()
    {
        currentAttackSkill.stopAttack();
        setState((int)STATE_HUSKGUARD.STOP_COMBAT);
        isTurn = false;
    }
}

public enum STATE_HUSKGUARD
{
    IDLE= 1, 
    WALK=2,
    RUN=3,
    COMBAT= GameConstant.ENEMYGROUND_STATE_DETECT_PLAYER,
    STOP_COMBAT= GameConstant.ENEMYGROUND_STATE_NOT_DETECT_PLAYER,
    SLEEP=6,
    WAKE=7,
}
