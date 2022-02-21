using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10;
    public float jForce = 15;
    private bool canDoubleJump;
    
    public float dashSpeed, dashTime;
    private float dashCounter;
    
    public SpriteRenderer spriteRender, afterImage;
    public float afterImgLifetime, timeBetweenAfterImgs;
    private float afterImgCounter;
    public Color afterImgColor;
    
    public float waitAfterDashing;
    private float dashRechargeCounter;

   
   //check what layer is the ground layer
    public LayerMask whatIsGround;
    
    private Rigidbody2D rb;
    private CapsuleCollider2D capsuleCollider;
    private Animator anim;
   
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
        spriteRender = GetComponent<SpriteRenderer>();
    }

    void Update()
    {   
        //Allow player to dash only if the counter is 0;
        if (dashRechargeCounter > 0)
        {
            dashRechargeCounter -= Time.deltaTime;
        }
        else
        {
            if (Input.GetButtonDown("Fire2"))
            {
                dashCounter = dashTime;
                showAfterImage();
            }
        }
        if (dashCounter > 0)
        {
            //remaining dash time
            dashCounter -= Time.deltaTime;

            rb.velocity = new Vector2(dashSpeed * transform.localScale.x, rb.velocity.y);

            afterImgCounter -= Time.deltaTime;
            if(afterImgCounter <= 0)
            {
                showAfterImage();
            }

            dashRechargeCounter = waitAfterDashing;
        }
        else
        {


            //Movement
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
        //Jump
        if (Input.GetButtonDown("Jump")) {
            if (isGrounded())
            {
                rb.velocity = new Vector2(rb.velocity.x, jForce);
                canDoubleJump = true;

            }
            else
            {
                if (canDoubleJump)
                {
                    rb.velocity = new Vector2(rb.velocity.x, jForce);
                    //after one jump canDoubleJump is set to false 
                    canDoubleJump = false;
                }
            }
        }


       
        anim.SetBool("grounded", isGrounded());
        anim.SetFloat("moveSpeed", Mathf.Abs(rb.velocity.x));

    }
    //check if player is grounded
    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.CapsuleCast(capsuleCollider.bounds.center, capsuleCollider.bounds.size, CapsuleDirection2D.Vertical, 0, Vector2.down, .1f, whatIsGround);
        return raycastHit.collider != null;
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
}
