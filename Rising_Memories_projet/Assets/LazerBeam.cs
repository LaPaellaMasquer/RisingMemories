using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerBeam : Gun
{
    public Transform firePoint;
    public LineRenderer line;
    public Camera cam;
    public bool isAlreadyShooting;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            Vector2 mousePos = (Vector2)cam.ScreenToWorldPoint(Input.mousePosition);

            line.SetPosition(0, PlayerAttack.instance.canon.transform.position);
            Vector2 dir = mousePos - (Vector2)PlayerAttack.instance.canon.transform.position;
            RaycastHit2D hit = Physics2D.Raycast((Vector2)PlayerAttack.instance.canon.transform.position, dir.normalized);
            line.SetPosition(1, hit.point);

            if (!isAlreadyShooting)
            {
                isAlreadyShooting = true;
                StartCoroutine(applyDmg(dir, hit));
            }
        }

        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            line.enabled = false;
        }
    }

    public IEnumerator applyDmg(Vector2 dir, RaycastHit2D hit)
    {
        yield return new WaitForSeconds(.1f);
        RaycastHit2D[] hits;
        hits = Physics2D.RaycastAll(firePoint.position, dir.normalized, (hit.point - (Vector2)firePoint.position).magnitude);
        foreach (RaycastHit2D target in hits)
        {
            if (target.transform.CompareTag("Enemy"))
            {
                target.transform.GetComponent<EnemyStat>().takeDamage(10);
            }
        }
        isAlreadyShooting = false;
    }

    public override void shot(Vector3 playerPos, Vector2 sp)
    {
        line.enabled = true;
    }

}
