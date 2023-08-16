using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeoCheckIsGround : isGroundCheckPlayer
{
    public override void NotIsInAir()
    {
        if(((Geo)player).canCollect == false)
            ((Geo)player).canCollect = true;
        base.NotIsInAir();
    }
}
