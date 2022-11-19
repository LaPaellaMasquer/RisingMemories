using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerMovements.instance.AddForce(new Force(new Vector2(0, 1)));
            Destroy(gameObject);
        }
    }
}
