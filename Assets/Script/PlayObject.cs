using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayObject : BaseObject
{
    [SerializeField] protected float currentHP;
    [SerializeField] protected float MaxHP;
    [SerializeField] public bool isAttack;

    protected override void Start()
    {
        base.Start();
        currentHP = MaxHP;
        isAttack = false;
        isRight = true;
    }

    protected override void Update()
    {
        base.Update();
    }

    public virtual void Attack()
    {

    }

    public override void takeDamage(float damage)
    {
        currentHP -= damage;

        if(currentHP <= 0 )
        {
            Dead();
        }
    }

    public override void Dead()
    {
        this.isDead = true;
    }
}
