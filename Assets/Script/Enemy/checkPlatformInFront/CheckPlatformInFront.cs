using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPlatformInFront : MonoBehaviour
{
    [SerializeField] private Enemy enemy;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision != null && collision.transform.tag == "Platform" && enemy.isDead == false && enemy.isAttack == false)
        {
            enemy.setState(GameConstant.ENEMYGROUND_STATE_NOT_DETECT_GROUND);
        }
    }
}
