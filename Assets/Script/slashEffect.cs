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

    [Header("---------Repel--------------")]
    [SerializeField] Repel repel;
    bool isUP = false;

    bool isAttacked = false;

    

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

        isUP = true;
    }

    public void finishSlash()
    {
        isAttacked = false;
        isUP = false;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {     
        //if (isAttacked)
        //{
        //    return;
        //}
        //else
        //{
        //    isAttacked = true;
        //}

        if (player.isDead == false)
        {
            if(collision.tag== "Untagged")
            {

            }
            else if (collision.tag == "Platform" || collision.tag == "Wall")
            {
                repel.repel(!player.isRight, isUP, true);
            }
            else if(collision.tag == "Non-moving object")
            {
                BaseObject obj = collision.transform.GetComponent<BaseObject>();

                repel.repel(!player.isRight, isUP, true);
                obj.takeDamage(player.Damage);
            }
            else if (collision.tag != transform.tag)
            {
                BaseObject obj = collision.transform.GetComponent<BaseObject>();
                if (obj == null) obj = collision.transform.GetComponentInParent<BaseObject>();

                Rigidbody2D rb = collision.transform.GetComponentInParent<Rigidbody2D>();

                if (obj.isDead == true) return;

                repel.repel(!player.isRight, isUP, true);
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
                    Repel repl = obj.GetComponent<Repel>();

                    if(repl != null)
                    {
                        if (obj is Vengefly || obj is Gruzzer)
                        {
                            obj.GetComponent<Repel>().repel(player.isRight, isUP);
                            return;
                        }
                        else if (obj as Boss) return;
                        obj.GetComponent<Repel>().repel(player.isRight);
                    }
                } 
            }
        }
    }
}

