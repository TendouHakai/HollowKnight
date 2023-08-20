using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuskGuardian_STOMP : AttackSkill
{
    [Header("----------shockWave----------")]
    [SerializeField] shockWave shockWaveFrefabs;

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
        shockWave shockwave = Instantiate(shockWaveFrefabs, transform.position + new Vector3(0,-1.5f,0), Quaternion.identity);
        shockwave.isRight = true;
        Destroy(shockwave.gameObject, 4f);

        shockWave shockwave1 = Instantiate(shockWaveFrefabs, transform.position + new Vector3(0, -1.5f, 0), Quaternion.identity);
        shockwave1.isRight = false;
        Destroy(shockwave1.gameObject, 4f);
    }
}
