using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : Door
{
    [Header("----------Stone count----------")]
    [SerializeField] int HP;
    [SerializeField] int takedameStonecount;

    [Header("----------Portal----------")]
    [SerializeField] GameObject portalFrefab;

    public override void takeDamage(float damage)
    {
        HP -= 1;

        int count = takedameStonecount;
        if (HP <= 0)
            count = countStone;

        for (int i = 0; i < count; i++)
        {
            int right = Random.Range(0, 2);
            Rigidbody2D rb = Instantiate(listStone[Random.Range(0, 2)], transform.position, Quaternion.identity);

            if (right == 0)
            {
                rb.AddForce(Vector2.left * Random.Range(5, 20), ForceMode2D.Impulse);
            }
            else
            {
                rb.AddForce(Vector2.right * Random.Range(5, 20), ForceMode2D.Impulse);
            }

            rb.AddForce(Vector2.up * Random.Range(1f, 5f), ForceMode2D.Impulse);

            Destroy(rb.gameObject, 2f);
        }

        GameObject effect = Instantiate(effectFrefabs, transform.position, Quaternion.identity);

        Destroy(effect, 1f);

        SoundManager.getInstance().PlaySFXEnemy("Rock_hit");

        if (HP <= 0)
        {
            Instantiate(portalFrefab, transform.position + new Vector3(0,-2,0), Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
