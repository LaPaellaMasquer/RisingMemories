using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    GameObject weapon1,weapon2;
    [SerializeField]
    Vector2 offset;

    public GameObject defaultAttack;


    // Gun
    public Gun playerGun;
    public GameObject fireDirection;
    public GameObject ammo;
    public GameObject cam;
    public Transform playerPos;
    public Vector2 g_offset;
    public GameObject weaponSprite;
    public GameObject canon;

    #region Singleton
    public static PlayerAttack instance;
    [SerializeField] private AudioClip SoundAttack = null ;
    private AudioClip SoundArme = null;
    private AudioSource perso_AudioSource = null;
    private Weapons weapons;


    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'un instance de PlayerAttack dans la scène");
            return;
        }
        perso_AudioSource = GetComponent<AudioSource>();
        instance = this;
    }
    #endregion
    void Start()
    {
        this.ChangeWeaponSlash(defaultAttack);
    }
    public void setSound ( AudioClip sound )
    {
        this.SoundArme = sound;
    }
    public void getUnarmed()
    {
        this.ChangeWeaponSlash(defaultAttack);
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseMenu.isPaused)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            this.GetComponent<Animator>().SetTrigger("Attack");
            GameObject attack = Instantiate(weapon1);
            if (Input.GetAxis("Vertical") > .2)
            {
                attack.transform.position = (Vector2) this.transform.position + new Vector2(0,1);
                attack.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
            }
            else
            {
                attack.transform.position = this.transform.position;
                if (PlayerMovements.instance.flipped)
                {
                    attack.transform.position = (Vector2)attack.transform.position + new Vector2(offset.x, offset.y);
                }
                else
                {
                    attack.GetComponent<SpriteRenderer>().flipX = true;
                    attack.transform.position = (Vector2)attack.transform.position + new Vector2(-offset.x, offset.y);
                }
            }
            attack.transform.SetParent(gameObject.transform);
            if (SoundArme == null ) {
                perso_AudioSource.PlayOneShot(SoundAttack);
            }else{
                  perso_AudioSource.PlayOneShot(SoundArme);
            }
        }

        // Gun Part

        if(playerGun == null)
        {
            return;
        }

        Vector2 sp = GetDirection();
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if(this.playerGun != null)
            {
                // this.playerGun.shot(new Vector3(playerPos.position.x, playerPos.position.y - .4f, 0), sp);
                this.playerGun.shot(new Vector3(canon.transform.position.x, canon.transform.position.y, 0), sp);
            }
        }
        fireDirection.transform.localPosition = ((Vector2) playerPos.position - sp).normalized * -playerGun.gunSize;
        //weaponSprite.transform.LookAt( (Vector2)weaponSprite.transform.position - ((Vector2)playerPos.position - sp).normalized);
        weaponSprite.transform.LookAt( fireDirection.transform);
        weaponSprite.transform.Rotate(0, -90, 0);
    }

    public void setOffset(Vector2 _offset)
    {
        this.offset = _offset;
    }

    public void ChangeWeaponSlash(GameObject hit1, GameObject hit2 = null )
    {
        this.weapon1 = hit1;
        if(hit2 != null)
        {
            this.weapon2 = hit2;
        }
    }

    public void changeGun(Gun _g)
    {
        this.playerGun = _g;
        if(_g != null)
        {
            this.canon.transform.localPosition = _g.canonOffset;
        }
        
    }

    private Vector2 GetDirection()
    {
        Vector3 cameraPos = cam.transform.position;
        float x = 0f;
        float y = 0f;
        x += cameraPos.x;
        y += cameraPos.y;

        Vector3 mouse = Input.mousePosition;
        x += mouse.x / (Screen.height / cam.GetComponent<Camera>().orthographicSize);
        y += mouse.y / Screen.height * cam.GetComponent<Camera>().orthographicSize;

        x += g_offset.x;
        y += g_offset.y;

        Vector2 end = new Vector2(x, y);
        return end;
    }
}
