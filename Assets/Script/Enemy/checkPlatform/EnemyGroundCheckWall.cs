using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroundCheckWall : MonoBehaviour
{
    [SerializeField] Enemy enemyground;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision != null && (collision.tag == "Wall" || collision.tag == "Platform" || collision.tag == "Non-moving object"))
        {
            if (enemyground.isAttack == false && enemyground.isTurn == false && enemyground.isDead == false)
            {
                enemyground.setState(GameConstant.ENEMYGROUND_STATE_DETECT_WALL);
            }
        }
    }
}
