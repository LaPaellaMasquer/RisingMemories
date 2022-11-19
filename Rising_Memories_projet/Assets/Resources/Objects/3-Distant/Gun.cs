using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Objet
{
    public GameObject[] atks;
    public float reloadingTime;
    protected bool reload = true;
    protected int currentShotIndex = 0;
    public float gunSize = 1f;
    public Vector2 canonOffset;
    
    public Gun()
    {
        this.type = 3;
    }

    public override void equipEffect()
    {
        PlayerAttack.instance.changeGun(this);
    }
    public override void unEquipEffect()
    {
        PlayerAttack.instance.changeGun(null);
    }

    public virtual void shot(Vector3 playerPos, Vector2 sp)
    {
        if (reload)
        {
            Bullet shoot = Instantiate(atks[currentShotIndex], new Vector3(playerPos.x, playerPos.y, 0), Quaternion.identity).GetComponent<Bullet>();
            currentShotIndex++;
            if (currentShotIndex >= atks.Length)
            {
                currentShotIndex = 0;
            }
            shoot.speedX = (playerPos.x - sp.x) * (-1f);
            shoot.speedY = (playerPos.y - sp.y) * (-1f);
            reload = false;
            StartCoroutine(Reload());
        }
    }

    public IEnumerator Reload()
    {
        yield return new WaitForSeconds(reloadingTime);
        this.reload = true;
    }
}
