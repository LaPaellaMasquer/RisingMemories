using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJump : JumpBoost
{
    public override void equipEffect()
    {
        PlayerMovements.instance.maxJumpAmount = 2;
    }
    public override void unEquipEffect()
    {
        PlayerMovements.instance.maxJumpAmount = 1;
    }
}
