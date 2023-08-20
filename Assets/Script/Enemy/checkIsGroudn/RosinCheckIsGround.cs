using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RosinCheckIsGround : isGroundCheck
{
    public override void NotIsInAir()
    {
        obj.Dead();
    }

    public override void IsInAir()
    {
        
    }
}
