using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float shotSpeed;
    [SerializeField] public Vector2 moveDir;
    [SerializeField] private GameObject impactFX;
    [SerializeField] private int shotDamage;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        rb.velocity = moveDir * shotSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Instantiate(impactFX, transform.position, Quaternion.identity);
        if(other.tag == "Enemy")
        {
            other.GetComponent<EnemyHealth>().DamageEnemy(shotDamage);
        }
        Destroy(gameObject);    
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
