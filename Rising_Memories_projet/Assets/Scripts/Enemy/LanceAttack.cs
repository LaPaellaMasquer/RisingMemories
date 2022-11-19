using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanceAttack : MonoBehaviour
{
    public bool isClone = false;
    private bool isShoot = false;
    public int speed;
    public int damage;

    public GameObject objectToDestroy;
    private GameObject player;
    public Rigidbody2D rb;

    private Vector3 posiFixed;//fixe la position du joueur
    private Vector3 dir;//vecteur de direction


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (isClone == true)
        {
            transform.Translate(dir.normalized * speed * Time.deltaTime);
            if (isShoot == false)
            {
                ShootProjectile();
            }
        }
    }



    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player") && isClone == true)
        {
            PlayerHealth health = collision.GetComponent<PlayerHealth>();
            health.TakeDamage(damage);
            Destroy(objectToDestroy);
        }
        else if (collision.gameObject.layer == 8 && isClone == true)
        {
            Destroy(objectToDestroy);
        }
    }

    public void ShootProjectile()
    {
        dir = player.transform.position - transform.position;
        if (dir.x < 0)
        {
            SpriteRenderer sprite = GetComponent<SpriteRenderer>();
            sprite.flipX = true;
        }
        rb.AddForce(new Vector2(dir.x * 10, 50f));
        isShoot = true;
    }
}
