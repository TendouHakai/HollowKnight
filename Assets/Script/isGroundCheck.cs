using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isGroundCheck : MonoBehaviour
{
    [SerializeField] protected BaseObject obj;
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
        bool isGround = obj.isGrounded;
        obj.isGrounded = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, layerMask);

        if (isGround == obj.isGrounded) return;
        if(obj.isGrounded)
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
        obj.ani.SetBool("IsInAir", false);
    }

    public virtual void IsInAir()
    {
        obj.ani.SetBool("IsInAir", true);
    }
}
