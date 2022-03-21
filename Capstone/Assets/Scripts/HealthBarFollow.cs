using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarFollow : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private float height = .5f;

    
    void Update()
    {
        if(enemy != null)
        {
            transform.position = new Vector2(enemy.transform.position.x, enemy.transform.position.y + height);
        } else
        {
            gameObject.SetActive(false);
        }
    }
}
