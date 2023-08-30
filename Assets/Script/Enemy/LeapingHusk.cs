using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LeapingHusk : Enemy
{
    [Header("----------Jump----------")]
    [SerializeField] private float jumpHeight;
    private float jumpForce;

    [Header("----------Collision----------")]
    [SerializeField] GameObject collision;

    protected override void Start()
    {
        base.Start();

        jumpForce = Mathf.Sqrt(jumpHeight * (Physics2D.gravity.y * rb.gravityScale) * (-2)) * rb.mass;
    }

    protected override void Update()
    {
        if(isDead) return;
        base.Update();

        ani.SetFloat("speedEnemy", Mathf.Abs(velocity.x));
    }

    public override void setState(int state)
    {
        if(isDead) return;
        Vector3 temp = velocity;
        if (isDead)  return; 
        switch (state)
        {
            case (int)STATE_LEAPINGHUSK.Turn:
                temp.x = -velocity.x;
                velocity = temp;
                isMove = false;
                isTurn = true;

                ani.SetTrigger("Turn");
                break;
            case (int)STATE_LEAPINGHUSK.Turn2:
                temp.x = -velocity.x;
                velocity = temp;
                isMove = false;
                isTurn = true;

                ani.SetTrigger("Turn");
                break;
            case (int)STATE_LEAPINGHUSK.Attack:
                ani.SetTrigger("Attack");
                isMove = false;
                isAttack = true;
                break;
            case (int)STATE_LEAPINGHUSK.Die:
                isDead = true;
                ani.Play("LeapingHusk_DEAD_IN_AIR");
                collision.SetActive(false);
                this.transform.Find("BoxCollisionMoving").GetComponent<BoxCollider2D>().size = new Vector2(1, 0.8f);
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
        Speed = Mathf.Abs(x / 1.002f);
        Vector3 temp = velocity;
        temp.x = x > 0 ? 1f : -1f;
        
        isRight = x > 0 ? true : false;
        velocity = temp;
    }

    public override void FinishTurn()
    {
        isTurn = false;
        isMove = true;
        isRight = !isRight;
        flip();
    }

    public void startAttack()
    {
        isMove = true;
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    public void notMove()
    {
        isMove = false;
    }

    public void FinishAttack()
    {
        Speed = 1f;
        isAttack = false;
    }

    // dead
    public override void Dead()
    {
        setState((int)STATE_LEAPINGHUSK.Die);
        base.Dead();
    }
}

public enum STATE_LEAPINGHUSK
{
    Turn = GameConstant.ENEMYGROUND_STATE_DETECT_WALL,
    Turn2 = GameConstant.ENEMYGROUND_STATE_NOT_DETECT_GROUND,
    Attack = GameConstant.ENEMYGROUND_STATE_DETECT_PLAYER,
    Die = 1,
}