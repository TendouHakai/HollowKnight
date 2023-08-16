using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkIsGround : MonoBehaviour
{
    [SerializeField] private Enemy enemy;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision != null && collision.transform.tag == "Platform")
        {
            enemy.isGrounded = true;
            enemy.ani.SetBool("isInAir", false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision != null && collision.transform.tag == "Platform")
        {
            enemy.isGrounded = false;
            enemy.ani.SetBool("isInAir", true);
        }
    }
}
