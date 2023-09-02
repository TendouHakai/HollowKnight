using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HollowShade_MeleeAttack : AttackSkill
{
    [SerializeField] GameObject collision;
    public override void startAttack()
    {
        base.startAttack();
        collision.SetActive(true);
        boss.ani.Play("HollowShade_SLASH");
    }

    public override void stopAttack()
    {
        base.stopAttack();
        collision.SetActive(false);
        boss.ani.Play("HollowShade_IDLE");
    }

    public override void aniEvent()
    {
        base.aniEvent();
    }
}
