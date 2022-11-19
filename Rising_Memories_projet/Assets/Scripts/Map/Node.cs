using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node {
    public enum t_node { 
        DUNGEON,
        JUMP,
        ARENA,
        SHOP,
        BOSS,
    }

    public GameObject nodeprefab;
    private t_node nodetype;
    private Vector3 pos;
    public int nparents = 0;
    public List<Node> next = new List<Node>();
    public string state = "INACTIVE";


    public Node(t_node type)
    {
        this.nodetype = type;
    }

    public Vector3 getPos() 
    {
        return this.pos;
    }

    public void setPos(Vector3 pos)
    {
        this.pos = pos;
    }

    public t_node getType()
    {
        return this.nodetype;
    }
}
