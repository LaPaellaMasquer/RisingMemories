using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEquipements : MonoBehaviour
{
    #region Singleton
    public static PlayerEquipements instance;
    [SerializeField] private AudioClip SoundRecolt = null ;
    private AudioSource perso_AudioSource;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'un instance de PlayerEquipement dans la scène");
            return;
        }
        perso_AudioSource = GetComponent<AudioSource>();
        instance = this;
    }
    #endregion

    Objet[] equipements;

    [SerializeField]
    private Text moneyDisplay;

    int money;
    JumpBoost jumpBoost;
    MoveBoost moveBoost;
    Weapons weapon;
    Gun gun;
    public Image[] equipementsDisplay;
    private void Start()
    {
        equipements = new Objet[4];
    }

    private void Update()
    {
        foreach (Objet item in equipements)
        {
            if(item != null)
            {
                item.onUpdate();
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            equipements[0].onUse();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            equipements[1].onUse();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            equipements[2].onUse();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            equipements[3].onUse();
        }
    }

    public void Equip(Objet item)
    {
        int slot = item.getType();
        if (equipements[slot] != null)
        {
            UnEquip(slot);
        }
        perso_AudioSource.PlayOneShot(SoundRecolt);
        disableAllEquipementsEffects();
        equipements[slot] = item;
        equipementsDisplay[slot].sprite = item.img;
        equipementsDisplay[slot].enabled = true;
        item.getInBag();
        ApplyEquipementBuff();
        switch (slot)
        {
            case 0 :
                weapon = (Weapons)item;
                break;
            case 1:
                moveBoost = (MoveBoost) item;
                break;
            case 2:
                jumpBoost = (JumpBoost) item;
                break;
            case 3:
                gun = (Gun)item;
                PlayerAttack.instance.weaponSprite.GetComponent<SpriteRenderer>().sprite = item.img;
                break;
            default:
                break;
        }
    }


    public void UnEquip(int slot)
    {
        disableAllEquipementsEffects();
        equipements[slot].getOutBag();
        equipements[slot] = null;
        ApplyEquipementBuff();

        switch (slot)
        {
            case 0:
                weapon = null;
                break;
            case 1:
                moveBoost = null;
                break;
            case 2:
                jumpBoost = null;
                break;
            default:
                break;
        }
    }
    void disableAllEquipementsEffects()
    {
        foreach (Objet item in equipements)
        {
            if (item != null)
            {
                item.unEquipEffect();
            }

        }
    }
    void ApplyEquipementBuff()
    {
        foreach (Objet item in equipements)
        {
            if (item != null)
            {
                item.equipEffect();
            }
        }
    }


    /*-----------Equipments Event--------------*/

    public void jumpEvent()
    {
        foreach (Objet item in equipements)
        {
            if (item != null)
            {
                item.onJump();
            }
        }
    }

    public void landingEvent()
    {
        foreach (Objet item in equipements)
        {
            if (item != null)
            {
                item.onLanding();
            }
        }
    }

    public int getSold()
    {
        return this.money;
    }

    public void Pay(int amount)
    {
        this.money -= amount;
        this.moneyDisplay.text = this.money.ToString();
    }

    public void GainMoney(int amount)
    {
        this.money += amount;
        this.moneyDisplay.text = this.money.ToString();
    }
}
