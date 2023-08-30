using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuskGuardian_STOMP : AttackSkill
{
    [Header("----------shockWave----------")]
    [SerializeField] shockWave shockWaveFrefabs;
    [SerializeField] Repel repel;


    public override void startAttack()
    {
        base.startAttack(); 
        boss.ani.Play("HuskGuardian_START_STOMP");
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
                repel.repel(!boss.isRight);
                aniEventCount+=1;
                break;
            case 1:
                SoundManager.getInstance().PlaySFXEnemy("Boss_land");

                shockWave shockwave = Instantiate(shockWaveFrefabs, transform.position + new Vector3(0, -1.5f, 0), Quaternion.identity);
                shockwave.isRight = true;

                shockWave shockwave1 = Instantiate(shockWaveFrefabs, transform.position + new Vector3(0, -1.5f, 0), Quaternion.identity);
                shockwave1.isRight = false;

                aniEventCount = 0;
                break;
        } 
    }
}
