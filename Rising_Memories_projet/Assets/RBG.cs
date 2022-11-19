using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RBG : Gun
{
    public GameObject ammo;
    public RBG()
    {
        this.reloadingTime = 1f;
    }
    public override void shot(Vector3 playerPos, Vector2 sp)
    {
        RfgAmmo shoot = Instantiate(ammo, new Vector3(playerPos.x, playerPos.y, 0), Quaternion.identity).GetComponent<RfgAmmo>();
        Vector2 dir = new Vector2((playerPos.x - sp.x) * (-1f), (playerPos.y - sp.y) * (-1f)).normalized;
        shoot.speedX = dir.x;
        shoot.speedY = dir.y;
        reload = false;
        StartCoroutine(Reload());
    }
}
