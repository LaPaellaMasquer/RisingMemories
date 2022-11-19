using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealSword : Weapons
{
    public override void onUse()
    {
        PlayerHealth.instance.HealPlayer(10);
    }
}
