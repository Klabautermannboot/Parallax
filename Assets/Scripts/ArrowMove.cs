using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMove : MonoBehaviour
{
    public Transform player;
    public Camera camera;
    private UnityEngine.Vector2 screenBounds;

    void Start()
    {
         float halfHight = camera.orthographicSize;

        float halfWidth = camera.aspect * halfHight;

        screenBounds = Camera.main.ScreenToWorldPoint(new UnityEngine.Vector3(Screen.width,Screen.height,Camera.main.transform.position.z)); 
        
        transform.position = new UnityEngine.Vector3(-(1.5f*screenBounds.x),player.transform.position.y,transform.position.z);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x,player.transform.position.y, transform.position.z);
    }
}
