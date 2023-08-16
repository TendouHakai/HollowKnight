using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConstant : MonoBehaviour
{
    //#region Player state
    //public const int MOVING_LEFT_STATE = 0;
    //public const int MOVING_RIGHT_STATE = 1;
    //public const int IDLE_STATE = 3;
    //public const int JUMP_STATE = 4;
    //public const int RELEASE_JUMP_STATE = 5;
    //#endregion

    #region ENEMY GROUND STATE
    public const int ENEMYGROUND_STATE_DETECT_WALL = 10;
    public const int ENEMYGROUND_STATE_NOT_DETECT_GROUND = 11;
    public const int ENEMYGROUND_STATE_DETECT_PLAYER = 12;
    public const int ENEMYGROUND_STATE_NOT_DETECT_PLAYER = 13;

    public const int ENEMY_STATE_DEAD = 14;
    #endregion

    #region ENEMY FLY TURN STATE
    public const int ENEMYFLY_STATE_TURNX = 20;
    public const int ENEMYFLY_STATE_TURNY = 21;
    #endregion

    #region take damage
    public const float collissionForceX = 2.5f;
    public const float collissionForceY = 7f;

    public const float collisionForceSlash = 8.0f;
    #endregion


    //#region Husk Bully
    //public const float speedBuskBully_WALK = 1.5f;
    //public const float speedBuskBully_RUN = 2.7f;

    //public const int HUSKBULLY_STATE_IDLE = 0;
    //public const int HUSKBULLY_STATE_WALK = 1;
    //public const int HUSKBULLY_STATE_ATTACK = ENEMYGROUND_STATE_DETECT_PLAYER;
    //public const int HUSKBULLY_STATE_STOP_ATTACK = ENEMYGROUND_STATE_NOT_DETECT_PLAYER;
    //public const int HUSKBULLY_STATE_TURN = ENEMYGROUND_STATE_DETECT_WALL;
    //#endregion

    //#region Leaping Husk
    //public const int LEAPINGHUSK_STATE_IDLE = 0;
    //public const int LEAPINGHUSK_STATE_WALK = 1;
    //public const int LEAPINGHUSK_STATE_TURN = ENEMYGROUND_STATE_DETECT_WALL;
    //public const int LEAPINGHUSK_STATE_START_ATTACK = ENEMYGROUND_STATE_DETECT_PLAYER;
    //public const int LEAPINGHUSK_STATE_END_ATTACK = ENEMYGROUND_STATE_NOT_DETECT_PLAYER;
    //#endregion

    //#region TikTik
    //public const int TIKTIK_STATE_TURN = ENEMYGROUND_STATE_DETECT_WALL;
    //public const int TIKTIK_STATE_TURN2 = ENEMYGROUND_STATE_NOT_DETECT_GROUND;
    //#endregion

    //#region Vengefly
    //public const int VENGEFLY_STATE_TURNX = ENEMYGROUND_STATE_DETECT_WALL;
    //public const int VENGEFLY_STATE_TURNY = 0;
    //public const int VENGEFLY_STATE_START_ATTACK = 1;
    //#endregion
}
