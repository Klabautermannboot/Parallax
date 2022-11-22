using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{   
   
    public Animator animator;
    public int maxHealth = 100;
    public HealthbarScript healthbar;                                                     
    int currentHealth;
    private Rigidbody2D player;
    private bool canDie = true;
    private string currentState;
    private bool isHit;
    private float hitTime;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth);
        FindObjectOfType<PlayerMovement2>();
        FindObjectOfType<PlayerMovement>();
        FindObjectOfType<PlayerCombat>();
        player = GetComponent<Rigidbody2D>();
    }

void ChangeAnimationState(string newState) 
        {
            if(currentState == newState) return;

            animator.Play(newState);
            currentState = newState;

        }


    public void TakeDamage(int damage)
    {
       if(canDie== true) 
       {
        if(currentHealth > 0 )
        {
        currentHealth -= damage;
        healthbar.SetHealth(currentHealth);
        isHit = true;
             
            if(isHit==true) 
            {
            GetComponent<PlayerMovement>().ChangeAnimationState("Player_hit");
            FindObjectOfType<AudioManager>().PlayDelayed("IsHit", 0.2f);
            hitTime = animator.GetCurrentAnimatorStateInfo(0).length;
            Invoke("hitOver",(hitTime/4));
            }
        }
        if(currentHealth <= 0)
        {

            Die();
            
        }
       }
    }

    void hitOver()
    {
    isHit=false;
    GetComponent<PlayerMovement>().LastAnimation();
    }


    void Die()
    {   
        GetComponent<PlayerMovement>().ChangeAnimationState("Player_death");
        canDie = false;
        player.velocity = Vector3.zero;

        GetComponent<PlayerCombat>().enabled = false;
        GetComponent<BushHit>().enabled =false;
        FindObjectOfType<AudioManager>().PlayDelayed("Dead",0.5f);
      // GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<Shoot>().enabled = false;
        GetComponent<PlayerMovement>().enabled = false;   
       // GetComponent<PlayerMovement>().enabled = false;
       GameObject.FindObjectOfType<Score>().blueHasKilled();
        
        this.enabled= false;

    }
    

}
