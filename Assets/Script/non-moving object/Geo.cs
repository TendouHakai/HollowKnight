using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Geo : BaseObject
{
    [Header("----------Coin----------")]
    [SerializeField] int coin;
    public bool canCollect;
    public bool isCollect;

    protected override void Start()
    {
        canCollect = false;
        isCollect = false;

        rb.AddForce(Vector2.up * Random.Range(1,3), ForceMode2D.Impulse);

        int right = Random.Range(0, 2);
        if(right == 0)
        {
            rb.AddForce(Vector2.left * Random.Range(1, 8), ForceMode2D.Impulse);
        }
        else
        {
            rb.AddForce(Vector2.right * Random.Range(1, 8), ForceMode2D.Impulse);
        }
    }

    protected override void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(canCollect == true && isCollect == false)
        {
            if(collision.tag == "Player")
            {
                ani.Play("Geo_COLLECT");
                isCollect = true;

                SoundManager.getInstance().PlaySFXEnemy("Geo_Collect");
                HUDManager.getInstance().addCoin(coin);
            }
        }
    }

    // isCollect
    public void Finishcollect()
    {
        Destroy(gameObject);
    }
}
