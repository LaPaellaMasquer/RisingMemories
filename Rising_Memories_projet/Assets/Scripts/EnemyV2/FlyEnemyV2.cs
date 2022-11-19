using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEnemyV2 : EnemyScriptMoveBase
{
    [SerializeField]
    private float Radius;
    private float Dis = 0.3f;
    private bool isMove = true;
    private bool isAttack;

    private float speed = 2f;

    private const string FILE_PATH = "Enemy/";


    public void Awake()
    {
        this.setCenterCircle(transform);
        this.setRadiusCircle(Radius);
        setPlayer();
    }

    private void Update()
    {
        if (isPlayerEnter == true && Vector3.Distance(transform.position, player.transform.position) > Dis)
        {
            if (isMove == true && !Physics2D.Linecast(transform.position, player.transform.position, layerGround))
            {
                Vector3 dir = player.transform.position - transform.position;
                transform.Translate(dir.normalized * speed * Time.deltaTime);
                isAttack = true;
            }
            else if (isAttack == true && !Physics2D.Linecast(transform.position, player.transform.position, layerGround))
            {

                GameObject nodePrefab;
                nodePrefab = Resources.Load(FILE_PATH + "Projectile") as GameObject;
                nodePrefab = GameObject.Instantiate(nodePrefab, transform.position, Quaternion.identity);
            }
            StartCoroutine(delay());
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            Collider2D enemyCollider = GetComponent<Collider2D>();
            enemyCollider.isTrigger = true;
            StartCoroutine(delayTrigger());
        }  
    }

    public IEnumerator delayTrigger()
    {
        yield return new WaitForSeconds(1f);
        Collider2D enemyCollider = GetComponent<Collider2D>();
        enemyCollider.isTrigger = false;
    }

    public IEnumerator delay()
    {
        if (isMove == true)
        {
            yield return new WaitForSeconds(1f);
            isMove = false;
        }
        else
        {
            isAttack = false;
            yield return new WaitForSeconds(1f);
            isMove = true;
        }
    }
}
