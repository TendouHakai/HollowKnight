using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breakablePlatform : MonoBehaviour
{
    public int countStone;
    [Header("----------Stone frefabs----------")]
    [SerializeField] protected Rigidbody2D stone01;
    [SerializeField] protected Rigidbody2D stone02;
    [SerializeField] protected Rigidbody2D stone03;

    List<Rigidbody2D> listStone = new List<Rigidbody2D>();

    [Header("----------Effect----------")]
    [SerializeField] protected GameObject effectFrefabs;

    void Start()
    {

        listStone.Add(stone01); listStone.Add(stone02); listStone.Add(stone03);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            for (int i = 0; i < countStone; i++)
            {
                Rigidbody2D rb = Instantiate(listStone[Random.Range(0, 3)], transform.position, Quaternion.identity);

                int right = Random.Range(0, 2);

                if (right == 1)
                {
                    rb.AddForce(Vector2.left * Random.Range(5, 20), ForceMode2D.Impulse);
                }
                else
                {
                    rb.AddForce(Vector2.right * Random.Range(5, 20), ForceMode2D.Impulse);
                }

                rb.AddForce(Vector2.up * Random.Range(-3f, 3f), ForceMode2D.Impulse);

                Destroy(rb.gameObject, 3f);
            }

            GameObject effect = Instantiate(effectFrefabs, transform.position, Quaternion.identity);

            Destroy(effect, 1f);

            SoundManager.getInstance().PlaySFXEnemy("Rock_hit");

            Destroy(this.gameObject);
        }
    }
}
