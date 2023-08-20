using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeoCheckIsGround : isGroundCheck
{
    public override void NotIsInAir()
    {
        if(((Geo)obj).canCollect == false)
            ((Geo)obj).canCollect = true;
        base.NotIsInAir();
    }
}
