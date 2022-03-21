using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHP = 100;
    private int currentHP;
    private Animator anim;
    private Rigidbody2D rb;

    void Start()
    {
        currentHP = maxHP;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(int dmgAmt)
    {
        currentHP -= dmgAmt;

        anim.SetTrigger("hurt");

        if (currentHP <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("They dead.");
        anim.SetBool("isDead", true);

        //Disable enemy
        rb.gravityScale = 0;
        
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;


    }
}
