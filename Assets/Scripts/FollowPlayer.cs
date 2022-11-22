using System.Linq.Expressions;
using System;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FollowPlayer : MonoBehaviour
{   

    public Camera camera;
    public List<Transform> targets;
    public Vector3 offset;
    public float smoothTime= 0.5f;
    private Vector3 velocity;
    public float arrowPosition;
    private float minusArrowPosition;
    public float outOfScreen;
    public GameObject redLeftArrow;
    public Transform redLArrow;
    public Transform redRArrow;
    public Transform blueRArrow;
    public Transform blueLArrow;
    public GameObject redRightArrow;

    public GameObject blueLeftArrow;
    public GameObject blueRightArrow;

    private delegate bool Predicate<in targets>();
    public Transform red;
    public Transform blue;
    public Display display;
        
    // Update is called once per frame

  
    void LateUpdate()
    {
        
        if(targets.Count == 0)
        {
            return;
        }
        // transform.position = (new Vector3(player.transform.position.x,transform.position.y,transform.position.z));
        
        Vector3 centerPoint = getCenterPoint();

        Vector3 newPosition = centerPoint + offset;

        transform.position = Vector3.SmoothDamp(transform.position,new Vector3(newPosition.x, transform.position.y,transform.position.z),ref velocity, smoothTime) ;


        //Arrow Position
        
        float halfHight = camera.orthographicSize;

        float halfWidth = camera.aspect * halfHight;

        arrowPosition   = newPosition.x + halfWidth - 4f;
        
        minusArrowPosition = newPosition.x - halfWidth + 1f; 


    //Instantiate Arrows red
    
        if(centerPoint.x - halfWidth > red.transform.position.x)
        {
            redLeftArrow.GetComponent<SpriteRenderer>().enabled = true;
        }

        else
      
        {
            redLeftArrow.GetComponent<SpriteRenderer>().enabled = false;
        }
    
         if(centerPoint.x + halfWidth < red.transform.position.x)
        {
            redRightArrow.GetComponent<SpriteRenderer>().enabled = true;
        }

        else
        {
            redRightArrow.GetComponent<SpriteRenderer>().enabled = false;
        }

//Instantiate Arrows blue

     if(centerPoint.x - halfWidth > blue.transform.position.x)
        {
            blueLeftArrow.GetComponent<SpriteRenderer>().enabled = true;
        }

        else
        {
            blueLeftArrow.GetComponent<SpriteRenderer>().enabled = false;
        }
    
         if(centerPoint.x + halfWidth < blue.transform.position.x)
        {
            blueRightArrow.GetComponent<SpriteRenderer>().enabled = true;
           
        }

        else
        {
            blueRightArrow.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    
    Vector3 getCenterPoint()
    {
            if (targets.Count == 1)
            {
                return targets[0].position;

            }

            var bounds = new Bounds(targets[0].position, Vector3.zero) ;
            
            for (int i = 0; i < targets.Count; i ++)
            {
                bounds.Encapsulate(targets[i].position);

            } 
            return bounds.center;
    }
}
