using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum SprayMod
{
    Random,
    Continuous,
}
public class shotGun : Gun
{
    public float spray;
    public float amountOfBullet;
    public SprayMod sprayMod;
    public float bulletSpeedRandomness;

    public shotGun()
    {
        this.type = 3;
    }

    Vector2 CustRotate(float angle)
    {
        return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)); ;
    }

    float getAngle(Vector2 src)
    {
        float res = Mathf.Acos(src.normalized.x);
        
        print(res);
        return res;
    }

    Vector2 rotateVector(Vector2 src, float angle)
    {
        return CustRotate(angle + getAngle(src));
    }
    public override void shot(Vector3 canonPos, Vector2 sp)
    {
        if (reload)
        {
            switch (sprayMod)
            {
                case SprayMod.Continuous:
                    #region Continuous Shot
                    float decal = spray / amountOfBullet;
                    print("Decal : ");
                    print(decal);
                    for (int i = 0; i < amountOfBullet / 2; i++)
                    {
                        Bullet shoot = Instantiate(atks[currentShotIndex], new Vector3(canonPos.x, canonPos.y, 0), Quaternion.identity).GetComponent<Bullet>();
                        Vector2 dir = rotateVector(new Vector2((canonPos.x - sp.x) * (-1f), (canonPos.y - sp.y) * (-1f)), decal * i);
                        shoot.speedX = dir.x;
                        if ((canonPos.y - sp.y) * (-1f) > 0)
                        {
                            shoot.speedY = dir.y;
                        }
                        else
                        {
                            shoot.speedY = -dir.y;
                        }
                        shoot.bulletSpeed += Random.Range(-bulletSpeedRandomness, bulletSpeedRandomness);

                        shoot = Instantiate(atks[currentShotIndex], new Vector3(canonPos.x, canonPos.y, 0), Quaternion.identity).GetComponent<Bullet>();
                        dir = rotateVector(new Vector2((canonPos.x - sp.x) * (-1f), (canonPos.y - sp.y) * (-1f)), -decal * i);
                        shoot.speedX = dir.x;
                        if ((canonPos.y - sp.y) * (-1f) > 0)
                        {
                            shoot.speedY = dir.y;
                        }
                        else
                        {
                            shoot.speedY = -dir.y;
                        }
                        shoot.bulletSpeed += Random.Range(-bulletSpeedRandomness, bulletSpeedRandomness);
                    }
                    #endregion
                    break;
                case SprayMod.Random :
                    for (int i = 0; i < amountOfBullet; i++)
                    {
                        //Bullet shoot = Instantiate(atks[currentShotIndex], new Vector3(playerPos.x, playerPos.y, 0), Quaternion.identity).GetComponent<Bullet>();
                        //Vector2 dir = rotateVector(new Vector2((playerPos.x - sp.x) * (-1f), (playerPos.y - sp.y) * (-1f)), Random.Range(-spray/2,spray/2));
                        
                        Bullet shoot = Instantiate(atks[currentShotIndex], new Vector3(canonPos.x, canonPos.y, 0), Quaternion.identity).GetComponent<Bullet>();
                        Vector2 dir = rotateVector(new Vector2((PlayerAttack.instance.fireDirection.transform.position.x - sp.x) * (-1f), (PlayerAttack.instance.fireDirection.transform.position.y - sp.y) * (-1f)), Random.Range(-spray/2,spray/2));

                        shoot.speedX = dir.x;
                        if ((canonPos.y - sp.y) * (-1f) > 0)
                        {
                            shoot.speedY = dir.y;
                        }
                        else
                        {
                            shoot.speedY = -dir.y;
                        }
                        shoot.bulletSpeed += Random.Range(-bulletSpeedRandomness, bulletSpeedRandomness);
                    }
                    break;
                default:
                    break;
            }
            
            
            reload = false;
            StartCoroutine(Reload());
        }

    }
}
