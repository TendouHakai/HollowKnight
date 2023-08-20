using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCollission : MonoBehaviour
{
    [SerializeField] protected BaseObject Bobj;

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (Bobj.isDead == false)
        {
            if (collision.tag == "Platform" || collision.tag == "Untagged")
            {

            }
            else if (collision.tag !=transform.tag)
            {
                BaseObject obj = collision.GetComponent<BaseObject>();
                if(obj == null) obj = collision.GetComponentInParent<BaseObject>();

                if (obj == null || obj.isDead == true) return;

                Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();

                obj.takeDamage(Bobj.Damage);

                if (rb == null || obj.isDead == true) return;

                if (Bobj.isRight)
                {
                    rb.AddForce(Vector2.right * GameConstant.collissionForceX, ForceMode2D.Impulse);
                    rb.AddForce(Vector2.up * GameConstant.collissionForceY, ForceMode2D.Impulse);
                }
                else
                {
                    rb.AddForce(Vector2.left * GameConstant.collissionForceX, ForceMode2D.Impulse);
                    rb.AddForce(Vector2.up * GameConstant.collissionForceY, ForceMode2D.Impulse);
                }
            }
        }
    }

    protected virtual void OnTriggerStay2D(Collider2D collision)
    {
        if (Bobj.isDead == false)
        {
            if (collision.tag == "Platform" || collision.tag == "Untagged")
            {

            }
            else if (collision.tag != transform.tag)
            {
                BaseObject obj = collision.GetComponent<BaseObject>();
                if (obj == null) obj = collision.GetComponentInParent<BaseObject>();

                if (obj == null || obj.isDead == true) return;

                Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();

                obj.takeDamage(Bobj.Damage);

                if (rb == null || obj.isDead == true) return;

                if (Bobj.isRight)
                {
                    rb.AddForce(Vector2.right * GameConstant.collissionForceX, ForceMode2D.Impulse);
                    rb.AddForce(Vector2.up * GameConstant.collissionForceY, ForceMode2D.Impulse);
                }
                else
                {
                    rb.AddForce(Vector2.left * GameConstant.collissionForceX, ForceMode2D.Impulse);
                    rb.AddForce(Vector2.up * GameConstant.collissionForceY, ForceMode2D.Impulse);
                }
            }
        }
    }
}
