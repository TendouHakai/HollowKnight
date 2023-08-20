using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField] int sceneNumber;
    [SerializeField] Vector3 posPlayer;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            SceneManager.LoadScene(sceneNumber);

            collision.transform.position = posPlayer;
        }
    }
}
