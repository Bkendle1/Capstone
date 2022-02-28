using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject followObject;
    public Vector2 followOffset;
    private Vector2 threshold;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
    //calculate threshold
    private Vector3 calculateThreshold()
    {   
        Rect aspect = Camera.main.pixelRect; //find aspect ratio of camera
        Vector2 t = new Vector2(Camera.main.orthographicSize * aspect.width / aspect.height, Camera.main.orthographicSize);
        t.x -= followOffset.x;
        t.y -= followOffset.y;
        return t;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
    }
}
