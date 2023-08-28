using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuskGuardian_MeleeAttack : AttackSkill
{
    [SerializeField] GameObject collision;

    public override void startAttack()
    {
        base.startAttack(); 
        boss.ani.Play("HuskGuardian_ATTACK");
    }

    public override void Attack()
    {
        
    }

    public override void stopAttack()
    {
        base.stopAttack();

        boss.ani.Play("HuskGuardian_IDLE");
    }

    // animation event
    public override void aniEvent()
    {
        switch (aniEventCount)
        {
            case 0:
                collision.SetActive(true);
                SoundManager.getInstance().PlaySFXEnemy("Boss_trike_ground");
                aniEventCount += 1;
                break;
            case 1:
                collision.SetActive(false);
                aniEventCount =0;
                break;
        }
    }
}
