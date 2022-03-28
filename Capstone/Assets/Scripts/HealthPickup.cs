using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] private int healthAmt;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private GameObject pickupEffect;

    private void Update()
    {
        transform.Rotate(new Vector3(transform.rotation.x, rotateSpeed, transform.rotation.z));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            
            if(PlayerHealth.instance.currentHP < PlayerHealth.instance.maxHP)
            {
                if(pickupEffect != null)
                {
                    Instantiate(pickupEffect, transform.position, Quaternion.identity);
                }
                PlayerHealth.instance.AddHealth(healthAmt);
                Destroy(gameObject);
            }
        }
    }
}
