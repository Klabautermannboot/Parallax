
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorMoveVertical : MonoBehaviour
{
    private Rigidbody2D position;

    private bool bounds;
    public float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        bounds = false;
        position = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if(transform.position.y > 5f)
        {
            bounds = true;
        }
        
        if(transform.position.y < 1.5f)
        {
            bounds=false;
            
        }
        if(bounds==true)
        {
            position.velocity = Vector2.down * moveSpeed;

        }
        if(bounds==false)
        {
            position.velocity = Vector2.up * moveSpeed;
        }
    }
}
