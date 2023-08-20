using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private Player player;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (player == null) 
            player = GameObject.FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player.isDead) return;

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
}
