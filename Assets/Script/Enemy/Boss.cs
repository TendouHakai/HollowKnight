using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    [Header("----------COMBAT----------")]
    [SerializeField] protected BoxCollider2D coliderCombat;
    [SerializeField] protected float rangeCombat;

    public bool isCombat;
    [Header("----------ATTACK SKILL----------")]
    [SerializeField] public List<AttackSkill> skillList;
    protected AttackSkill currentAttackSkill;

    protected override void Start()
    {
        base.Start();
        isCombat = false;
    }

    protected override void Update()
    {
        base.Update();

        if(isCombat == true && isAttack==false)
        {
            currentAttackSkill = GetAttackSkill();
            if(currentAttackSkill != null )
            {
                currentAttackSkill.startAttack();
                isAttack = true;
            }
        }
    }

    // ATTACK SKILL
    public AttackSkill GetAttackSkill()
    {
        foreach (var skill in skillList)
        {
            if (skill.isReady())
                return skill;
        }
        return null;
    }

    // ATTACK
    public override void Attack()
    {
        currentAttackSkill.Attack();
    }

}
