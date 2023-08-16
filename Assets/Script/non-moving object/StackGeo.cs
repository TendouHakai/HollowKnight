using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackGeo : BaseObject
{
    [Header("----------GEO----------")]
    [SerializeField] int geoStackCount;
    [SerializeField] int geoCountperStack;
    [SerializeField] GameObject geoFrefabs;

    [Header("----------Effect----------")]
    [SerializeField] protected GameObject effectFrefabs;

    public override void takeDamage(float damage)
    {
        geoStackCount -= 1;
        for (int i = 0; i < geoCountperStack; i++)
        {
            Instantiate(geoFrefabs, transform.position, Quaternion.identity);
            SoundManager.getInstance().PlaySFXEnemy("knight_attackSwordReject");
        }
        if (geoStackCount <= 0)
        {
            Dead();
        }
    }

    public override void Dead()
    {
        GameObject effect = Instantiate(effectFrefabs, transform.position, Quaternion.identity);

        Destroy(effect, 1f);
        Destroy(this.gameObject);
    }
}
