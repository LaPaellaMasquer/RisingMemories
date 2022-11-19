using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDidierV2 : EnemyScriptMoveBase
{
    public float Radius;
    public BoxCollider2D Collider2DEnnemi;

    private Rigidbody2D rb;

    public float speed;

    private bool isAttack = true;

    private const string FILE_PATH = "Enemy/";

    public void Awake()
    {
        this.setCenterCircle(transform);
        this.setRadiusCircle(Radius);
        setPlayer();
    }

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerEnter)
        {
            if (Vector3.Distance(player.transform.position, transform.position) < 5f)
            {
                if (isAttack == true)
                {
                    GameObject nodePrefab;
                    nodePrefab = Resources.Load(FILE_PATH + "LancePrefabe") as GameObject;
                    nodePrefab = GameObject.Instantiate(nodePrefab, transform.position, Quaternion.identity);
                    LanceAttack LaunchLance = nodePrefab.GetComponent<LanceAttack>();
                    LaunchLance.isClone = true;
                    StartCoroutine(delay());
                }
            }
            else
            {
                Vector3 dir = player.transform.position - transform.position;
                transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
            }
        }
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
            yield return new WaitForSeconds(1f);
            isAttack = true;
        }
    }
}
