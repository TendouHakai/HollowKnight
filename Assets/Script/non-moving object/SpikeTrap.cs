using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : BaseObject
{
    [SerializeField] Transform pointRevival;

    protected override void Start()
    {
        
    }

    protected override void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            BaseObject obj = collision.GetComponent<BaseObject>();

            obj.takeDamage(Damage);


            collision.transform.position = pointRevival.position;
        }
    }
}
