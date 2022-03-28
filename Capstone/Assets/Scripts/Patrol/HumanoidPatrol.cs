using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidPatrol : MonoBehaviour
{

    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private float waitAtPoint, jForce;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask whatIsWall;
    [SerializeField] public float moveSpeed;

    private int currentPoint;
    private float waitCounter;
    private Rigidbody2D rb;
    private bool atWall;
    private Animator anim;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        waitCounter = waitAtPoint;
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        //prevents points to follow parent
        foreach (Transform pPoint in patrolPoints)
        {
            pPoint.SetParent(null);
        }  
    }

    void Update()
    {


        //if we're at wall
        atWall = Physics2D.OverlapCircle(wallCheck.position, .3f, whatIsWall);
        if (atWall) {
            rb.velocity = new Vector2(rb.velocity.x, jForce);
        }

        //check if we're at point
        if (Mathf.Abs(transform.position.x - patrolPoints[currentPoint].position.x) > .2f)
        {
            //left side of point
            if (transform.position.x < patrolPoints[currentPoint].position.x)
            {
                rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
                //transform.localScale = new Vector3(1.8f, 1.8f, 1.8f);
                transform.localScale = Vector3.one;

            }
            else //right side of point
            {
                rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
                //transform.localScale = new Vector3(-1.8f, 1.8f, 1.8f);
                transform.localScale = new Vector3(-1f, 1f, 1f);

            }
        }
        else //when we're at point
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);

            waitCounter -= Time.deltaTime;
            //move to next point
            if (waitCounter <= 0)
            {
                waitCounter = waitAtPoint;
                currentPoint++;

                //keep points in bounds
                if(currentPoint >= patrolPoints.Length)
                {
                    currentPoint = 0;
                }
            }

        }
        anim.SetFloat("moveSpeed", Mathf.Abs(rb.velocity.x));
    }


    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(wallCheck.position, .3f);
    }
}
