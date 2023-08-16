using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : BaseObject
{
    public int countStone;
    [Header("----------Stone frefabs----------")]
    [SerializeField] protected Rigidbody2D stone01;
    [SerializeField] protected Rigidbody2D stone02;
    [SerializeField] protected Rigidbody2D stone03;

    List<Rigidbody2D> listStone = new List<Rigidbody2D>();

    [Header("----------Effect----------")]
    [SerializeField] protected GameObject effectFrefabs;

    protected override void Start()
    {
        isDead = true;

        listStone.Add(stone01); listStone.Add(stone02); listStone.Add(stone03);
    }

    public override void takeDamage(float damage)
    {
        for(int i = 0; i < countStone; i++)
        {
            Rigidbody2D rb = Instantiate(listStone[Random.Range(0,2)], transform.position, Quaternion.identity);

            if (isRight)
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

        Destroy(this.gameObject);
    }
}
