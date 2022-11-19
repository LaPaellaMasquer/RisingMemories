using UnityEngine;

public class SpeedBoost : MoveBoost
{
    public override void equipEffect()
    {
        PlayerMovements.instance.speedBonus += 100;
    }
    public override void unEquipEffect()
    {
        PlayerMovements.instance.speedBonus -= 100;
    }
}
