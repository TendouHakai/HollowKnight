using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElderBug : BaseObject
{
    public override void setState(int state)
    {
        switch (state)
        {
            case (int)STATE_ELDERBUG.Talk:
                ani.SetTrigger("Talk");
                break;
            case (int)STATE_ELDERBUG.EndTalk:
                ani.SetTrigger("EndTalk");
                break;
        }
        base.setState(state);
    }

    protected override void Update()
    {
        if(Target != null)
        {
            float x = Target.position.x - transform.position.x;
            if(x < 0)
            {
                ani.SetBool("IsRight", false);
            }
            else
            {
                ani.SetBool("IsRight", true);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && Target == null)
        {
            Target = collision.transform;
        }
    }
}

public enum STATE_ELDERBUG
{
    IDLE=1,
    Turn=2,
    Talk=GameConstant.NPC_STATE_TALK,
    EndTalk= GameConstant.NPC_STATE_END_TALK,
}
