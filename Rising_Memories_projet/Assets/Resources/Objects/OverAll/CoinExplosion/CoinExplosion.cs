using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinExplosion : MonoBehaviour
{
    [SerializeField]
    private GameObject coin;

    public int force;
    public int amount;
    void Start()
    {
        for (int i = 0; i < amount; i++)
        {
            Rigidbody2D c = Instantiate(coin,transform).GetComponent<Rigidbody2D>();
            c.AddForce(new Vector2(Random.Range(-force, force), Random.Range(-force, 2*force)));
        }
    }

}
