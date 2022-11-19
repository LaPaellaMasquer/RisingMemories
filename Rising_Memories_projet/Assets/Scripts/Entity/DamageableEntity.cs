using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableEntity : MonoBehaviour
{
    [SerializeField]
    int maxHealth;
    [SerializeField]
    int health;
    [SerializeField]
    HealthBarMobs healthBar;

    void Start()
    {
        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetHealth(health);
    }
    public void TakeDamage(int damage)
    {
            health -= damage;
            healthBar.SetHealth(health);
            
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerDamage"))
        {
            PlayerDamageBox hit = collision.GetComponent<PlayerDamageBox>();
            if(hit == null)
            {
                Debug.LogError("A item marked as PlayerDamage have no PlayerDamageBox");
            }
            else
            {
                TakeDamage(hit.getDamage());
                TargetedDamage(collision.transform);
            }
        }
    }



    public Transform center;

    public GameObject blood;

    private float rotation;

    public void TargetedDamage(Transform dmgSource)
    {
        GameObject bloodAnim = Instantiate(blood);
        if (dmgSource.position.y < center.position.y)
        {
            if (dmgSource.position.x > center.position.x)
            {
                rotation = (float)Math.Atan((center.position.y - dmgSource.position.y) / (dmgSource.position.x - center.position.x)) * 57.2958f;
            }
            else
            {
                rotation = -180f + (float)Math.Atan((center.position.y - dmgSource.position.y) / (center.position.x - dmgSource.position.x)) * -57.2958f;
            }
        }
        else
        {
            if (dmgSource.position.x > center.position.x)
            {
                rotation = (float)Math.Atan((dmgSource.position.y - center.position.y) / (dmgSource.position.x - center.position.x)) * -57.2958f;
            }
            else
            {
                rotation = (float)Math.Atan((dmgSource.position.y - center.position.y) / (center.position.x - dmgSource.position.x)) * 57.2958f + 180f;
            }
        }
        bloodAnim.GetComponent<Transform>().Rotate(0f, rotation, 0f);
        bloodAnim.GetComponent<Transform>().position = center.position;
        bloodAnim.GetComponent<ParticleSystem>().Play();
    }


}
