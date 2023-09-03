using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TownBench : MonoBehaviour
{
    [SerializeField] protected GameObject talkUI;
    protected bool PlayerIsInRange = false;
    Player player = null;
    // Start is called before the first frame update
    void Start()
    {
        talkUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerIsInRange)
        {
            if(PlayerControl.getInstance().isSitting)
            {
                if (Input.anyKeyDown)
                {
                    player.setState((int)STATE_PLAYER.Wake);
                    talkUI.SetActive(true);
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    SoundManager.getInstance().PlaySFXPlayer("Bench_rest");
                    Vector3 temp = transform.position;
                    temp.y = player.transform.position.y;
   
                    // save data
                    SaveLoadSystem.SavePlayerData(temp, SceneManager.GetActiveScene().buildIndex);
                    SaveLoadSystem.saveAllData();

                    player.setState((int)STATE_PLAYER.Sit);
                    talkUI.SetActive(false);
                }
            }
        }
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerIsInRange = true;

            if(player == null)
            {
                player = collision.GetComponent<Player>();
            }

            if(PlayerControl.getInstance().isSitting)
            {
                talkUI.SetActive(false);
            }
            else talkUI.SetActive(true);
        }
    }

    protected void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerIsInRange = false;
            talkUI.SetActive(false);
        }
    }
}
