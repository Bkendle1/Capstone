using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private int damageAmount;
    private int attackNum = 1; //attack index
    private Animator anim;
    private bool inAttackRange;
    private bool isNear;
    private float timeBtwAttack;
    private float currentDistance;

    [SerializeField] private float distance = 2f;
    [SerializeField] private float startTimeBtwAttack;
    [SerializeField] private Transform attackPos;
    [SerializeField] private LayerMask whoIsPlayer;
    [SerializeField] private float attackRangeX, attackRangeY;
    [SerializeField] private int attack1, attack2, attack3;
    [SerializeField] private GameObject player;
    private EnemyPatrol patrol;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        patrol = GetComponent<EnemyPatrol>();
        timeBtwAttack = startTimeBtwAttack; 
    }

    private void Update()
    {
        //is player in attack range
        inAttackRange = Physics2D.OverlapBox(attackPos.position, new Vector2(attackRangeX, attackRangeY), 0, whoIsPlayer);

        currentDistance = Mathf.Abs(transform.position.x - player.transform.position.x);

        //check if distance from player is near
        if(currentDistance > distance)
        {
            isNear = false;
        } else
        {
            isNear = true;
        }

        if (timeBtwAttack <= 0)
        {

            if (inAttackRange && isNear)
            {
                patrol.enabled = false;
                //DealDamage();
                anim.Play("attack" + attackNum);
                timeBtwAttack = startTimeBtwAttack;
            }
            else
            {
                patrol.enabled = true;
            }
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }
        
    }

    private void DealDamage()
    {
        
        PlayerHealth.instance.TakeDamage(damageAmount);
        attackNum++;
        if (attackNum == 1)
        {
            damageAmount = attack1;
        }
        else if (attackNum == 2)
        {
            damageAmount = attack2;
        }
        else if (attackNum == 3)
        {
            damageAmount = attack3;
        }
        else if (attackNum > 3)
        {
            attackNum = 1;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(attackPos.position, new Vector2(attackRangeX, attackRangeY));
    }
}
