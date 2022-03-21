using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private PlayerController player;
    [SerializeField] private BoxCollider2D boundsBox;
    private float halfHeight, halfWidth;

    [SerializeField] private Transform backBG, frontBG;

    private Vector2 lastPos;
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        halfHeight = Camera.main.orthographicSize;
        halfWidth = halfHeight * Camera.main.aspect; // 16:9 aspect ratio, halfWidth = halfHeight/9 * 16
        lastPos = transform.position;
    }

    void Update()
    {
        if (player != null) //check if player isn't active in scene, like if they're killed
        {
            
            transform.position = new Vector3(
                Mathf.Clamp(player.transform.position.x, boundsBox.bounds.min.x + halfWidth, boundsBox.bounds.max.x - halfWidth),
                 Mathf.Clamp(player.transform.position.y, boundsBox.bounds.min.y + halfHeight, boundsBox.bounds.max.y - halfHeight),
                transform.position.z);

            Vector2 amountToMove = new Vector2(transform.position.x - lastPos.x, transform.position.y - lastPos.y);

            backBG.position += new Vector3(amountToMove.x, amountToMove.y, 0f);
            frontBG.position += new Vector3(amountToMove.x, amountToMove.y, 0f) * .5f;
            lastPos = transform.position;


        }
    }
}
