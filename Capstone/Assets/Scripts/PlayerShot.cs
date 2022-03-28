using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShot : MonoBehaviour
{
    [SerializeField] private BulletController shotToFire;
    [SerializeField] private Transform shotPoint;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;
    private PlayerAbilityTracker abilities;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        abilities = GetComponent<PlayerAbilityTracker>();
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(1) && abilities.canShoot)
        {
            anim.SetTrigger("shoot");
            Instantiate(shotToFire, shotPoint.position, shotPoint.rotation).moveDir = new Vector2(transform.localScale.x, 0);
        }

        if (transform.localScale.x == 1)
        {
            shotToFire.transform.localScale = Vector3.one;
        }
        else if (transform.localScale.x == -1)
        {
            shotToFire.transform.localScale = new Vector3(-1f, 1f, 1f);
        } 
    }
}
