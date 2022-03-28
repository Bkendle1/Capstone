using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private float timeBtwAttack;
    [SerializeField] private float startTimeBtwAttack;
    [SerializeField] private Transform attackPos;
    
    private int attackDamage;
    [SerializeField] private LayerMask whoIsEnemy;
    [SerializeField] private float attackRangeX, attackRangeY;
    [SerializeField] private int attack1, attack2, attack3;
    private Animator anim;
    private int attackNum = 0; //attack index
    private PlayerController pc;
    private float originalSpeed;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        pc = GetComponent<PlayerController>();
        originalSpeed = pc.moveSpeed;
    }

    private void Update()
    {
        
        if(timeBtwAttack <= 0)
        {
            //then you can attack
            if (Input.GetButtonDown("Fire1"))
            {
                pc.moveSpeed = originalSpeed;

                attackNum++;
                if (attackNum == 1)
                {
                    attackDamage = attack1;
                } else if (attackNum == 2)
                {
                    attackDamage = attack2;
                } else if (attackNum == 3)
                {
                    attackDamage = attack3;
                } else if (attackNum > 3)
                {
                    attackNum = 1;
                }
                    anim.Play("player_attack" + attackNum);

                //deal damage to enemies within circle
                Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(attackPos.position, new Vector2(attackRangeX, attackRangeY),0, whoIsEnemy);
                foreach (Collider2D enemy in enemiesToDamage)
                {
                    enemy.GetComponent<EnemyHealth>().DamageEnemy(attackDamage);
                }
                timeBtwAttack = startTimeBtwAttack;
                
            } else
            { 
                pc.moveSpeed = originalSpeed;
            }
        } else
        {
            timeBtwAttack -= Time.deltaTime;
        }
    }
   
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackPos.position, new Vector2(attackRangeX, attackRangeY));
    }
}
