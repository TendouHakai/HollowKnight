using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour, Subcriber
{
    private static PlayerControl instance;

    public static PlayerControl getInstance()
    {
        if (instance == null)
        {
            instance = GameObject.FindObjectOfType<PlayerControl>();
        }
        return instance;
    }

    private void Awake()
    {
        instance = this;

        DontDestroyOnLoad(gameObject);
    }

    [SerializeField] private Player player;
    public bool isInteract = false;
    public bool isSitting = false;

    // Start is called before the first frame update
    private void Start()
    {
        GameStateManager.getInstance().publisherGameState.subcribe(this);
        if (player == null)
        {
            player = GameObject.FindObjectOfType<Player>();
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if(player.isDead || isInteract || isSitting) return;

        player.ani.SetBool("IsLookUp", false);
        player.ani.SetBool("IsLookDown", false);

        if (Input.GetKeyDown(KeyCode.A))
        {
            player.setState((int)STATE_PLAYER.Focus);
        }
        
        if(Input.GetKeyUp(KeyCode.A))
        {
            player.setState((int)(STATE_PLAYER.EndFocus ));
        }

        if (HUDManager.getInstance().isGetFocus == true) return;

        if (Input.GetKey(KeyCode.RightArrow))
        {
            player.setState((int)STATE_PLAYER.MoveRight);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            player.setState((int)STATE_PLAYER.MoveLeft);
        }
        else
        {
            player.setState((int)STATE_PLAYER.IDLE);
            if(Input.GetKey(KeyCode.UpArrow) && player.atacando == true)
            {
                player.ani.SetBool("IsLookUp", true);
            }
            else if(Input.GetKey(KeyCode.DownArrow) && player.atacando == true)
            {
                player.ani.SetBool("IsLookDown", true);
            }  
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            player.setState((int)STATE_PLAYER.Jump);
        } 

        if (player.rb.velocity.y > 0)
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                player.setState((int)STATE_PLAYER.ReleaseJump);
            }
        }

        // attack combo
        player.Combo();
    }

    // subcribe
    public void update(int state)
    {
        if(state == (int)Game_State.BacktoMenu)
        {
            Debug.Log("Destroy playerControl");
            Destroy(this.gameObject);
        }
        else if(state == (int)Game_State.Pause)
        {
            SoundManager.getInstance().StopSFXPlayer();
            enabled = false;
        }
        else
        {
            enabled = true;
        }
    }
}
