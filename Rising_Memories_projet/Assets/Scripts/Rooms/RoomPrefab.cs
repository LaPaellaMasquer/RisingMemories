using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomPrefab : MonoBehaviour
{
    public bool isExit;
    public bool isSpawn;
    public bool isEnemy;
    public GameObject spawn;
    public GameObject exit;
    public GameObject enemycontainer;

    // Start is called before the first frame update
    void Start()
    {
        if (isSpawn)
        {
            GameObject player = GameObject.Find("Player");
            player.transform.position = spawn.transform.position;
            player.GetComponent<PlayerHealth>().isInvincible = false;
            player.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,1f);
        }
        exit.SetActive(isExit);
        spawn.SetActive(isSpawn);
        if (enemycontainer != null)
        {
            enemycontainer.SetActive(isEnemy);
        }
    }
}
