using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public float maxSpeedY;

    [Header("----------Attack----------")]
    [SerializeField] private int combo;
    [SerializeField] public bool atacando;
    [SerializeField] private slashEffect slash;

    [Header("----------Take Damage----------")]
    [SerializeField] public bool isUndying;
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
        DontDestroyOnLoad(this.gameObject);

        jumpForce = Mathf.Sqrt(jumpHeight * (Physics2D.gravity.y * rb.gravityScale) * (-2)) * rb.mass;
        isRight = true;
        combo = 0;

        isUndying = false;
        timeStartUndying = 0f;
    }

    protected override void Start()
    {
        base.Start();
        PLayerData data = SaveLoadSystem.LoadPlayerData();
        if (data != null)
        {
            transform.position = new Vector3(data.positionBench[0], data.positionBench[1], -0.01f);
            setState((int)STATE_PLAYER.Sit);
        }
    }

    // load data

    private void FixedUpdate()
    {
        if(isDead) return;
        if (Mathf.Abs(rb.velocity.y) > maxSpeedY && rb.velocity.y < 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, -maxSpeedY);
        }
        
        if (isMove) 
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
        if (state == this.State && state != (int)STATE_PLAYER.Jump) return;
        switch (state)
        {
            case (int)STATE_PLAYER.MoveLeft:
                isWall = Physics2D.BoxCast(colider.bounds.center, colider.bounds.size - new Vector3(0.1f,0f, 0f), 0f, Vector2.left, .3f, layerMask);
                temp.x = isWall == true? 0: -1;
                velocity = temp;
                isRight = false;
                if (isGrounded == true && this.State != (int)STATE_PLAYER.Jump && this.State != (int)STATE_PLAYER.Land)
                    SoundManager.getInstance().PlaySFXPlayerLoop("knight_run");
                else return;
                break;
            case (int)STATE_PLAYER.MoveRight:
                isWall = Physics2D.BoxCast(colider.bounds.center, colider.bounds.size - new Vector3(0.1f, 0f, 0f), 0f, Vector2.right, .3f, layerMask);
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
                endCombo();
                ani.Play("player_DEADTH");
                rb.gravityScale = 0f;
                SaveLoadSystem.SaveHollowShadeData(transform.position, SceneManager.GetActiveScene().buildIndex, HUDManager.getInstance());
                SaveLoadSystem.saveAllData();
                HUDManager.getInstance().downSoulToZero();
                HUDManager.getInstance().downCointToZero();
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
                endCombo();
                ani.Play("player_FOCUS_START");
                SoundManager.getInstance().PlaySFXPlayer("knight_focus_charging");
                break;
            case (int)STATE_PLAYER.EndFocus:
                isMove= true;
                ani.Play("player_FOCUS_END");
                break;
            case (int)STATE_PLAYER.Sit:
                currentHP = MaxHP;
                HUDManager.getInstance().healthUpFull();
                isMove = false;
                endCombo();
                ani.Play("player_SIT");
                PlayerControl.getInstance().isSitting = true;
                effect = Instantiate(focusEffectGET, transform.position + new Vector3(0, 0.25f, -0.01f), Quaternion.identity);
                Destroy(effect, 0.4f);
                break;
            case (int)STATE_PLAYER.Wake: 
                ani.Play("player_WAKE");
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
        endCombo();

        ani.Play("player_IDLE");
        setState((int)STATE_PLAYER.IDLE);
    }

    public void endCombo()
    {
        atacando = true;
        combo = 0;
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
            endCombo();
            ani.Play("player_TAKE_DAMAGE");

            isUndying = true;
            timeStartUndying = 0f;
            colider.enabled = false;
            flashEffect.startFlash();

            effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            SoundManager.getInstance().PlaySFXPlayer("knight_takeDamage");
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

        PLayerData data = SaveLoadSystem.LoadPlayerData();
        if (data != null)
        {
            SceneLoader.getInstance().loadScene(data.sceneNumber, new Vector3(data.positionBench[0], data.positionBench[1], -0.01f));
        }
        else SceneLoader.getInstance().loadSceneCurrent();
    }

    // Revival
    public void Revial()
    {
        transform.Find("BoxColiderMoving").GetComponent<BoxCollider2D>().size = new Vector2(0.5f, 1f);
        isDead = false;

        PLayerData data = SaveLoadSystem.LoadPlayerData();
        if (data != null)
            setState((int)STATE_PLAYER.Sit);
        else
        {
            isMove = true;
            currentHP = MaxHP;
            HUDManager.getInstance().healthUpFull();
            ani.Play("player_IDLE");
        }
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

    // Sitting and save data
    public void endSitting()
    {
        isMove = true;
        PlayerControl.getInstance().isSitting = false;
    }

    // subcriber
    public override void update(int state)
    {
        if(state == (int)Game_State.BacktoMenu)
        {
            Debug.Log("Destroy player");
            Destroy(this.gameObject);
        }
        else base.update(state);
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
    Sit = 11,
    Wake = 12,
}
