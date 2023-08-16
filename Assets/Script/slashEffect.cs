using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using Unity.VisualScripting;
using UnityEngine;

public class slashEffect : MonoBehaviour
{
    public float damage;
    public bool isRight;
    [SerializeField] Animator ani;
    [SerializeField] BaseObject player;
    [SerializeField] CapsuleCollider2D collider;

    [Header("----------Slash----------")]
    [SerializeField] Transform slashComboPoint;
    [SerializeField] Vector2  offsetslashCombo;

    [Header("----------Up slash----------")]
    [SerializeField] Transform upSlashPoint;
    [SerializeField] Vector2 offsetupSlash;

    

    private void Start()
    {
        isRight = false;
    }

    //public void setIsRight(bool isRight)
    //{
    //    this.isRight = isRight;
    //    flip();
    //}

    public void setSlashCombo(int combo)
    {
        collider.offset = offsetslashCombo;
        transform.position = slashComboPoint.position;
        if (combo == 0)
            ani.Play("SlashEffectAlt");
        else ani.Play("SlashEffect");
    }

    public void upSlash()
    {
        collider.offset = offsetupSlash;
        transform.position = upSlashPoint.position;  
        ani.Play("UpSlashEffect");
    }

    public void finishSlash()
    {
        this.gameObject.SetActive(false);
    }

    //public void flip()
    //{
    //    if (isRight)
    //    {
    //        transform.rotation = Quaternion.Euler(0, 180, 0);
    //    }
    //    else transform.rotation = Quaternion.Euler(0, 0, 0);
    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (player.isDead == false)
        {
            if (collision.transform.tag == "Platform" || collision.transform.tag == "Untagged" || collision.transform.tag == "Wall")
            {
                
            }
            else if(collision.transform.tag == "Non-moving object")
            {
                BaseObject obj = collision.transform.GetComponent<BaseObject>();
                
                obj.takeDamage(player.Damage);
            }
            else if (collision.transform.tag != transform.tag)
            {
                BaseObject obj = collision.transform.GetComponent<BaseObject>();
                if (obj == null) obj = collision.transform.GetComponentInParent<BaseObject>();

                Rigidbody2D rb = collision.transform.GetComponent<Rigidbody2D>();

                if (obj.isDead == true) return;

                obj.takeDamage(player.Damage);



                if (obj.isDead == true)
                {
                    rb.AddForce(Vector2.up * GameConstant.collissionForceY, ForceMode2D.Impulse);

                    if (player.isRight)
                    {
                        rb.AddForce(Vector2.right * GameConstant.collisionForceSlash, ForceMode2D.Impulse);
                    }
                    else
                    {
                        rb.AddForce(Vector2.left * GameConstant.collisionForceSlash, ForceMode2D.Impulse);
                    }
                }
                else
                {
                    if (obj.canStun == true) return;
                    if(obj is Vengefly) 
                    {
                        obj.GetComponent<Repel>().repel(player.isRight);
                        return;
                    }

                    if (player.isRight)
                    {
                        //rb.AddForce(Vector2.right * GameConstant.collisionForceSlash, ForceMode2D.Impulse);
                        obj.GetComponent<Repel>().repel(player.isRight);
                    }
                    else
                    {
                        //rb.AddForce(Vector2.left * GameConstant.collisionForceSlash, ForceMode2D.Impulse);
                        obj.GetComponent<Repel>().repel(player.isRight);
                    }
                } 
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Non-moving object")
        {
            BaseObject obj = collision.transform.GetComponent<BaseObject>();
            obj.takeDamage(player.Damage);
        }
    }
}

