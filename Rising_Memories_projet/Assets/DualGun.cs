using System.Collections;
using UnityEngine;

public class DualGun : Gun
{
    public float delayBetweenShoot;
    public float decal = .1f;
    public override void shot(Vector3 playerPos, Vector2 sp)
    {
        base.shot(playerPos, sp);
        StartCoroutine(ShotSecondary(playerPos, sp));
    }

    public IEnumerator ShotSecondary(Vector3 playerPos, Vector2 sp)
    {
        yield return new WaitForSeconds(delayBetweenShoot);
        Bullet shoot = Instantiate(atks[currentShotIndex], new Vector3(playerPos.x, playerPos.y, 0), Quaternion.identity).GetComponent<Bullet>();
        shoot.speedX = (playerPos.x - sp.x) * (-1f);
        shoot.speedY = (playerPos.y - sp.y) * (-1f);
    }
}
