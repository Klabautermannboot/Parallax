using System.Numerics;
using System.IO;
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushHit : MonoBehaviour
{
    // Start is called before the first frame update
public Animator cam;

public Animator[] prefabAnimator;
public float hitTime;
private float starttime;
private int damageTaken;


 void Start()
 {
    starttime = hitTime;
 }
public void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Bush"))
        {
            prefabAnimator[1] = other.gameObject.GetComponent<Animator>();
            prefabAnimator[1].SetBool("IsHit",true);
            FindObjectOfType<AudioManager>().Play("PlantHit");
            cam.SetTrigger("Shake");
            GetComponent<PlayerLife>().TakeDamage(10);
            InvokeRepeating("Damage",1.5f,1.5f);

        }    

    }

void Damage()
{
            GetComponent<PlayerLife>().TakeDamage(5);
            cam.SetTrigger("Shake");
            damageTaken ++;
            if(damageTaken == 1)
            {
                FindObjectOfType<AudioManager>().Play("AngryShroom");
            }

}
    public void OnCollisionExit2D(Collision2D other) 
    {   
        if(other.gameObject.CompareTag("Bush"))
        {
            damageTaken = 0;
            prefabAnimator[1].SetBool("IsHit",false);
            //playtime = 0f;
            CancelInvoke("Damage");
        }
    }



}

