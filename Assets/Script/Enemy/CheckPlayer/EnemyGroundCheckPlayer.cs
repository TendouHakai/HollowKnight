using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyGroundCheckPlayer : MonoBehaviour
{
    [SerializeField] Enemy enemy;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision != null && collision.tag == "Player")
        {
            if(enemy.isAttack == false  && enemy.isDead == false)
            {
                enemy.setTarget(collision.gameObject.transform);
                enemy.setState(GameConstant.ENEMYGROUND_STATE_DETECT_PLAYER);
            }  
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision != null && collision.tag == "Player" && enemy.isDead == false)
        {
            if (enemy.isAttack == false)
            {
                enemy.setState(GameConstant.ENEMYGROUND_STATE_NOT_DETECT_PLAYER);
            }
        }
    }
}
