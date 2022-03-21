using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Stun")]
    [SerializeField] public float startDazeTime;
    [SerializeField] private float flashTime = 1f;
    private float dazeTime;
    private float originalSpeed;

    [Header("Health")]
    [SerializeField] public int maxHP = 100;
    [SerializeField] private GameObject effect;
    [SerializeField] private HealthBar healthBar;
    private int currentHP;


    private SpriteRenderer sr;
    private Color originalColor;
    private Animator anim;
    private EnemyPatrol enemy;

    private void Awake()
    {
        enemy = GetComponent<EnemyPatrol>();
        originalSpeed = enemy.moveSpeed;
        anim = GetComponent<Animator>();
        currentHP = maxHP;
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
        healthBar.SetMaxHealth(maxHP);
    }

    private void Update()
    {
        if (dazeTime <= 0)
        {
            //sets movespeed back to original
            enemy.moveSpeed = originalSpeed;
        }
        else
        {
            enemy.moveSpeed = 0;
            dazeTime -= Time.deltaTime;
        }
    }
    
    private void Flash()
    {
        sr.color = Color.red;
        Invoke("ResetColor", flashTime);
    }
    private void ResetColor()
    {
        sr.color = originalColor;
    }

    public void DamageEnemy(int damage)
    {
        currentHP = Mathf.Clamp(currentHP -= damage, 0, maxHP);
        healthBar.SetHealth(currentHP);
        dazeTime = startDazeTime;
        
        if (currentHP <= 0)
        {
           Instantiate(effect, transform.position, transform.rotation);
           Destroy(gameObject);
        }
        else
        {
            Flash();
            anim.SetTrigger("hurt");
        }
    }
}
