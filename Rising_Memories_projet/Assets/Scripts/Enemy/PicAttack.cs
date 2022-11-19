using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PicAttack : MonoBehaviour
{
    public int damage;
    private bool isTouch;
    public GameObject objectToDestroy;
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (isTouch == true)
        {
            PlayerHealth health = player.GetComponent<PlayerHealth>();
            health.TakeDamage(damage);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            isTouch = true;
        }
        else
        {
            isTouch = false;
        }
        if (collision.gameObject.layer == 8)
        {
            StartCoroutine(DestroyObject());            
        }
    }

    public IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(1f);
        Destroy(objectToDestroy);
    }
}