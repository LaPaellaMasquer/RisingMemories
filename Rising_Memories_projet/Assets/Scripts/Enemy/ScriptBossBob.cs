using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptBossBob : MonoBehaviour
{
    [SerializeField]
    private GameObject pointDis;

    private const string FILE_PATH = "Enemy/";
    private GameObject player;
    private bool isAttack;
    private bool isProtected=true;
    private bool isFullAttack = false;
    private EnemyStat health;

    private bool isGrounded;
    private int count=0;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        health = gameObject.GetComponent<EnemyStat>();
        StartCoroutine(delayStart());
    }

    // Update is called once per frame
    void Update()
    {
        if (isAttack == true && isFullAttack==false)
        {
            GameObject picPrefab;
            picPrefab = Resources.Load(FILE_PATH + "PicPrefab") as GameObject;
            picPrefab = GameObject.Instantiate(picPrefab, new Vector3(player.transform.position.x, 6f, player.transform.position.z), picPrefab.transform.rotation);
            StartCoroutine(delay());
        }
        else if(health.getCurLife()<300f && isProtected==true)
        {
            for (int i = 0; i < 2; i++)
            {
                GameObject littleBobPrefab;
                littleBobPrefab = Resources.Load(FILE_PATH + "EnemyPetitBob") as GameObject;
                littleBobPrefab = GameObject.Instantiate(littleBobPrefab, transform.position, Quaternion.identity);
            }
            StartCoroutine(delayLittleBob());
        }else if(health.getCurLife() < 500f)
        {   
            if (isGrounded == true && count <= 3)
            {
                isFullAttack = true;
                isGrounded = false;
                Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
                rb.AddForce(new Vector2(0f, 150000f));
                if (count == 3)
                {
                    StartCoroutine(fullAttackPic());
                }
                count++;
            }
        }       
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            isGrounded = true;
        }
    }

        public IEnumerator delayLittleBob()
    {
        yield return new WaitForSeconds(0.5f);
        isProtected = false;
    }

    public IEnumerator fullAttackPic()
    {
        float dis = transform.position.x - pointDis.transform.position.x;
        float oneDis = dis / 12;
        float currentPosForLeft = transform.position.x;
        float currentPosForRight = transform.position.x;
        for (int i = 0; i < 12; i++)
        {
            yield return new WaitForSeconds(0.5f);
            GameObject picPrefab;
            picPrefab = Resources.Load(FILE_PATH + "PicPrefab") as GameObject;
            picPrefab = GameObject.Instantiate(picPrefab, new Vector3((currentPosForRight - oneDis + 1), 6f, player.transform.position.z), picPrefab.transform.rotation);
            picPrefab = GameObject.Instantiate(picPrefab, new Vector3((currentPosForLeft + oneDis - 1), 6f, player.transform.position.z), picPrefab.transform.rotation);
            currentPosForRight = currentPosForRight - oneDis;
            currentPosForLeft = currentPosForLeft + oneDis;
        }
        yield return new WaitForSeconds(1f);
        isFullAttack = false;
    }

    public IEnumerator delay()
    {
        if (isAttack == true)
        {
            isAttack = false;
            yield return new WaitForSeconds(1f);
            StartCoroutine(delay());
        }
        else
        {
            yield return new WaitForSeconds(2f);
            isAttack = true;
        }
    }

    public IEnumerator delayStart()
    {
        yield return new WaitForSeconds(3f);
        isAttack = true;
    }
}
