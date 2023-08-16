using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isGroundCheckPlayer : MonoBehaviour
{
    [SerializeField] protected BaseObject player;
    [SerializeField] protected Collider2D coll;
    [SerializeField] protected LayerMask layerMask;

    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (collision != null && collision.transform.tag == "Platform")
    //    {
    //        player.isGrounded = true;
    //        player.ani.SetBool("IsInAir", false);
    //    }
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision != null && collision.transform.tag == "Platform")
    //    {
    //        player.isGrounded = false;
    //        player.ani.SetBool("IsInAir", true);
    //    }
    //}

    protected virtual void Update()
    {
        checkIsGround();
    }

    public void checkIsGround()
    {
        bool isGround = player.isGrounded;
        player.isGrounded = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, layerMask);

        if (isGround == player.isGrounded) return;
        if(player.isGrounded)
        {
            NotIsInAir();
        }
        else
        {
            IsInAir();
        }
    }

    public virtual void NotIsInAir()
    {
        player.ani.SetBool("IsInAir", false);
        player.setState((int)STATE_PLAYER.Land);
    }

    public virtual void IsInAir()
    {
        player.ani.SetBool("IsInAir", true);
    }
}
