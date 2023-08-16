using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OncollisionWall : MonoBehaviour
{
    [SerializeField] Enemy enemy;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.transform.tag == "Platform" || collision.transform.tag == "Wall") && enemy.isDead == false)
        {
            Vector2 direction = collision.GetContact(0).normal;
            if (Mathf.RoundToInt(direction.x) != 0)
            {
                enemy.setState(GameConstant.ENEMYFLY_STATE_TURNX);
            }
            else    
            {
                enemy.setState(GameConstant.ENEMYFLY_STATE_TURNY);
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if ((collision.transform.tag == "Platform" || collision.transform.tag == "Wall") && enemy.isDead == false)
        {
            Vector2 direction = collision.GetContact(0).normal;
            if (Mathf.RoundToInt(direction.x) != 0)
            {
                enemy.setState(GameConstant.ENEMYFLY_STATE_TURNX);
            }
            else 
            {
                enemy.setState(GameConstant.ENEMYFLY_STATE_TURNY);
            }
        }
    }
}
