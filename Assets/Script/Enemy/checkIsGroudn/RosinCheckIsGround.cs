using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RosinCheckIsGround : isGroundCheckPlayer
{
    public override void NotIsInAir()
    {
        player.Dead();
    }

    public override void IsInAir()
    {
        
    }
}
