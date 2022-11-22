using System.Numerics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMoveRight : MonoBehaviour
{
    public Transform player;
    public Camera camera;
    private UnityEngine.Vector2 screenBounds;
    

    void Start()
    {
         float halfHight = camera.orthographicSize;

        float halfWidth = camera.aspect * halfHight;

        screenBounds = Camera.main.ScreenToWorldPoint(new UnityEngine.Vector3(Screen.width,Screen.height,Camera.main.transform.position.z)); 
        
        transform.position = new UnityEngine.Vector3((screenBounds.x/2),player.transform.position.y,transform.position.z);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new UnityEngine.Vector3(transform.position.x,player.transform.position.y, transform.position.z);
    }
}
