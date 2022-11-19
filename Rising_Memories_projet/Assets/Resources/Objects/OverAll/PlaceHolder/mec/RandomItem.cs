using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class RandomItem : MonoBehaviour
{
    [SerializeField]
    public Slot choice;
    public enum Slot
    {
        All,
        Attack,
        Move,
        Jump,
        Gun
    }
    public GameObject[] attackItems;
    public GameObject[] moveItems;
    public GameObject[] jumpItems;
    public GameObject[] gunItems;
    public bool isTreasure = true;
    
    public GameObject randItem()
    {
        int ind = -1;
        GameObject res = null;
        if (choice == Slot.All)
        {
            int select = Random.Range(0, 4);
            switch (select)
            {
                case 0:
                    choice = Slot.Attack;
                    break;
                case 1:
                    choice = Slot.Move;
                    break;
                case 2:
                    choice = Slot.Jump;
                    break;
                case 3:
                    choice = Slot.Gun;
                    break;
            }
        }
        switch (choice)
        {
            case Slot.Attack:
                ind = Random.Range(0, attackItems.Length);
                res = Instantiate(attackItems[ind]);
                break;
            case Slot.Move:
                ind = Random.Range(0, moveItems.Length);
                res = Instantiate(moveItems[ind]);
                break;
            case Slot.Jump:
                ind = Random.Range(0, jumpItems.Length);
                res = Instantiate(jumpItems[ind]);
                break;
            case Slot.Gun:
                ind = Random.Range(0, gunItems.Length);
                res = Instantiate(gunItems[ind]);
                break;
            default:
                break;
        }
        res.transform.position = gameObject.transform.position;
        res.transform.parent = gameObject.transform.parent;
        return res;
    }

    void Start()
    {
        if (isTreasure)
        {
            randItem();
            Destroy(gameObject);
        }
    }
}
