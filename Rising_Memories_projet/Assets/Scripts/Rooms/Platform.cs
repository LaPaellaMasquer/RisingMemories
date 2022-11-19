using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private void OnCollisionStay2D(Collision2D collision)
    {
        GameObject body = collision.transform.gameObject;
        if(body.name == "Player")
        {
            if (Input.GetAxis("Vertical") < -0.3)
            {
                this.GetComponent<BoxCollider2D>().isTrigger = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject body = collision.transform.gameObject;
        if (body.name == "Player")
        {
            this.GetComponent<BoxCollider2D>().isTrigger = false;
        }
    }
}
