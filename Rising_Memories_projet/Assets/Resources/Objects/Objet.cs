using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Objet : MonoBehaviour
{
    public Sprite img;
    public GameObject objInfoDisplay;
    protected int type;
    public int price = 0;
    public abstract void equipEffect();
    public abstract void unEquipEffect();

    public int getPrice()
    {
        return price;
    }
    public virtual void onUse()
    {

    }

    //Unity Event

    public virtual void onUpdate()
    {

    }

    //Jump related Event
    public virtual void onJump()
    {

    }

    public virtual void onLanding()
    {

    }

    //Attack related Event

    public virtual void onAttack()
    {

    }

    public int getType()
    {
        return this.type;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (objInfoDisplay != null && collision.CompareTag("Player"))
            objInfoDisplay.SetActive(true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (objInfoDisplay != null && collision.CompareTag("Player"))
            objInfoDisplay.SetActive(false);
    }

    #region Apparition/Dispartion Scene
    public void getInBag()
    {
        disapearFromScreen();
        transform.parent = PlayerEquipements.instance.transform;
    }
    public void getOutBag()
    {
        appearOnScreen();
        transform.parent = null;
        transform.rotation = new Quaternion();
        GameObject.Find("GameManager").GetComponent<DontDestroyOnLoad>().RemoveFromDontDestroyOnLoad(this.gameObject);
    }

    public void disapearFromScreen()
    {
        Collider2D hitBox = gameObject.GetComponent<Collider2D>();
        if (hitBox != null)
        {
            hitBox.enabled = false;
        }

        SpriteRenderer render = gameObject.GetComponent<SpriteRenderer>();
        if (render != null)
        {
            render.enabled = false;
        }
    }

    public void appearOnScreen()
    {
        Collider2D hitBox = gameObject.GetComponent<Collider2D>();
        if (hitBox != null)
        {
            hitBox.enabled = true;
        }

        SpriteRenderer render = gameObject.GetComponent<SpriteRenderer>();
        if (render != null)
        {
            render.enabled = true;
        }
    }
    #endregion
}
