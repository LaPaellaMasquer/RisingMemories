using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerDamageBox : MonoBehaviour
{
    [SerializeField]
    int damage;

    public bool needDelete = false;
    public int getDamage()
    {
        return damage;
    }

    public float time;

    private void Start()
    {
        StartCoroutine(AutoClear());
    }

    public IEnumerator AutoClear()
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyStat aux = collision.GetComponent<EnemyStat>();
            aux.takeDamage(damage);
            aux.TargetedDamage(this.transform);
            Debug.Log("Enemy hit");
        }
    }

}
