using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : MonoBehaviour
{

    private const string FILE_PATH = "Objects/OverAll/CoinExplosion/";
    public bool isAlreadyTargeted;

    [SerializeField]
    private float MaxHealth;
    [SerializeField]
    private int damage;
    private float CurHealth;
    private float AttackDamage;
    Transform center;
    public bool isAlive { get { return CurHealth > 0f; } }
    private bool isTouch;
    private HealthBarEnemy healthbar;
    private Transform objectHealthBar;

    private GameObject player;
    [SerializeField]
    private int maxCoin;

    private Animator anim;

    void Start()
    {
        center = this.transform;
        player = GameObject.FindGameObjectWithTag("Player");
        CurHealth = MaxHealth;
        objectHealthBar = this.transform.Find("HealthBar");
        if (objectHealthBar != null)
        {
            healthbar = objectHealthBar.GetComponent<HealthBarEnemy>();
        }
        anim = gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            takeDamage(100);
        }
        if (isTouch == true)
        {
            isTouch = false; 
            PlayerHealth health = player.GetComponent<PlayerHealth>();
            health.TakeDamage(damage);
        }
    }

    public void takeDamage(float damage)
    {
        CurHealth = CurHealth - damage;
        if (isAlive)
        {
            if (objectHealthBar != null)
            {
                healthbar.SetHealth(CurHealth, MaxHealth);
            }      
        }
        else
        {
            if (anim == null)
            {
                Destroy(gameObject);
            }
            else
            {
                StartCoroutine(death());
            }
        }
    }

    public IEnumerator death()
    {
        anim.SetBool("alive", true);
        yield return new WaitForSeconds(0.50f);
        GameObject nodePrefab;
        nodePrefab = Resources.Load(FILE_PATH + "CoinExplosion") as GameObject;
        nodePrefab = GameObject.Instantiate(nodePrefab, transform.position, Quaternion.identity);
        nodePrefab.GetComponent<CoinExplosion>().amount = UnityEngine.Random.Range(0, maxCoin);   
        Destroy(gameObject);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            isTouch = true;
        }
    }

    public float getCurLife()
    {
        return CurHealth;
    }

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
