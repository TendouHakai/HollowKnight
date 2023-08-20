using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isGroundCheckPlayer : isGroundCheck
{
    public override void NotIsInAir()
    {
        base.NotIsInAir();
        obj.setState((int)STATE_PLAYER.Land);
    }
}
