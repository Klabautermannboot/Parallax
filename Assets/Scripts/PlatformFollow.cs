using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformFollow : MonoBehaviour
{
    public Transform player;
    

public void OnTriggerEnter2D(Collider2D other)
{
    if(other.gameObject.CompareTag("Platform"))
    {
        player.transform.parent = other.gameObject.transform;
    }
}

public void OnTriggerExit2D(Collider2D other) 
{
    if(other.gameObject.CompareTag("Platform"))
    {
        player.transform.parent = null;

    }  
}

    
}
