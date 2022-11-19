using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : Objet
{
    public GameObject atk1, atk2;
    public Vector2 offset;
    [SerializeField] private AudioClip SoundAttack = null ;

    public override void equipEffect()
    {
        if (this.atk2 == null)
        {
            PlayerAttack.instance.ChangeWeaponSlash(this.atk1);
            
        }
        else
        {
            PlayerAttack.instance.ChangeWeaponSlash(this.atk1, this.atk2);
        }
        PlayerAttack.instance.setOffset(offset);
        PlayerAttack.instance.setSound(SoundAttack);
    }
    public override void unEquipEffect()
    {
        PlayerAttack.instance.getUnarmed();
    }

}
