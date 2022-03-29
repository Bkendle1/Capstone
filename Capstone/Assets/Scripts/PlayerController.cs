using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("Movement")]
    [SerializeField] public float moveSpeed = 10f;
    [SerializeField] private float jForce = 15f;
    private bool canDoubleJump;

    [Header("Dash")]
    [SerializeField] private float waitAfterDashing;
    private float dashRechargeCounter;
    [SerializeField] private float dashSpeed, dashTime;
    private float dashCounter;
    [SerializeField] private SpriteRenderer spriteRender, afterImage;
    [SerializeField] private float afterImgLifetime, timeBetweenAfterImgs;
    private float afterImgCounter;
    [SerializeField] private Color afterImgColor;

    [Header("Coyote Time")]
    [SerializeField] private float hangTime;
    private float hangCounter;

    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private LayerMask whatIsWall;

    private Rigidbody2D rb;
    private CapsuleCollider2D capsuleCollider;
    private Animator anim;

    [Header("Gliding")]
    [SerializeField] private float glidingSpeed;
    private bool isGliding;

    [Header("Wall Jumping/Sliding")]
    [SerializeField] private Transform wallCheckCollider;
    [SerializeField] private float wallCheckRadius = 0.2f;
    [SerializeField] private float slideFactor = 0.2f;
    private bool isWallSliding = false;

    private CapsuleCollider2D capsule;
    private PlayerAbilityTracker abilities;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
        spriteRender = GetComponent<SpriteRenderer>();
        abilities = GetComponent<PlayerAbilityTracker>();
        capsule = GetComponent<CapsuleCollider2D>();    
    }

    void Update()
    {
        //Dash 
        if (dashRechargeCounter > 0)
        {
            dashRechargeCounter -= Time.deltaTime;

        }
        else
        {
            if (Input.GetButtonDown("Fire3") && abilities.canDash)
            {
                dashCounter = dashTime;
                showAfterImage();
            }
        }

        if (dashCounter > 0)
        {
            //length of dash
            dashCounter -= Time.deltaTime;

            rb.velocity = new Vector2(dashSpeed * transform.localScale.x, rb.velocity.y);

            afterImgCounter -= Time.deltaTime;
            if (afterImgCounter <= 0)
            {
                showAfterImage();
            }

            dashRechargeCounter = waitAfterDashing;
        }
        else
        {
            //PLAYER CANT DOUBLE JUMP AFTER WALL JUMP
            //Movement
            if (!isWallSliding)
            {
                rb.velocity = new Vector2(moveSpeed * Input.GetAxis("Horizontal"), rb.velocity.y);

                //Flip Character
                if (rb.velocity.x < 0)
                {
                    transform.localScale = new Vector3(-1f, 1f, 1f);
                }
                else if (rb.velocity.x > 0)
                {
                    transform.localScale = Vector3.one;
                }
            }
        }

        //Coyote Time
        if (isGrounded())
        {
            hangCounter = hangTime;
        }
        else
        {
            hangCounter -= Time.deltaTime;
        }

        //Jump
        if (Input.GetButtonDown("Jump"))
        {

            if (isGrounded())
            {
                rb.velocity = new Vector2(rb.velocity.x, jForce);
                canDoubleJump = true;
            }
            else
            {
                if (canDoubleJump && abilities.canDoubleJump)
                {
                    rb.velocity = new Vector2(rb.velocity.x, jForce);
                    //after one jump canDoubleJump is set to false 
                    canDoubleJump = false;
                }
                //Hang time
                if (hangCounter > 0f && abilities.canDoubleJump)
                {
                    rb.velocity = new Vector2(rb.velocity.x, jForce);
                }
            }
        }

        //Variable Jump
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * .5f);
        }

        //Wall Jump/Slide
        wallCheck();

        //Gliding
        isGliding = false;
        if (Input.GetButton("Jump") && rb.velocity.y < 0 && abilities.canGlide)
        {
            isGliding = true;
            rb.velocity = new Vector2(rb.velocity.x, -glidingSpeed);
        }

        anim.SetBool("grounded", isGrounded());
        anim.SetBool("wallSliding", isWallSliding);
        anim.SetBool("gliding", isGliding);
        anim.SetFloat("moveSpeed", Mathf.Abs(rb.velocity.x));

    }//end of Update()

    //check if player is grounded
    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.CapsuleCast(capsuleCollider.bounds.center, capsuleCollider.bounds.size, CapsuleDirection2D.Vertical, 0, Vector2.down, .1f, whatIsGround);
        return raycastHit.collider != null;
    }

    void wallCheck()
    {
        float horizontalMove = Input.GetAxisRaw("Horizontal");
        if (Physics2D.OverlapCircle(wallCheckCollider.position, wallCheckRadius, whatIsWall)
            && horizontalMove != 0
            && rb.velocity.y < 0
            && !isGrounded())
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, -slideFactor);
            if (Input.GetButtonDown("Jump") && abilities.canDoubleJump)
            {
                rb.velocity = new Vector2(rb.velocity.x, jForce);
            }
        }
        else
        {
            isWallSliding = false;
        }
    }
    public void showAfterImage()
    {
        //create a copy of the afterimage prefab
        SpriteRenderer img = Instantiate(afterImage, transform.position, transform.rotation);

        img.sprite = spriteRender.sprite;
        img.transform.localScale = transform.localScale;
        img.color = afterImgColor;

        //Remove afterimage after it's lifetime
        Destroy(img.gameObject, afterImgLifetime);

        afterImgCounter = timeBetweenAfterImgs;
    }
    private void OnLevelWasLoaded(int level)
    {
        FindStartPos();
    }
    public void FindStartPos()
    {
        transform.position = GameObject.FindWithTag("StartPos").transform.position;
    }
}

