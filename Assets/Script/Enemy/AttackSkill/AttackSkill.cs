using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSkill : MonoBehaviour, Subcriber
{
    [SerializeField] public Boss boss;
    [SerializeField] public float damage;
    protected bool isCooldown;
    protected bool isInRange;

    [Header("----------Time----------")]
    [SerializeField] protected float duration;
    [SerializeField] protected float CD;
    protected float timeCDStart;

    [Header("----------Range----------")]
    [SerializeField] public bool isRange;
    [SerializeField] protected float  MaxRange;
    [SerializeField] protected float  MinRange;

    [Header("----------isStopMove----------")]
    public bool stopMove;

    protected int aniEventCount;

    protected virtual void Start()
    {
        
    }

    protected virtual void Update()
    {
        if(boss.isCombat)
        {
            // CD
            if (isCooldown == false)
            {
                if (timeCDStart > CD)
                {
                    isCooldown = true;
                    timeCDStart = 0f;
                }
                else timeCDStart += Time.deltaTime;
            }

            // is In range
            if(boss.Target != null)
            {
                if(isRange == true)
                {
                    float x = Mathf.Abs(boss.Target.position.x - transform.position.x);
                    if (x >= MinRange && x <= MaxRange)
                        isInRange = true;
                    else isInRange = false;
                }
                else isInRange = true;
            }
        }

    }

    public virtual void startAttack()
    {
        if(stopMove)
            boss.isMove = false;
    }

    public virtual void Attack()
    {

    }

    public virtual void stopAttack()
    {
        boss.isMove = true;
        isCooldown = false;
        timeCDStart = 0f;

        boss.isAttack = false;
    }

    public virtual bool isReady()
    {
        if (isCooldown && isInRange)
            return true;
        return false;
    }

    // animation event
    public virtual void aniEvent()
    {

    }

    //subcribe
    public void update(int state)
    {
        if (state == (int)Game_State.Pause)
        {
            enabled = false;
        }
        else enabled = true;
    }
}
