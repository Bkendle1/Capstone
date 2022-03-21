using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHP = 100;
    private int currentHP;
    private Animator anim;

    void Start()
    {
        currentHP = maxHP; 
    }

    public void TakeDamage(int dmgAmt)
    {
        currentHP -= dmgAmt;

        if (currentHP <= 0)
        {
            Die();
        } 
    }

    void Die()
    {
        Debug.Log("They dead.");
    }
}
