using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : PlayObject
{
    //public Rigidbody2D rb;
    //public Animator ani;
    //public SpriteRenderer render;

    [Header("----------Jump----------")]
    [SerializeField] private float jumpHeight;
    private float jumpForce;
    float timeLand = 0.2f;
    float timeLandStart = 0f;

    [Header("----------Attack----------")]
    [SerializeField] private int combo;
    [SerializeField] public bool atacando;
    [SerializeField] private slashEffect slash;

    [Header("----------Take Damage----------")]
    [SerializeField] private bool isUndying;
    [SerializeField] private float timeUndying;
    private float timeStartUndying;
    [SerializeField] private BoxCollider2D colider; 
    [SerializeField] private LayerMask layerMask; 

    [Header("----------Effect----------")]
    [SerializeField] private GameObject hitEffect;
    [SerializeField] private GameObject deathEffect;
    [SerializeField] private GameObject focusEffect;
    [SerializeField] private GameObject focusEffectGET;
    [SerializeField] private EffectFlasfHit flashEffect;
    private GameObject effect;

    private void Awake()
    {
        jumpForce = Mathf.Sqrt(jumpHeight * (Physics2D.gravity.y * rb.gravityScale) * (-2)) * rb.mass;
        isRight = true;
        combo = 0;

        isUndying = false;
        timeStartUndying = 0f;

        MaxHP = HUDManager.getInstance().health;
        currentHP = MaxHP;
    }

    private void FixedUpdate()
    {
        if(isDead) return;
        if(isMove) 
            Move();
    }

    protected override void Update()
    {
        if (isDead) return;
        ani.SetFloat("speedPlayer", Mathf.Abs(velocity.x*Speed));
        flip();

        // Undying
        if(isUndying)
        {
            if (timeStartUndying > timeUndying)
            {
                colider.enabled = true;

                isUndying = false;
                timeStartUndying = 0f;
                colider.enabled = true;

                flashEffect.endFlash();
            }
            else timeStartUndying += Time.deltaTime;
        }

        // land
        if(State == (int)STATE_PLAYER.Land)
        {
            if (timeLandStart > timeLand)
            {
                setState((int)STATE_PLAYER.EndLand);
                timeLandStart = 0f;
            }
            else timeLandStart += Time.deltaTime;
        }
    }

    public override void setState(int state)
    {
        Vector3 temp = velocity;
        bool isWall = false;
        if (state == this.State) return;
        switch (state)
        {
            case (int)STATE_PLAYER.MoveLeft:
                isWall = Physics2D.BoxCast(colider.bounds.center, colider.bounds.size, 0f, Vector2.left, .1f, layerMask);
                temp.x = isWall == true? 0: -1;
                velocity = temp;
                isRight = false;
                if (isGrounded == true && this.State != (int)STATE_PLAYER.Jump && this.State != (int)STATE_PLAYER.Land)
                    SoundManager.getInstance().PlaySFXPlayerLoop("knight_run");
                else return;
                break;
            case (int)STATE_PLAYER.MoveRight:
                isWall = Physics2D.BoxCast(colider.bounds.center, colider.bounds.size, 0f, Vector2.right, .1f, layerMask);
                temp.x = isWall == true ? 0 : 1;
                velocity = temp;
                isRight = true;

                if (isGrounded == true && this.State != (int)STATE_PLAYER.Jump && this.State != (int)STATE_PLAYER.Land)
                    SoundManager.getInstance().PlaySFXPlayerLoop("knight_run");
                else return;
                break;
            case (int)STATE_PLAYER.IDLE:
                temp.x = 0;
                velocity = temp;
                if (isGrounded == true && this.State != (int)STATE_PLAYER.Jump && this.State != (int)STATE_PLAYER.Land)
                    SoundManager.getInstance().StopSFXPlayer();
                else return;
                break;
            case (int)STATE_PLAYER.Jump:
                if (isGrounded)
                {
                    rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

                    SoundManager.getInstance().PlaySFXPlayer("knight_jump");
                }
                break;
            case (int)STATE_PLAYER.ReleaseJump:
                temp = rb.velocity;
                temp.y = 0;
                rb.velocity = temp;
                break;
            case (int)STATE_PLAYER.Die:
                isMove = false;
                atacando = true;
                ani.Play("player_DEADTH");
                rb.gravityScale = 0f;
                SoundManager.getInstance().PlaySFXPlayer("knight_die");
                break;
            case (int)STATE_PLAYER.Land:
                SoundManager.getInstance().PlaySFXPlayer("knight_land");
                timeLandStart = 0f;
                break;
            case (int)STATE_PLAYER.EndLand:
                break;
            case (int)STATE_PLAYER.Focus:
                isMove = false;
                atacando = true;
                ani.Play("player_FOCUS_START");
                SoundManager.getInstance().PlaySFXPlayer("knight_focus_charging");
                break;
            case (int)STATE_PLAYER.EndFocus:
                isMove= true;
                ani.Play("player_FOCUS_END");
                break;
        }
        base.setState(state);
    }

    // Attack combo function
    public void Combo()
    {
        if (Input.GetKeyDown(KeyCode.S) && atacando)
        {
            //timeAttackStart = 0f;
            atacando = false;
            if (Input.GetKey(KeyCode.UpArrow))
            {
                ani.Play("player_UP_SLASH");
            }
            else ani.SetTrigger("Slash" + combo);

            SoundManager.getInstance().PlaySFXPlayer("knight_attackSword");
        }
    }

    public void FinishAni()
    {
        atacando = true;
        combo = 0;

        ani.Play("player_IDLE");
        setState((int)STATE_PLAYER.IDLE);
    }

    public void startCombo()
    {
        atacando = true;
        if (combo < 1)
        {
            combo++;
        }
    }

    // Attack slash function
    public void Slash()
    {
        slash.gameObject.SetActive(true);
        //slash.setIsRight(isRight);
        slash.setSlashCombo(combo);
    }

    public void upSlash()
    {
        slash.gameObject.SetActive(true);
        slash.upSlash();
    }

    // take damage
    public override void takeDamage(float damage)
    {
        base.takeDamage(damage);

        HUDManager.getInstance().healthDown((int)damage);

        if(isDead == false)
        {
            atacando = true;
            ani.Play("player_TAKE_DAMAGE");

            isUndying = true;
            timeStartUndying = 0f;
            colider.enabled = false;
            flashEffect.startFlash();

            effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(effect, 1f);
        }
        else
        {
            setState((int)STATE_PLAYER.Die);
        }
    }

    // death
    public void startDeathEffect()
    {
        effect =  Instantiate(deathEffect, transform.position, Quaternion.identity);
    }

    public void endDeathEffect()
    {
        Destroy(effect);
        ani.Play("player_DEATH_HEAD_IN_AIR");

        transform.Find("BoxColiderMoving").GetComponent<BoxCollider2D>().size = new Vector2(0.5f, 0.6f);

        rb.gravityScale = 3.5f;
        rb.AddForce(Vector2.up * GameConstant.collissionForceY, ForceMode2D.Impulse);
        rb.AddForce(isRight == true ? Vector2.right * GameConstant.collissionForceX : Vector2.left * GameConstant.collissionForceX, ForceMode2D.Impulse);
    }

    // Focus function
    public void startFocus()
    {
        if (HUDManager.getInstance().isEnoughSoul() && HUDManager.getInstance().isMaxHealth() == false)
        {
            effect = Instantiate(focusEffect, transform.position + new Vector3(0, 0.25f, -0.01f), Quaternion.identity);
            Destroy(effect, 1f);

            HUDManager.getInstance().isGetFocus = true;
            HUDManager.getInstance().downSoul();
            HUDManager.getInstance().healthUp();
        }
        else
        {
            ani.Play("player_FOCUS_END");
        }
    }

    public void getFocus()
    {
        SoundManager.getInstance().PlaySFXPlayer("knight_focus_GET");
        HUDManager.getInstance().isGetFocus = false;
        currentHP += 1;

        effect = Instantiate(focusEffectGET, transform.position + new Vector3(0,0.25f,-0.01f), Quaternion.identity);
        Destroy(effect, 0.25f);
    }

    public void endFocus()
    {
        if (HUDManager.getInstance().isGetFocus == true)
        {
            if (effect != null) Destroy(effect);
            HUDManager.getInstance().isGetFocus = false;
            HUDManager.getInstance().upSoul();
            HUDManager.getInstance().healthDown(1);
        }
    }
} 

public enum STATE_PLAYER
{
    MoveLeft = 1,
    MoveRight = 2,
    IDLE = 3,
    Jump = 4,
    ReleaseJump = 5,
    Die = 6,
    Land = 7,
    EndLand = 8,
    Focus = 9,
    EndFocus = 10,
}
