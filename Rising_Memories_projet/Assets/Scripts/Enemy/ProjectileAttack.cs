using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAttack : MonoBehaviour
{
    private bool isShoot = false;
    public int speed;
    public int damage;
    
    public GameObject objectToDestroy;
    private GameObject player;


    private Vector3 posiFixed;//fixe la position du joueur
    private Vector3 dir;//vecteur de direction

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
            transform.Translate(dir.normalized * speed * Time.deltaTime);
            if (isShoot == false)
            {
                ShootProjectile();
            }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            PlayerHealth health = collision.GetComponent<PlayerHealth>();
            health.TakeDamage(damage);
            Destroy(objectToDestroy);
        }
        else if (collision.gameObject.layer == 8)
        {
            Destroy(objectToDestroy);
        }
    }

    public void ShootProjectile()
    {
        transform.position = transform.position;
        dir = player.transform.position - transform.position;
        posiFixed = player.transform.position;
        isShoot = true;
    }
}
