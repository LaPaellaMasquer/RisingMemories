using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    public static PlayerMovements instance;

    List<Force> extForces;
    public float maxFallingSpeed = 40;

    public bool flipped;

    public int maxJumpAmount = 1;
    public int jumpAmount = 0;
    public bool canJump = true;

    //Player State Variables
    public bool isWalking;

    //Variable to adjust balancing.
    public float moveSpeed;
    public float jumpForce;

    [SerializeField]
    public float speedBonus;

    //Ground Check (for jump)
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask collisionLayers;

    //Wall Jump
    public Transform rWallCheck, lWallCheck;
    public bool wallJumpOn;
    public bool onWall;

    //Fast acces to GameObject
    public Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Vector3 velocity = Vector3.zero;


    private float horizontalMovement;
    private float verticalMovement;

    public Vector2 totalExtForces;
    [SerializeField] private AudioClip Default_Jump = null;
    private AudioSource perso_AudioSaute;
    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        jumpAmount = maxJumpAmount;
        extForces = new List<Force>();
        perso_AudioSaute= GetComponent<AudioSource>();
        if (rb == null)
        {
            Debug.LogError("Player Movements cannot acces RigidBody2D");
        }
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'un instance de PlayerMovements dans la scène");
            return;
        }

        instance = this;
    }



    void FixedUpdate()
    {
        if (canJump)
        {
            if (checkGround())
            {
                RefillJump();
            }
        }

        horizontalMovement = Input.GetAxis("Horizontal") * (moveSpeed+speedBonus) * Time.deltaTime;

        MovePlayer();
        
    }

    void Update()
    {

        if (Input.GetButtonDown("Jump") && jumpAmount>0 && canJump)
        {
            Jump();
        }

        Flip(rb.velocity.x);
        if(horizontalMovement > 0.3f){
            gameObject.GetComponent<Animator>().SetFloat("Hspeed", horizontalMovement);
        }
        else
        {
            gameObject.GetComponent<Animator>().SetFloat("Hspeed", -horizontalMovement);
        }
        
    }

    public void RefillJump()
    {
        PlayerEquipements.instance.landingEvent();
        jumpAmount = maxJumpAmount;
    }
    void MovePlayer()
    {
        if (!onWall)
        {
            if (horizontalMovement > 0.5 || horizontalMovement < -0.5)
            {
                isWalking = true;
            }
            else
            {
                isWalking = false;
            }

            Vector2 oldTotal = totalExtForces;
            totalExtForces = Vector2.zero;
            foreach (Force frc in extForces)
            {
                Vector2 v = frc.getForce();
                if (v == Vector2.zero)
                {
                    extForces.Remove(frc);
                }
                else
                {
                    totalExtForces = new Vector2(totalExtForces.x + v.x, v.y + totalExtForces.y);
                }
            }
            Vector2 targetVelocity = new Vector2(horizontalMovement + totalExtForces.x - oldTotal.x, rb.velocity.y + totalExtForces.y - oldTotal.y);
            
            //Max fall Speed Control
            if (-targetVelocity.y > maxFallingSpeed)
            {
                targetVelocity = new Vector2(targetVelocity.x, -maxFallingSpeed);
            }

            //Movements final appliance
            rb.velocity = targetVelocity;
        }
        else
        {

        }
    }
    void Flip(float _velocity)
    {
        if (_velocity > 0.1f)
        {
            spriteRenderer.flipX = true;
            //this.transform.rotation = new Quaternion(0,180,0,0);
            flipped = true;
        }
        else if (_velocity < -0.1f)
        {
            spriteRenderer.flipX = false;
            //this.transform.rotation = new Quaternion(0, 0, 0, 0);
            flipped = false;
        }
    }

    public void AddForce(Force f)
    {
        extForces.Add(f);
    }

    public bool checkGround()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, collisionLayers);
    }
    public IEnumerator jumpCD()
    {
        yield return new WaitForSeconds(0.2f);
        canJump = true;
    }
    void Jump()
    {
        canJump = false;
        StartCoroutine(jumpCD());
        PlayerEquipements.instance.jumpEvent();
        jumpAmount -= 1;
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(new Vector2(0f, jumpForce));
        perso_AudioSaute.PlayOneShot(Default_Jump);
    }
    public void jumpSpecial(Vector2 jumpVect)
    {
        canJump = false;
        StartCoroutine(jumpCD());
        jumpAmount -= 1;
        rb.velocity = new Vector2(0, 0);
        rb.AddForce(jumpVect);
    }
}