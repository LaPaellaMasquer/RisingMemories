using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    public bool ismoving;

    private Vector3 finish;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject body = collision.gameObject;
        body.GetComponent<PlayerHealth>().TakeDamage(10000);
    }


    public void setMovement(Vector3 finish)
    {
        this.finish = finish;
    }

    // Update is called once per frame
    void Update()
    {
        if (ismoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, finish, Time.deltaTime * 1.5f);
        }
    }
}
