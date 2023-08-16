using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : PlayObject
{
    //[Header("----------Component----------")]
    //[SerializeField] protected Rigidbody2D rb;
    //[SerializeField] protected SpriteRenderer render;
    //[SerializeField] public Animator ani;
    public bool isTurn;

    [Header("----------Frefabs----------")]
    [SerializeField] protected GameObject hitEffect;
    [SerializeField] protected GameObject deathEffect;
    [SerializeField] protected EffectFlasfHit flashEffect;

    [Header("----------GEO----------")]
    [SerializeField] protected int geoCount;
    [SerializeField] protected GameObject geoFrefabs;

    protected override void Start()
    {
        base.Start();

        isRight = true;
        isTurn = false;
 
        velocity = new Vector3 (1, 0, 0);
    }

    protected override void Update()
    {
        if(isDead) return;
        flip();
        base.Update();

        // set tarrget to attack
        if(isAttack && isDead == false)
        {
            Attack();
        }
    }

    // take Damage
    public override void takeDamage(float damage)
    {
        base.takeDamage(damage);
        GameObject effect = null;
        if (isDead)
        {
            effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            SoundManager.getInstance().PlaySFXEnemy("Enemy_die");
        }
        else
        {
            effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            SoundManager.getInstance().PlaySFXEnemy("knight_damage");
            flashEffect.startFlash();
        }

        Destroy(effect, 1f);
    }

    public virtual void FinishTurn()
    {
        isRight = velocity.x > 0f ? true : false;
        flip();
        isTurn = false;
        isMove = true;
    }

    // dead
    public override void Dead()
    {
        base.Dead();
        for (int i = 0; i < geoCount; i++)
        {
            Instantiate(geoFrefabs, transform.position, Quaternion.identity);
        }

        HUDManager.getInstance().upSoul();
    }
}
