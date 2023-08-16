using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseObject : MonoBehaviour
{
    public float Damage;
    public float Speed;
    [SerializeField] protected Vector3 velocity;
    [SerializeField] protected Transform Target;
    [SerializeField] protected int State;
    public bool isMove;
    public bool isDead;
    public bool isRight;
    public bool canStun;
    public bool isStun;

    [Header("----------Component----------")]
    [SerializeField] public Rigidbody2D rb;
    [SerializeField] public SpriteRenderer render;
    [SerializeField] public Animator ani;
    public bool isGrounded;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        isMove = true;
        isDead = false;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if(isMove)
            Move();
    }

    public virtual void Move()
    {
        transform.position += Speed * velocity*Time.deltaTime;
    }

    public virtual void setTarget(Transform target)
    {
        this.Target = target;
    }

    public virtual void setState(int state)
    {
        this.State = state;
    }

    public virtual void takeDamage(float damage)
    {
        
    }

    public virtual void Dead()
    { 

    }

    public virtual void flip()
    {
        if (isRight)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}