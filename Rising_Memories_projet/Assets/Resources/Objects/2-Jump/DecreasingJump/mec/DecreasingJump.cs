using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecreasingJump : JumpBoost
{
    private float baseJumpForce;

    public override void equipEffect()
    {
        PlayerMovements.instance.maxJumpAmount += 5;
        baseJumpForce = PlayerMovements.instance.jumpForce;
    }
    public override void unEquipEffect()
    {
        PlayerMovements.instance.jumpForce = baseJumpForce;
        PlayerMovements.instance.maxJumpAmount -= 5;
    }
    public override void onJump()
    {
        PlayerMovements.instance.jumpForce -= 50f;
    }

    public override void onLanding()
    {
        PlayerMovements.instance.jumpForce = baseJumpForce;
    }
}
