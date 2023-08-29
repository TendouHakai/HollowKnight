using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gruzzer : Enemy
{
    [Header("----------Fly----------")]
    [SerializeField] float angle;

    [Header("----------Collision----------")]
    [SerializeField] GameObject collision;

    protected override void Start()
    {
        base.Start();

        velocity = new Vector3 (Mathf.Cos(angle*Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0);
    }
    public override void setState(int state)
    {
        if(isDead) return;
        Vector3 temp = velocity;
        switch (state)
        {
            case ((int)STATE_GRUZZER.TurnX):
                temp.x = -temp.x;
                isRight = temp.x > 0f ? true : false;
                break;
            case ((int)STATE_GRUZZER.TurnY):
                temp.y = -temp.y;
                break;
            case (int)STATE_GRUZZER.Die:
                ani.Play("Gruzzer_DEAD_IN_AIR");
                collision.SetActive(false);
                temp.x = 0;
                temp.y = 0;

                rb.gravityScale = 3.5f;
                isDead = true;
                break;
        }
        velocity = temp;
        base.setState(state);
    }

    // dead
    public override void Dead()
    {
        setState((int)STATE_GRUZZER.Die);
        base.Dead();
    }

    public void deadFinish()
    {
        Destroy(this.gameObject);
    }
}

public enum STATE_GRUZZER
{
    TurnX = GameConstant.ENEMYFLY_STATE_TURNX,
    TurnY = GameConstant.ENEMYFLY_STATE_TURNY,
    Die = 2,
}
