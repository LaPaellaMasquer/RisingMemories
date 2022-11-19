using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBobV2 : EnemyScriptMoveBase
{
    [SerializeField]
    private float Radius;
    private CircleCollider2D Collider2DEnnemi;

    private Rigidbody2D rb;

    private bool isGrounded;

    public void Awake()
    {
        this.setCenterCircle(transform);
        this.setRadiusCircle(Radius);
        setPlayer();
    }

    private void Start()
    {
        Collider2DEnnemi = gameObject.GetComponent<CircleCollider2D>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerEnter == true && isGrounded == true)
        {
            isGrounded = false;

            Vector3 dir = player.transform.position - transform.position;
            rb.AddForce(new Vector2(dir.x * 10, Random.Range(100f, 300f)));
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            isGrounded = true;
        }
        else if (collision.transform.CompareTag("Enemy"))
        {
            Vector3 dir = collision.transform.position - transform.position;
            rb.AddForce(new Vector2(dir.x * -10, 100f));
        }
    }
}
