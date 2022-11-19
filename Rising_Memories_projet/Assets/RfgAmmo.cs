using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class RfgAmmo : MonoBehaviour
{
    CrakingLine[] lines;
    public float range = 10;
    public GameObject lightningPrefab;
    public GameObject blowParticle;
    public GameObject constantParticleEffect;
    public Light2D _light;

    public float bulletSpeed = 40;
    public float speedX;
    public float speedY;

    private void Start()
    {
        StartCoroutine(ScanForTarget());
    }

    public IEnumerator Blow()
    {
        StartCoroutine(FadeLight());

        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(this.transform.position, range);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy"))
            {
                hitCollider.GetComponent<EnemyStat>().takeDamage(50);
            }
        }

        this.GetComponent<SpriteRenderer>().enabled = false;
        this.blowParticle.SetActive(true);
        this.constantParticleEffect.GetComponent<ParticleSystem>().emissionRate = 0;
        this.speedX = 0;
        this.speedY = 0;
        yield return new WaitForSeconds(4);
        Destroy(gameObject);
    }

    public IEnumerator FadeLight()
    {
        this._light.intensity -= .7f;
        this.transform.localScale /= 2;
        yield return new WaitForSeconds(.1f);
        StartCoroutine(FadeLight());

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground") || collision.CompareTag("Enemy"))
        {
            this.GetComponent<CircleCollider2D>().enabled = false;
            StartCoroutine(Blow());
        }
    }

    private void Update()
    {
        Vector3 dir = new Vector2(speedX, speedY);
        transform.Translate(dir.normalized * bulletSpeed * Time.deltaTime, Space.World);
    }

    public IEnumerator ScanForTarget()
    {
        yield return new WaitForSeconds(.5f);

        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(this.transform.position, range);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy"))
            {
                EnemyStat ent = hitCollider.GetComponent<EnemyStat>();
                if (!ent.isAlreadyTargeted)
                {
                    CrakingLine beam = Instantiate(lightningPrefab).GetComponent<CrakingLine>();
                    beam.src = this.gameObject;
                    beam.dst = hitCollider.gameObject;
                    beam._dstStat = hitCollider.GetComponent<EnemyStat>();
                    beam.transform.SetParent(this.transform);
                    beam.transform.localPosition = new Vector3(0, 0, 0);
                    ent.isAlreadyTargeted = true;
                }
            }
        }
        StartCoroutine(ScanForTarget());
    }
}
