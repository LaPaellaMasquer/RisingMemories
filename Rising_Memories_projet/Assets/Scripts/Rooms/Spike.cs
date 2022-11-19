using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    private void OnCollisionStay2D(Collision2D collision)
    {
        GameObject body = collision.transform.gameObject;
        if(body.name == "Player")
        {
            body.GetComponent<PlayerHealth>().TakeDamage(10); ;
        }
        if (body.tag == "Enemy")
        {
            body.GetComponent<EnemyStat>().takeDamage(10);
        }
    }
}
