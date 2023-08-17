using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rosin : BaseObject
{
    [Header("----------Raycast----------")]
    [SerializeField] LayerMask layerMask;
    [SerializeField] float distance;
    [SerializeField] Collider2D colider;
    [SerializeField] float gravityScale;

    private void FixedUpdate()
    {
        if (Physics2D.BoxCast(colider.bounds.center, colider.bounds.size, 0f, Vector2.down, distance, layerMask))
        {
            rb.gravityScale = gravityScale;
        }
    }

    protected override void Update()
    {
        Debug.DrawRay(transform.position, Vector2.down * distance, Color.yellow);
    }

    //deadth
    public override void Dead()
    {
        isDead = true;
    }
}
