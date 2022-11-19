using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class MapGenerator : MonoBehaviour
{
    private const string FILE_PATH = "Map/";
    private  Vector3Int SCALE = new Vector3Int(2, 2, 2);
    public int nbnode = 10;
    private static Node root;

    private GameObject createNodePrefab(Vector3 pos, Node.t_node type)
    {
        GameObject nodePrefab;
        nodePrefab = Resources.Load(FILE_PATH + "node") as GameObject;
        nodePrefab = GameObject.Instantiate(nodePrefab, pos, Quaternion.identity);
        nodePrefab.transform.localScale = SCALE;
        nodePrefab.transform.SetParent(transform, false);
        nodePrefab.GetComponent<NodePrefab>().SwitchSprite(type);

        return nodePrefab;
    }

    public void assignNode(int nstage)
    {
        GameObject nodePrefab;
        GameObject nextPrefab;
        Node node = root;
        GameObject linkPrefab;
        LineRenderer link;
        int count = node.next.Count;
        int nchild = count;
        float nextX = 0;
        float coeff = 1f;
        Vector3 pos = new Vector3(0, -(nstage - 1) / 2, 0);
        Queue<Node> nextNodes = new Queue<Node>();
        
        node.setPos(pos);
        nodePrefab = createNodePrefab(node.getPos(), node.getType());
        nodePrefab.GetComponent<NodePrefab>().node = node;
        nodePrefab.GetComponent<NodePrefab>().setActive(true);
        node.nodeprefab = nodePrefab;

        do
        {
            foreach (var n in node.next)
            {
                if (n.nodeprefab == null)
                {
                    if (nchild == count && count != 1)
                    {
                        nextX = pos.x - coeff * (count - 1) / 2;
                    }
                    else if (count == 1)
                    {
                        nextX = pos.x + coeff * (n.nparents-1) /2;
                    }

                    n.setPos(new Vector3(nextX, pos.y + coeff, 0));

                    nextPrefab = createNodePrefab(n.getPos(), n.getType());

                    nextPrefab.GetComponent<NodePrefab>().node = n;
                    n.nodeprefab = nextPrefab;
                    nodePrefab.GetComponent<NodePrefab>().next.Add(nextPrefab);
                    nextNodes.Enqueue(n);
                    nextX += coeff;
                }
                else
                {
                    nodePrefab.GetComponent<NodePrefab>().next.Add(n.nodeprefab);
                }
                linkPrefab  = Resources.Load(FILE_PATH + "link") as GameObject;
                linkPrefab = GameObject.Instantiate(linkPrefab, transform);

                link = linkPrefab.GetComponent<LineRenderer>();
                link.SetPosition(0, pos);
                link.SetPosition(1, n.getPos());

                nchild--;
            }
            node = nextNodes.Dequeue();
            nodePrefab = node.nodeprefab;
            pos = node.getPos();
            count = node.next.Count;
            nchild = count;
        } while (node.getType() != Node.t_node.BOSS);
    }

    public void Generate()
    {
        int count = 1;
        int r_node;
        int pos = 0;
        Node node = new Node(Node.t_node.DUNGEON);
        Node[] stage = new Node[1];
        List<Node[]> l_stage = new List<Node[]>();
        Node[][] map;

        stage[0] = node;
        l_stage.Add(stage);

        while (count<nbnode-1)
        {
            do
            {
                r_node = Random.Range(1, 5);
            } while (r_node + count > nbnode - 1);

            stage = new Node[r_node];
            for (int i = 0; i < r_node; i++)
            {
                stage[i] = new Node((Node.t_node)Random.Range(0, 4));
            }

            l_stage.Add(stage);
            count += r_node;
        }

        stage = new Node[1];
        stage[0] = new Node(Node.t_node.BOSS);
        l_stage.Add(stage);

        map = l_stage.ToArray();

        for (int i = 0; i < map.Length-1; i++)
        {
            if (map[i].Length == 1)
            {
                for (int j = 0; j < map[i+1].Length; j++)
                {
                    map[i][0].next.Add(map[i + 1][j]);
                    map[i + 1][j].nparents++;
                }
            }
            else if(map[i+1].Length == 1)
            {
                for (int j = 0; j < map[i].Length; j++)
                {
                    map[i][j].next.Add(map[i + 1][0]);
                    map[i + 1][0].nparents++;
                }
            }
            else
            {
                count = map[i + 1].Length;

                for (int j = 0; j < map[i].Length; j++)
                {
                    if (count == 0)
                    {
                        map[i][j].next.Add(map[i + 1][pos - 1]);
                        map[i + 1][pos - 1].nparents++;
                    }
                    else if (j != map[i].Length - 1)
                    {
                        r_node = Random.Range(1, count + 1);

                        for (int k = 0; k < r_node; k++)
                        {
                            map[i][j].next.Add(map[i + 1][pos]);
                            map[i + 1][pos].nparents++;
                            pos++;
                        }

                        if (Random.Range(0, 10) < 5)
                        {
                            pos--;
                            count -= r_node - 1;
                        }
                        else
                        {
                            count -= r_node;
                        }
                    }
                    else
                    {
                        for (int k = pos; k < map[i+1].Length; k++)
                        {
                            map[i][j].next.Add(map[i + 1][k]);
                            map[i + 1][k].nparents++;
                        }
                    }
                }
                pos = 0;
            }
        }
        root = map[0][0];
        assignNode(map.Length);
    }

    private void read()
    {
        GameObject nodeprefab;
        GameObject nextprefab;
        Node node = root;
        Queue<Node> next = new Queue<Node>();
        GameObject linkPrefab;
        LineRenderer link;

        nodeprefab = createNodePrefab(node.getPos(), node.getType());
        nodeprefab.GetComponent<NodePrefab>().node = node;
        nodeprefab.GetComponent<NodePrefab>().setState(node.state);
        node.nodeprefab = nodeprefab;


        do
        {
            foreach (var n in node.next)
            {
                if(n.nodeprefab == null)
                {
                    nextprefab = createNodePrefab(n.getPos(), n.getType());
                    nextprefab.GetComponent<NodePrefab>().node = n;
                    nextprefab.GetComponent<NodePrefab>().setState(n.state);
                    if(nodeprefab.GetComponent<NodePrefab>().getState() == "SELECTED")
                    {
                        nextprefab.GetComponent<NodePrefab>().father = nodeprefab;
                    }
                    n.nodeprefab = nextprefab;
                    nodeprefab.GetComponent<NodePrefab>().next.Add(nextprefab);
                    next.Enqueue(n);
                }
                else
                {
                    nodeprefab.GetComponent<NodePrefab>().next.Add(n.nodeprefab);
                }

                linkPrefab = Resources.Load(FILE_PATH + "link") as GameObject;
                linkPrefab = GameObject.Instantiate(linkPrefab, transform);

                link = linkPrefab.GetComponent<LineRenderer>();
                link.SetPosition(0, node.getPos());
                link.SetPosition(1, n.getPos());
            }

            node = next.Dequeue();
            nodeprefab = node.nodeprefab;

        } while (node.getType() != Node.t_node.BOSS);

    }

    // Start is called before the first frame update
    void Start()
    {
        if( root == null)
        {
            Generate();
        }
        else
        {
            read();
            GameObject.Find("GameManager").GetComponent<DontDestroyOnLoad>().setActiveObj(false);
        }
    }

    public static void reset()
    {
        root = null;
    }
}
