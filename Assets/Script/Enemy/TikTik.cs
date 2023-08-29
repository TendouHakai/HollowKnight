using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TikTik : Enemy
{
    State_Turn_Tiktik state_Turn;
    Quaternion rotationTemp;

    [Header("----------Collision----------")]
    [SerializeField] protected GameObject collision;

    [Header("----------Stun----------")]
    [SerializeField] protected float timeStun;
    [SerializeField] protected GameObject StunEffect;
    float timeStunStart;

    protected override void Start()
    {
        base.Start();

        state_Turn = State_Turn_Tiktik.Right;

        rotationTemp = transform.rotation;  
    }

    protected override void Update()
    {
        if(isDead) return;
        rb.velocity = Vector2.zero;
        if (isStun)
        {
            if(timeStunStart > timeStun)
            {
                setState((int)STATE_TIKTIK.Walk);
                timeStunStart = 0f;
                isStun = false;
            }
            else timeStunStart += Time.deltaTime;

            return; 
        }
        if(isTurn)
        {
            if(transform.rotation.eulerAngles.z >= rotationTemp.eulerAngles.z -1.0f && transform.rotation.eulerAngles.z <= rotationTemp.eulerAngles.z + 1.0f)
            {
                isTurn = false;
            }
            else transform.rotation = Quaternion.Lerp(transform.rotation, rotationTemp, Time.deltaTime * 5);
        }
        
        if(isMove)
            Move();
    }

    public override void setState(int state)
    {
        if (isDead) return;
        Vector3 temp = velocity;
        switch (state)
        {
            case (int)STATE_TIKTIK.Turn1:
                //temp.x = -temp.x;
                //velocity = temp;
                //if(velocity.x > 0)
                //{
                //    isRight = true;
                //}
                //else isRight = false;

                isTurn = true;
                switch (state_Turn)
                {
                    case State_Turn_Tiktik.Right:
                        //temp.x = 0;
                        temp.y = 1;
                        state_Turn = State_Turn_Tiktik.Up;
                        transform.rotation = Quaternion.Euler(0, 180, 360);
                        rotationTemp = Quaternion.Euler(0, 180, 270);
                        break;
                    case State_Turn_Tiktik.Down:;
                        //temp.y = 0;
                        temp.x = 1;
                        state_Turn = State_Turn_Tiktik.Right;
                        rotationTemp = Quaternion.Euler(0, 180, 0);
                        break;
                    case State_Turn_Tiktik.Left:
                        //temp.x = 0;
                        temp.y = -1;
                        rotationTemp = Quaternion.Euler(0, 180, 90);
                        state_Turn = State_Turn_Tiktik.Down;
                        break;
                    case State_Turn_Tiktik.Up:
                        //temp.y = 0;
                        temp.x = -1;
                        state_Turn = State_Turn_Tiktik.Left;
                        rotationTemp = Quaternion.Euler(0, 180, 180);
                        break;
                }

                velocity = temp;
                break;
            case (int)STATE_TIKTIK.Turn2:
                isTurn = true;
                switch (state_Turn)
                {
                    case State_Turn_Tiktik.Right:
                        temp.x = 0;
                        temp.y = -1;
                        rotationTemp = Quaternion.Euler(0, 180, 90);
                        state_Turn = State_Turn_Tiktik.Down;
                        break;
                    case State_Turn_Tiktik.Down:
                        temp.y = 0;
                        temp.x = -1;
                        state_Turn = State_Turn_Tiktik.Left;
                        rotationTemp = Quaternion.Euler(0, 180, 180);
                        break;
                    case State_Turn_Tiktik.Left:
                        temp.x = 0;
                        temp.y = 1;
                        state_Turn = State_Turn_Tiktik.Up;
                        rotationTemp = Quaternion.Euler(0, 180, 270);
                        break;
                    case State_Turn_Tiktik.Up:
                        temp.y = 0;
                        temp.x = 1;
                        state_Turn = State_Turn_Tiktik.Right;
                        rotationTemp = Quaternion.Euler(0, 180, 0);
                        break;
                }

                velocity = temp;
                break;
            case (int)STATE_TIKTIK.Die:
                ani.Play("TikTik_DEAD_IN_AIR");
                collision.SetActive(false);
                rb.gravityScale = 3.5f;
                isMove = false;
                isDead = true;
                break;
            case (int)STATE_TIKTIK.Stun:
                ani.Play("TikTik_STUN");
                isMove = false;
                isStun = true;
                timeStunStart = 0f;
                break;
            case (int)STATE_TIKTIK.Walk:
                ani.Play("TikTik_WALK");
                isMove = true;
                break;

        }
        base.setState(state);
    }

    // dead
    public override void Dead()
    {
        setState((int)STATE_TIKTIK.Die);
        base.Dead();
    }

    // take damge
    public override void takeDamage(float damage)
    {
        GameObject effect = null;
        if (isStun == false)
        {
            currentHP -= damage;
            if (currentHP <= 0)
            {
                Dead();
            }

            if (isDead)
            {
                effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
                SoundManager.getInstance().PlaySFXEnemy("Enemy_die");
            }
            else
            {
                setState((int)STATE_TIKTIK.Stun);

                effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
                SoundManager.getInstance().PlaySFXEnemy("knight_damage");
                flashEffect.startFlash();
            }
        }
        else SoundManager.getInstance().PlaySFXEnemy("knight_attackSwordReject");

        Destroy(effect, 1f);
    }
}


public enum State_Turn_Tiktik
{
    Right = 1,
    Down = 2,
    Left = 3,
    Up = 4,
}

public enum STATE_TIKTIK
{
    Turn1 = GameConstant.ENEMYGROUND_STATE_DETECT_WALL,
    Turn2 = GameConstant.ENEMYGROUND_STATE_NOT_DETECT_GROUND,
    Die = 1,
    Stun = 2,
    Walk = 3,
}
