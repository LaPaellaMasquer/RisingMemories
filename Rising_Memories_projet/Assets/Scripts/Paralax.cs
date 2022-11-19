using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralax : MonoBehaviour
{
    private GameObject player;
    private Vector2 dungeonsize;
    [SerializeField]
    private GameObject[] layers;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        dungeonsize.x = Vector2.Distance(new Vector2(DungeonGenerator.maxpos.x, 0), new Vector2(DungeonGenerator.minpos.x, 0));
        dungeonsize.y = Vector2.Distance(new Vector2(DungeonGenerator.maxpos.y, 0), new Vector2(DungeonGenerator.minpos.y, 0));
        transform.position = Vector2.Lerp(DungeonGenerator.maxpos, DungeonGenerator.minpos,0.5f);
        float scale = Mathf.Max(dungeonsize.x / layers[0].GetComponent<SpriteRenderer>().size.x, dungeonsize.y / layers[0].GetComponent<SpriteRenderer>().size.y);
        transform.localScale = new Vector3(scale, scale, 1);
    }

    // Update is called once per frame
    void Update()
    {
        float speed = player.GetComponent<PlayerMovements>().rb.velocity.x * Time.deltaTime;
        layers[0].transform.Translate(new Vector3(speed*0.1f, 0, 0));
        layers[1].transform.Translate(new Vector3(speed * 0.2f, 0, 0));
        layers[2].transform.Translate(new Vector3(speed * 0.3f, 0, 0));
    }
}
