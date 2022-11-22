using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLife2 : MonoBehaviour
{   
   
    public Animator animator;
    private Rigidbody2D player;
    public int maxHealth = 100;
    public HealthbarScript healthbar;
    int currentHealth;
    private bool canDie = true;
    private string currentState;
    private float hitTime;
    private bool isHit;

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
        if(canDie == true)
        {
        if(currentHealth > 0 )
        {
        currentHealth -= damage;
        healthbar.SetHealth(currentHealth);
        isHit = true;
            
            if(isHit==true) 
            {
            GetComponent<PlayerMovement2>().ChangeAnimationState("Player_hit");
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
    GetComponent<PlayerMovement2>().LastAnimation();
    }

    void Die()
    {   
        canDie = false;
        GetComponent<PlayerMovement2>().ChangeAnimationState("Player_death");
        player.velocity = Vector3.zero;
        GetComponent<PlayerCombat2>().enabled = false;
        FindObjectOfType<AudioManager>().PlayDelayed("Dead",0.5f);
        GetComponent<BushHitPlayer2>().enabled =false;
        GetComponent<Shoot1>().enabled = false;
      // GetComponent<BoxCollider2D>().enabled = false;
       // GetComponent<PlayerMovement2>().enabled = false;   
        GetComponent<PlayerMovement2>().enabled = false;
        GameObject.FindObjectOfType<Score>().redHasKilled();
        this.enabled= false;
        

    }
    

}
