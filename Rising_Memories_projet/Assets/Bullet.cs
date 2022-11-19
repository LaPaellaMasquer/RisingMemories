using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool isFriendly = true;
    public float bulletSpeed = 40;
    public int bulletDamage;
    public float speedX;
    public float speedY;
    public GameObject blowParticle;


    private void Update()
    {
        Vector3 dir = new Vector2(speedX, speedY);
        transform.Translate(dir.normalized * bulletSpeed * Time.deltaTime, Space.World);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyStat aux = collision.GetComponent<EnemyStat>();
            aux.takeDamage(bulletDamage);
            aux.TargetedDamage(this.transform);
            Debug.Log("Enemy hit");
        }
        if (collision.CompareTag("Player"))
        {
            return;
        }
        StartCoroutine(Blow());
    }

    public virtual IEnumerator Blow()
    {
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        speedX = 0;
        speedY = 0;
        this.GetComponent<SpriteRenderer>().enabled = false;
        blowParticle.SetActive(true);
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}