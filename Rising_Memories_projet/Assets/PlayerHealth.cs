using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public float invincibilityDelay = 2f;
    public float invicibilityFlashDelay = 0.2f;
    public bool isInvincible = false;

    public GameObject deathMenu;

    public SpriteRenderer graphics;
    public HealthBar healthBar;
    // Start is called before the first frame update
    public static PlayerHealth instance;
    [SerializeField] private AudioClip SoundBlessure = null;
    private AudioSource perso_AudioSource;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'un instance de player health dans la scène");
            return;
        }
        perso_AudioSource = GetComponent<AudioSource>();
        instance = this;
    }


    void Start()
    {
        currentHealth = maxHealth;

        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(10);
        }
    }


    public void TakeDamage(int damage)
    {
        if (!isInvincible)
        {
            perso_AudioSource.PlayOneShot(SoundBlessure);
            currentHealth -= damage;
            healthBar.SetHealth(currentHealth);
            isInvincible = true;
            StartCoroutine(InvincibilityFlash());
            StartCoroutine(HandleInvicibilityDelay());
        }
        if(currentHealth < 0)
        {
            Die();
        }
    }

    public void Die()
    {
        this.GetComponent<PlayerMovements>().enabled = false;
        this.GetComponent<Collider2D>().enabled = false;
        this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        this.GetComponent<Animator>().SetTrigger("Die");
        deathMenu.SetActive(true);
        deathMenu.GetComponent<Animator>().SetTrigger("Fade");
        
    }

    public void HealPlayer(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        healthBar.SetHealth(currentHealth);
    }

    public IEnumerator InvincibilityFlash()
    {
        while (isInvincible)
        {
            graphics.color = new Color(1f, 1f, 1f, 0f);
            yield return new WaitForSeconds(invicibilityFlashDelay);
            graphics.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(invicibilityFlashDelay);
        }
    }

    public IEnumerator HandleInvicibilityDelay()
    {
        yield return new WaitForSeconds(invincibilityDelay);
        isInvincible = false;
    }
}
