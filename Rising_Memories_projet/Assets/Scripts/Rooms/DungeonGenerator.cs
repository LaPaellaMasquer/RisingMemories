using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DungeonGenerator : MonoBehaviour
{

    private const string FILE_PATH = "Rooms/";
    private enum Direction
    {
        LEFT,
        RIGHT,
        TOP,
        BOTTOM
    }

    public static Node.t_node type;
    public int nbRooms = 6;
    private Room[,] map;
    private Vector2Int startPos;

    public static Vector2 maxpos;
    public static Vector2 minpos;
    private static int ngeneration = 0;
    private string world = "monde1";

    private Dictionary<Direction, Direction> directions = new Dictionary<Direction, Direction>() {
        {Direction.LEFT, Direction.RIGHT},
        {Direction.RIGHT, Direction.LEFT},
        {Direction.TOP, Direction.BOTTOM},
        {Direction.BOTTOM, Direction.TOP}
    };

    Dictionary<string, Dictionary<string, int[]>> WorldRooms = new Dictionary<string, Dictionary<string, int[]>>()
    {
        {"monde1", new Dictionary<string, int[]>(){ { "Boss", new int[1] { 1 } },
                                                    { "1_1_1_1", new int[1] { 1 } }, // L_R_T_B
                                                    { "1_1_1_0", new int[1] { 12 } },
                                                    { "1_1_0_1", new int[1] { 13 } },
                                                    { "1_1_0_0", new int[1] { 2 } },
                                                    { "1_0_1_1", new int[1] { 15 } },
                                                    { "1_0_1_0", new int[1] { 5 } },
                                                    { "1_0_0_1", new int[1] { 9 } },
                                                    { "1_0_0_0", new int[1] { 6 } },
                                                    { "0_1_1_1", new int[1] { 14 } },
                                                    { "0_1_1_0", new int[1] { 8 } },
                                                    { "0_1_0_1", new int[1] { 7 } },
                                                    { "0_1_0_0", new int[1] { 3 } },
                                                    { "0_0_1_1", new int[1] { 4 } },
                                                    { "0_0_1_0", new int[1] { 10 } },
                                                    { "0_0_0_1", new int[1] { 11 } },
                                                    { "Arena", new int[1] { 1 } },
                                                    { "Shop", new int[1] { 1 } },
                                                  }
        }
    };

    private GameObject createRoom(int id, Vector3 worldpos, bool isExit, bool isSpawn)
    {

        GameObject room = Resources.Load(FILE_PATH + world + "_" + id.ToString()) as GameObject; //FILE_PATH + world + "_" + id.ToString() +".prefab") as GameObject;
        room.GetComponent<RoomPrefab>().isExit = isExit;
        room.GetComponent<RoomPrefab>().isSpawn = isSpawn;
        room.GetComponent<RoomPrefab>().isEnemy = !isSpawn;

        if (isSpawn)
        {
            room = GameObject.Instantiate(room, worldpos, Quaternion.identity);
            room.transform.SetParent(transform, false);

            if (ngeneration == 1)
            {
                GameObject r_jump = Resources.Load("Objects/OverAll/PlaceHolder/itemPh") as GameObject;
                Vector3 jumppos = room.GetComponent<RoomPrefab>().spawn.transform.position;

                r_jump.GetComponent<RandomItem>().choice = RandomItem.Slot.Jump;
                r_jump.GetComponent<RandomItem>().isTreasure = true;
                r_jump = GameObject.Instantiate(r_jump, jumppos, Quaternion.identity);
                r_jump.transform.SetParent(transform, false);
            }
        }

        return room;
    }

    // call it once room is instantiate
    private void addTreasure(GameObject room)
    {
        GameObject r_obj = Resources.Load("Objects/OverAll/PlaceHolder/itemPh") as GameObject;
        Vector3 objpos = room.GetComponent<RoomPrefab>().exit.transform.position;

        r_obj.GetComponent<RandomItem>().choice = RandomItem.Slot.All;
        r_obj.GetComponent<RandomItem>().isTreasure = true;
        r_obj = GameObject.Instantiate(r_obj, objpos, Quaternion.identity);
        r_obj.transform.SetParent(transform, false);

    }

    private void AssignWalls(Vector2Int pos, Vector3 worldpos, Vector2 roomsize, bool istall)
    {
        GameObject wallload = Resources.Load(FILE_PATH + "wall") as GameObject;
        GameObject wall;
        Vector3 wallpos = worldpos;

        if (map[pos.x + 1, pos.y] == null)
        {
            if (istall)
            {
                wallload = Resources.Load(FILE_PATH + "wall_long") as GameObject;
            }
            wallpos.x += roomsize.x;
            wall = GameObject.Instantiate(wallload, wallpos, Quaternion.identity);
            wall.transform.SetParent(transform, false);
            wallload = Resources.Load(FILE_PATH + "wall") as GameObject;
        }

        if (map[pos.x - 1, pos.y] == null)
        {
            if (istall)
            {
                wallload = Resources.Load(FILE_PATH + "wall_long") as GameObject;
            }
            wallpos = worldpos;
            wallpos.x += -roomsize.x;
            wall = GameObject.Instantiate(wallload, wallpos, Quaternion.identity);
            wall.transform.SetParent(transform, false);
            wallload = Resources.Load(FILE_PATH + "wall") as GameObject;
        }

        if (map[pos.x, pos.y + 1] == null)
        {
            wallpos = worldpos;
            wallpos.y += -roomsize.y;
            wall = GameObject.Instantiate(wallload, wallpos, Quaternion.identity);
            wall.transform.SetParent(transform, false);
        }

        if (map[pos.x, pos.y - 1] == null)
        {
            wallpos = worldpos;
            wallpos.y += roomsize.y;
            wall = GameObject.Instantiate(wallload, wallpos, Quaternion.identity);
            wall.transform.SetParent(transform, false);
        }

        if (map[pos.x + 1, pos.y + 1] == null)
        {
            wallpos = worldpos;
            wallpos.x += roomsize.x;
            wallpos.y += -roomsize.y;
            wall = GameObject.Instantiate(wallload, wallpos, Quaternion.identity);
            wall.transform.SetParent(transform, false);
        }

        if (map[pos.x + 1, pos.y - 1] == null)
        {
            wallpos = worldpos;
            wallpos.x += roomsize.x;
            wallpos.y += roomsize.y;
            wall = GameObject.Instantiate(wallload, wallpos, Quaternion.identity);
            wall.transform.SetParent(transform, false);
        }

        if (map[pos.x - 1, pos.y + 1] == null)
        {
            wallpos = worldpos;
            wallpos.x += -roomsize.x;
            wallpos.y += -roomsize.y;
            wall = GameObject.Instantiate(wallload, wallpos, Quaternion.identity);
            wall.transform.SetParent(transform, false);
        }

        if (map[pos.x - 1, pos.y - 1] == null)
        {
            wallpos = worldpos;
            wallpos.x += -roomsize.x;
            wallpos.y += roomsize.y;
            wall = GameObject.Instantiate(wallload, wallpos, Quaternion.identity);
            wall.transform.SetParent(transform, false);
        }
    }

    private void AssignRooms(List<Direction> path)
    {
        GameObject room;
        Vector2Int roomsize;
        Vector3 worldpos = new Vector3(0, 0, 0);
        Vector2Int nextpos = startPos;
        Vector2Int coeff = new Vector2Int(0, 0);
        string roomPath = "";
        int bdir;
        int r_id;
        int id;
        bool isExit;
        int count = 1;
        bool isTreasure = false;
        bool istall;

        for (int i = 0; i < 4; i++)
        {
            bdir = map[nextpos.x, nextpos.y].GetAccess(i) ? 1 : 0;
            roomPath += bdir.ToString();
            if (i != 3)
            {
                roomPath += "_";
            }
        }

        r_id = Random.Range(0, WorldRooms[world][roomPath].Length);
        id = WorldRooms[world][roomPath][r_id];
        map[nextpos.x, nextpos.y].ChangeId(id);

        room = createRoom(id, worldpos, false, true);

        room.transform.Find("floor").GetComponent<Tilemap>().CompressBounds();
        roomsize = (Vector2Int)room.transform.Find("floor").GetComponent<Tilemap>().size;
        roomPath = "";

        map[nextpos.x, nextpos.y].setPos(worldpos);
        map[nextpos.x, nextpos.y].setSize(roomsize);

        maxpos = roomsize / 2;
        minpos = -roomsize / 2;

        istall = !map[nextpos.x, nextpos.y].GetAccess((int)Direction.LEFT) || !map[nextpos.x, nextpos.y].GetAccess((int)Direction.RIGHT) && !map[nextpos.x, nextpos.y].GetAccess((int)Direction.TOP) && !map[nextpos.x, nextpos.y].GetAccess((int)Direction.BOTTOM);
        AssignWalls(nextpos, worldpos, roomsize, istall);

        foreach (var dir in path)
        {
            switch (dir)
            {
                case Direction.LEFT:
                    nextpos.x--;
                    coeff = new Vector2Int(-1, 0);
                    break;
                case Direction.RIGHT:
                    nextpos.x++;
                    coeff = new Vector2Int(1, 0);
                    break;
                case Direction.TOP:
                    nextpos.y--;
                    coeff = new Vector2Int(0, 1);
                    break;
                case Direction.BOTTOM:
                    nextpos.y++;
                    coeff = new Vector2Int(0, -1);
                    break;
            }
            if (map[nextpos.x, nextpos.y].GetId() == -1)
            {
                count++;
                isExit = count == nbRooms;
                for (int i = 0; i < 4; i++)
                {
                    bdir = map[nextpos.x, nextpos.y].GetAccess(i) ? 1 : 0;
                    roomPath += bdir.ToString();
                    if (i != 3)
                    {
                        roomPath += "_";
                    }
                }

                r_id = Random.Range(0, WorldRooms[world][roomPath].Length);
                id = WorldRooms[world][roomPath][r_id];
                map[nextpos.x, nextpos.y].ChangeId(id);

                room = createRoom(id, worldpos, isExit, false);
                room.transform.Find("floor").GetComponent<Tilemap>().CompressBounds();
                worldpos.x = worldpos.x + coeff.x * (room.transform.Find("floor").GetComponent<Tilemap>().size.x / 2 + roomsize.x / 2);
                worldpos.y = worldpos.y + coeff.y * (room.transform.Find("floor").GetComponent<Tilemap>().size.y / 2 + roomsize.y / 2);

                room = GameObject.Instantiate(room, worldpos, Quaternion.identity);
                room.transform.SetParent(transform, false);

                if (!isTreasure && !isExit)
                {
                    isTreasure = Random.Range(0, 100) < 25;
                    if (isTreasure)
                    {
                        addTreasure(room);
                    }
                }

                roomPath = "";
                roomsize = (Vector2Int)room.transform.Find("floor").GetComponent<Tilemap>().size;

                map[nextpos.x, nextpos.y].setPos(worldpos);
                map[nextpos.x, nextpos.y].setSize(roomsize);

                istall = !map[nextpos.x, nextpos.y].GetAccess((int)Direction.LEFT) || !map[nextpos.x, nextpos.y].GetAccess((int)Direction.RIGHT) && !map[nextpos.x, nextpos.y].GetAccess((int)Direction.TOP) && !map[nextpos.x, nextpos.y].GetAccess((int)Direction.BOTTOM);
                AssignWalls(nextpos, worldpos, roomsize, istall);

                maxpos.x = Mathf.Max(maxpos.x, worldpos.x + Mathf.Sign(worldpos.x) * roomsize.x / 2);
                minpos.x = Mathf.Min(minpos.x, worldpos.x + Mathf.Sign(worldpos.x) * roomsize.x / 2);

                maxpos.y = Mathf.Max(maxpos.y, worldpos.y + Mathf.Sign(worldpos.y) * roomsize.y / 2);
                minpos.y = Mathf.Min(minpos.y, worldpos.y + Mathf.Sign(worldpos.y) * roomsize.y / 2);
            }
            else
            {
                worldpos = map[nextpos.x, nextpos.y].getPos();
                roomsize = map[nextpos.x, nextpos.y].getSize();
            }
        }
    }

    public void GenerateDungeon()
    {
        Direction r_dir;
        List<Direction> path = new List<Direction>();
        Vector2Int pos = new Vector2Int(nbRooms, nbRooms);
        Vector2Int nextpos = pos;
        Vector2Int range = new Vector2Int(nbRooms * 2, nbRooms * 2);
        int count = 1;

        map = new Room[nbRooms * 2, nbRooms * 2];
        startPos = pos;
        map[nbRooms, nbRooms] = new Room();

        while (count < nbRooms)
        {
            r_dir = (Direction)Random.Range(0, 4);

            switch (r_dir)
            {
                case Direction.LEFT:
                    nextpos.x--;
                    break;
                case Direction.RIGHT:
                    nextpos.x++;
                    break;
                case Direction.TOP:
                    nextpos.y--;
                    break;
                case Direction.BOTTOM:
                    nextpos.y++;
                    break;
            }

            if (nextpos != range)
            {
                if (map[nextpos.x, nextpos.y] == null)
                {
                    map[pos.x, pos.y].ChangeAccess((int)r_dir);
                    map[nextpos.x, nextpos.y] = new Room();
                    map[nextpos.x, nextpos.y].ChangeAccess((int)directions[r_dir]);
                    pos = nextpos;
                    count++;
                    path.Add(r_dir);
                }
                else
                {
                    if (!map[nextpos.x, nextpos.y].GetAccess((int)directions[r_dir]))
                    {
                        map[pos.x, pos.y].ChangeAccess((int)r_dir);
                        map[nextpos.x, nextpos.y].ChangeAccess((int)directions[r_dir]);
                    }
                    if (map[nextpos.x, nextpos.y].GetNbAccess() < 4)
                    {
                        pos = nextpos;
                        path.Add(r_dir);
                    }
                    else
                    {
                        nextpos = pos;
                    }
                }
            }
        }

        AssignRooms(path);
    }

    private void createLava(Vector3 start, Vector3 finish)
    {
        GameObject lava;
        if (world == "monde1")
        {
            if (ngeneration < 3)
            {
                start.y += -10;
            }
            else
            {
                start.y += -6;
            }
        }
        lava = Resources.Load(FILE_PATH + "lava") as GameObject;
        lava = GameObject.Instantiate(lava, start, Quaternion.identity);
        lava.GetComponent<Lava>().ismoving = true;
        lava.GetComponent<Lava>().setMovement(finish);
        lava.transform.SetParent(this.transform, false);
    }

    public void GenerateJump()
    {
        GameObject room;
        GameObject wallload;
        GameObject wall;
        Vector3Int roomsize;
        Vector3 lavapos = new Vector3(0, 0, 0);
        int totalHeight = 0;
        Vector3 worldpos = new Vector3(0, 0, 0);
        Vector3 wallpos = worldpos;
        string roomPath = "0_0_1_0";
        int r_id = Random.Range(0, WorldRooms[world][roomPath].Length);
        int id = WorldRooms[world][roomPath][r_id];

        room = createRoom(id, worldpos, false, true);

        room.transform.Find("floor").GetComponent<Tilemap>().CompressBounds();
        roomsize = room.transform.Find("floor").GetComponent<Tilemap>().size;

        //assign walls
        wallload = Resources.Load(FILE_PATH + "wall") as GameObject;
        wallpos.x += roomsize.x;
        wall = GameObject.Instantiate(wallload, wallpos, Quaternion.identity);
        wall.transform.SetParent(transform, false);

        wallpos.y += roomsize.y;
        wall = GameObject.Instantiate(wallload, wallpos, Quaternion.identity);
        wall.transform.SetParent(transform, false);

        wallpos.x += -roomsize.x;
        wall = GameObject.Instantiate(wallload, wallpos, Quaternion.identity);
        wall.transform.SetParent(transform, false);

        wallpos.x += -roomsize.x;
        wall = GameObject.Instantiate(wallload, wallpos, Quaternion.identity);
        wall.transform.SetParent(transform, false);

        wallpos.y += -roomsize.y;
        wall = GameObject.Instantiate(wallload, wallpos, Quaternion.identity);
        wall.transform.SetParent(transform, false);

        lavapos.y = -roomsize.y / 2;
        totalHeight += roomsize.y;

        minpos.x = -room.transform.Find("floor").GetComponent<Tilemap>().size.x / 2;
        minpos.y = -room.transform.Find("floor").GetComponent<Tilemap>().size.y / 2;

        roomPath = "0_0_1_1";

        for (int i = 0; i < nbRooms - 1; i++)
        {
            r_id = Random.Range(0, WorldRooms[world][roomPath].Length);
            id = WorldRooms[world][roomPath][r_id];

            room = createRoom(id, worldpos, false, false);
            room.GetComponent<RoomPrefab>().isEnemy = false;

            room.transform.Find("floor").GetComponent<Tilemap>().CompressBounds();
            worldpos.y += room.transform.Find("floor").GetComponent<Tilemap>().size.y / 2 + roomsize.y / 2;

            room = GameObject.Instantiate(room, worldpos, Quaternion.identity);
            room.transform.SetParent(transform, false);

            roomsize = room.transform.Find("floor").GetComponent<Tilemap>().size;
            totalHeight += roomsize.y - 2;

            //assign walls
            wallpos = worldpos;
            wallload = Resources.Load(FILE_PATH + "wall_long") as GameObject;
            wallpos.x += roomsize.x;
            wall = GameObject.Instantiate(wallload, wallpos, Quaternion.identity);
            wall.transform.SetParent(transform, false);

            wallpos = worldpos;
            wallpos.x += -roomsize.x;
            wall = GameObject.Instantiate(wallload, wallpos, Quaternion.identity);
            wall.transform.SetParent(transform, false);
        }

        roomPath = "0_0_0_1";
        r_id = Random.Range(0, WorldRooms[world][roomPath].Length);
        id = WorldRooms[world][roomPath][r_id];

        room = createRoom(id, worldpos, true, false);
        room.GetComponent<RoomPrefab>().isEnemy = false;
        room.transform.Find("floor").GetComponent<Tilemap>().CompressBounds();
        worldpos.y += room.transform.Find("floor").GetComponent<Tilemap>().size.y / 2 + roomsize.y / 2;
        room = GameObject.Instantiate(room, worldpos, Quaternion.identity);
        room.transform.SetParent(transform, false);

        room.transform.Find("floor").GetComponent<Tilemap>().CompressBounds();
        roomsize = room.transform.Find("floor").GetComponent<Tilemap>().size;
        totalHeight += roomsize.y;

        //assign walls
        wallload = Resources.Load(FILE_PATH + "wall") as GameObject;
        wallpos.x += roomsize.x;
        wall = GameObject.Instantiate(wallload, wallpos, Quaternion.identity);
        wall.transform.SetParent(transform, false);

        wallpos.y += -roomsize.y;
        wall = GameObject.Instantiate(wallload, wallpos, Quaternion.identity);
        wall.transform.SetParent(transform, false);

        wallpos.x += -roomsize.x;
        wall = GameObject.Instantiate(wallload, wallpos, Quaternion.identity);
        wall.transform.SetParent(transform, false);

        wallpos.x += -roomsize.x;
        wall = GameObject.Instantiate(wallload, wallpos, Quaternion.identity);
        wall.transform.SetParent(transform, false);

        wallpos.y += roomsize.y;
        wall = GameObject.Instantiate(wallload, wallpos, Quaternion.identity);
        wall.transform.SetParent(transform, false);

        maxpos.x = worldpos.x + room.transform.Find("floor").GetComponent<Tilemap>().size.x / 2;
        maxpos.y = worldpos.y + roomsize.y / 2;

        createLava(lavapos, new Vector3(0, totalHeight, 0));

    }

    public void GenerateBoss()
    {
        GameObject room;

        room = Resources.Load(FILE_PATH + world + "_Boss" + WorldRooms[world]["Boss"][0].ToString()) as GameObject;
        room.GetComponent<RoomPrefab>().isSpawn = true;
        room.GetComponent<RoomPrefab>().isEnemy = false;

        room.transform.Find("floor").GetComponent<Tilemap>().CompressBounds();
        maxpos.x = room.transform.Find("floor").GetComponent<Tilemap>().size.x / 2;
        maxpos.y = room.transform.Find("floor").GetComponent<Tilemap>().size.y / 2;
        minpos.x = -room.transform.Find("floor").GetComponent<Tilemap>().size.x / 2;
        minpos.y = -room.transform.Find("floor").GetComponent<Tilemap>().size.y / 2;

        room = GameObject.Instantiate(room, new Vector3(0, 0, 0), Quaternion.identity);
        room.transform.SetParent(transform, false);

    }

    public void GenerateArena()
    {
        GameObject room;
        room = Resources.Load(FILE_PATH + world + "_Arena" + WorldRooms[world]["Arena"][0].ToString()) as GameObject;
        room.GetComponent<RoomPrefab>().isSpawn = true;
        room.GetComponent<RoomPrefab>().isEnemy = false;

        room.transform.Find("floor").GetComponent<Tilemap>().CompressBounds();
        maxpos.x = room.transform.Find("floor").GetComponent<Tilemap>().size.x / 2;
        maxpos.y = room.transform.Find("floor").GetComponent<Tilemap>().size.y / 2;
        minpos.x = -room.transform.Find("floor").GetComponent<Tilemap>().size.x / 2;
        minpos.y = -room.transform.Find("floor").GetComponent<Tilemap>().size.y / 2;

        room = GameObject.Instantiate(room, new Vector3(0, 0, 0), Quaternion.identity);
        room.transform.SetParent(transform, false);
    }

    public void GenerateShop()
    {
        GameObject room;
        room = Resources.Load(FILE_PATH + world + "_Shop" + WorldRooms[world]["Shop"][0].ToString()) as GameObject;
        room.GetComponent<RoomPrefab>().isSpawn = true;
        room.GetComponent<RoomPrefab>().isExit = true;
        room.GetComponent<RoomPrefab>().isEnemy = false;

        room.transform.Find("floor").GetComponent<Tilemap>().CompressBounds();
        maxpos.x = room.transform.Find("floor").GetComponent<Tilemap>().size.x / 2;
        maxpos.y = room.transform.Find("floor").GetComponent<Tilemap>().size.y / 2;
        minpos.x = -room.transform.Find("floor").GetComponent<Tilemap>().size.x / 2;
        minpos.y = -room.transform.Find("floor").GetComponent<Tilemap>().size.y / 2;

        room = GameObject.Instantiate(room, new Vector3(0, 0, 0), Quaternion.identity);
        room.transform.SetParent(transform, false);
    }


    public void Generate()
    {
        ngeneration++;
        switch (type)
        {
            case Node.t_node.JUMP:
                GenerateJump();
                break;
            case Node.t_node.ARENA:
                GenerateArena();
                break;
            case Node.t_node.SHOP:
                GenerateShop();
                break;
            case Node.t_node.BOSS:
                GenerateBoss();
                break;
            default:
                GenerateDungeon();
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (ngeneration != 0)
        {
            GameObject[] managers = GameObject.FindGameObjectsWithTag("GameManager");
            managers[1].SetActive(false);
            GameObject.Destroy(managers[1]);
            GameObject.Find("GameManager").GetComponent<DontDestroyOnLoad>().setActiveObj(false);
            GameObject.Find("Main Camera").SetActive(false);
            GameObject.Find("Canvas").SetActive(false);
            GameObject.Find("Player").SetActive(false);
            GameObject.Find("GameManager").GetComponent<DontDestroyOnLoad>().setActiveObj(true);
        }
        Generate();
    }

    public static void reset()
    {
        ngeneration = 0;
    }
}