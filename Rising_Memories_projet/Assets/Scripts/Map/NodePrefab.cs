using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NodePrefab : MonoBehaviour
{
    public SpriteRenderer sprite_renderer;
    public CircleCollider2D trigger;
    public Sprite[] sprites;
    public List<GameObject> next;
    public GameObject father = null;
    public Node node;

    private Node.t_node type;
    private string state="INACTIVE";

    private void Start()
    {
        if (state == "INACTIVE")
        {
            this.sprite_renderer.color = Color.grey;
        }
    }

    public void SwitchSprite(Node.t_node type)
    {
        this.sprite_renderer.sprite = sprites[(int)type];
        this.type = type;
    }

    public void setActive(bool active)
    {
        if (active)
        {
            this.state = "ACTIVE";
            this.sprite_renderer.color = Color.white;
            this.node.state = this.state;
        }
        else
        {
            this.state = "INACTIVE";
            this.sprite_renderer.color = Color.grey;
            this.node.state = this.state;
        }
    }

    public string getState()
    {
        return this.state;
    }

    public void setState(string state)
    {
        /*if (state == "ACTIVE")
        {
            this.state = state;
            this.sprite_renderer.color = Color.white;
            this.node.state = this.state;
        }*/
        switch (state)
        {
            case "ACTIVE":
                this.state = state;
                this.sprite_renderer.color = Color.white;
                this.node.state = this.state;
                break;

            case "INACTIVE":
                this.state = state;
                this.sprite_renderer.color = Color.grey;
                this.node.state = this.state;
                break;

            case "SELECTED":
                this.state = state;
                this.sprite_renderer.color = Color.green;
                this.node.state = this.state;
                break;

            default:
                break;
        }
    }

    private void switchtScene() {
        DungeonGenerator.type = this.type;
        SceneManager.LoadScene("Level");

    }

    private void OnMouseDown()
    {
        if (state == "ACTIVE")
        {
            foreach (var n in next)
            {
                n.GetComponent<NodePrefab>().setActive(true);
                n.GetComponent<NodePrefab>().father = this.gameObject;
            }
            if (father != null)
            {
                List<GameObject> childs = father.GetComponent<NodePrefab>().next;
                foreach (var n in childs)
                {
                    if (n.transform.position != this.transform.position)
                    {
                        n.GetComponent<NodePrefab>().setActive(false);
                    }
                }
            }
            this.state = "SELECTED";
            this.node.state = this.state;
            this.sprite_renderer.color = Color.green;
            switchtScene();
        }
    }

}
