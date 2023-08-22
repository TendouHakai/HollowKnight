using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsedaHouse : MonoBehaviour
{
    [SerializeField] protected GameObject talkUI;
    protected int index = 0;
    protected bool PlayerIsInRange = false;

    [Header("------------SCENE INFO--------------")]
    [SerializeField] int sceneNumber;
    [SerializeField] Vector3 posPlayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            SceneLoader.getInstance().loadScene(sceneNumber, posPlayer);
        }
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerIsInRange = true;
            talkUI.SetActive(true);
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
