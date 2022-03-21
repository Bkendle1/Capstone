using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange = .5f;
    [SerializeField] private LayerMask whoIsEnemy;
    [SerializeField] private int attackDmg = 50; 
    private int index = 0;
 
    
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            index++;
            if(index > 3)
            {
                index = 1;
            }
            Invoke("Attack", .3f);

        }
    }

    void Attack()
    {
        anim.Play("player_attack" + index);
        //Detect enemy colliders and store them
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, whoIsEnemy);

        //Iterate through each enemy
        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyHealth>().TakeDamage(attackDmg);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);    
    }
}
