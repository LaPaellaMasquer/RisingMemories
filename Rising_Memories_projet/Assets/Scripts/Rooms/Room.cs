using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room 
{
    private int id = -1;
    private int nbAccess = 0;
    private Vector3 pos;
    private Vector2Int size;
    private bool[] access = new bool[4] { false, false, false, false }; // LEFT RIGHT TOP BOTTOM

    public void ChangeAccess(int dir)
    {
        access[dir] = !access[dir];

        if (access[dir])
        {
            nbAccess++;
        }
        else
        {
            nbAccess--;
        }
    }

    public bool GetAccess(int dir)
    {
        return access[dir];
    }

    public void ChangeId(int id)
    {
        this.id=id;
    }

    public int GetId()
    {
        return this.id;
    }

    public int GetNbAccess()
    {
        return nbAccess;
    }

    public void setPos(Vector3 pos)
    {
        this.pos = pos;
    }

    public Vector3 getPos()
    {
        return this.pos;
    }

    public void setSize(Vector2Int size)
    {
        this.size = size;
    }

    public Vector2Int getSize()
    {
        return this.size;
    }
}
