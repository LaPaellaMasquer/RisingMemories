using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class exitLevel : MonoBehaviour
{

    private bool isTriggered=false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (this.enabled)
        {
            GameObject body = collision.gameObject;
            isTriggered = body.name == "Player"? true: isTriggered;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (this.enabled)
        {
            GameObject body = collision.gameObject;
            isTriggered = body.name == "Player" ? false : isTriggered;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isTriggered && Input.GetKeyDown(KeyCode.E))
        {
            if (DungeonGenerator.type == Node.t_node.BOSS)
            {
                var manager = GameObject.FindObjectOfType<DontDestroyOnLoad>();
                manager.RemoveFromDontDestroyOnLoad();
                MapGenerator.reset();
                DungeonGenerator.reset()
;                SceneManager.LoadScene("StartMenu");
            }
            else
            {
                SceneManager.LoadScene("Map");
            }
        }
    }
}
