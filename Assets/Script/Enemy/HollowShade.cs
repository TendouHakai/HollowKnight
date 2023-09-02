using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HollowShade : Boss
{
    [Header("-------------COIN-----------------")]
    [SerializeField] int coin;
    [SerializeField] int soul;
    protected override void Start()
    {
        base.Start();
        setState((int)STATE_HOLLOWSHADE.IDLE);
        isMove = false;
    }

    protected override void Update()
    {
        if (isDead) return;
        base.Update();
        ani.SetFloat("SpeedEnemy", Mathf.Abs(velocity.x * Speed));
    }

    public override void setState(int state)
    {
        if (isDead) return;
        Vector3 temp = velocity;
        switch (state)
        {
            case (int)STATE_HOLLOWSHADE.COMBAT:
                if(isCombat == false)
                {
                    isCombat = true;
                    isMove = false;
                    updateDirect();
                    ani.Play("HollowShade_CAST");
                }
                break;
            case (int)STATE_HOLLOWSHADE.IDLE:
                temp.x = 0;
                temp.y = 0;
                break;
            case (int)STATE_HOLLOWSHADE.Turn:
                isTurn = true;
                temp.x = -temp.x;
                ani.SetTrigger("Turn");
                break;
            case (int)STATE_HOLLOWSHADE.Die:
                if (isAttack) stopAttack();
                ani.Play("HollowShade_DEATH");
                SaveLoadSystem.SaveHUDData(HUDManager.getInstance());
                SaveLoadSystem.deleteHollowShadeData();
                break;
        }
        velocity = temp;
        base.setState(state);
    }

    public override void Move()
    {
        if (isCombat == true)
        {
            Vector3 temp = Target.position - transform.position;

            if (temp.x * velocity.x < 0f)
            {
                setState((int)STATE_HOLLOWSHADE.Turn);
            }
            else
            {
                isRight = velocity.x > 0f ? true : false;
                flip();
                velocity = temp.normalized;
            }
        }
        base.Move();
    }

    public void updateDirect()
    {
        if (isTurn) return;
        float x = Target.position.x - transform.position.x;
        Vector3 temp = velocity;
        temp.x = x > 0f ? 1f : -1f;
        isRight = x > 0f ? true : false;
        velocity = temp;
    }

    // dead
    public override void Dead()
    {
        setState((int)(STATE_HOLLOWSHADE.Die));
        GameStateManager.getInstance().publisherGameState.unsubcribe(this);
        isDead = true;
    }

    public void endDeath()
    {
        HUDManager.getInstance().upSoul(soul);
        HUDManager.getInstance().addCoin(coin);
        Destroy(this.gameObject);
    }

    //combat
    public void stopAttack()
    {
        currentAttackSkill.stopAttack();
        isTurn = false;
    }

    public void startCombat()
    {
        isMove = true;
    }
}

public enum STATE_HOLLOWSHADE
{
    COMBAT = GameConstant.ENEMYGROUND_STATE_DETECT_PLAYER,
    IDLE = 0,
    Turn = 1,
    Die = 2,
}
