using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth instance;

    [Header("Health")]
    [SerializeField] public int maxHP;
    [SerializeField] private GameObject effect;
    [SerializeField] private HealthBar healthBar;

    [Header("Invincibility")]
    [SerializeField] private float invincibilityLength;
    [SerializeField] private float flashLength;
    private float invincibilityCounter;
    private float flashCounter;

    public int currentHP;
    private Animator anim;
    private SpriteRenderer sr;
  
    private void Awake()
    {
        anim = GetComponent<Animator>();
        //if there's no PlayerHealth that's already assigned to be the instance assign it and never destroy it
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        sr = GetComponent<SpriteRenderer>();
        currentHP = maxHP;
        healthBar.SetMaxHealth(maxHP);
    }


    private void Update()
    {

        if(invincibilityCounter > 0)
        {
            invincibilityCounter -= Time.deltaTime;

            //for the duration of invincibility have the sprite blink
            flashCounter -= Time.deltaTime;
            if(flashCounter <= 0)
            {
                sr.enabled = !sr.enabled;
                flashCounter = flashLength;
            }
            
            if(invincibilityCounter <= 0)
            {
                sr.enabled = true;
            }

        }
    }
    public void TakeDamage(int damage)
    {
        if (invincibilityCounter <= 0)
        {
            currentHP = Mathf.Clamp(currentHP -= damage, 0, maxHP);
            healthBar.SetHealth(currentHP);

            if (currentHP > 0)
            {
                //player hurt
                anim.SetTrigger("hurt");
                //invincibility
                invincibilityCounter = invincibilityLength;
            }
            else
            {
                Instantiate(effect, transform.position, transform.rotation);
                RespawnController.instance.Respawn();
            }
        }
    }
    public void AddHealth(int value)
    {
        currentHP = Mathf.Clamp(currentHP += value, 0, maxHP);
        healthBar.SetHealth(currentHP);
    }

    public void RefillHealth()
    {
        currentHP = maxHP;
        healthBar.SetHealth(currentHP);
    }
}
