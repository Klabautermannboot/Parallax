using System.Runtime.CompilerServices;
using System.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;


public class PlayerMovement : MonoBehaviour
{   


    [SerializeField]
    int PlayerID;
    ControlsManager controlsManager;


    // Private
   

    private Rigidbody2D player;
    private bool jumpKey;
    private int doubleJump;   
    private bool facingRight =true; 
    private float moveInputSideways;
    private string currentState;
    private string lastState;


    //Boost 


    private static float boostTimeStart = 0.4f;
    private float boostTime;
    private bool speedBoost;
    private bool boostUsed;
    private float boostCooldown;
    private bool boostCooldowns;
    private float boostAnimationTime;
    private bool boostAnimationDone;


    //Public

    public Cooldown cooldown;
    public float boostForce; 
    public float speed;
    public float jumpForce1;
    public float jumpForce2;
    

    //Animators


    public Animator Camera;
    public Animator animator;  


    [SerializeField] private Transform groundCheck = null;
    [SerializeField] private LayerMask backgroundMask;


    //Animator Constants
    const string PLAYER_IDLE = "Player_idle";
    const string PLAYER_RUN = "Player_run";
    const string PLAYER_JUMP = "Player_jump";
    const string PLAYER_DOUBLEJUMP = "Player_doubleJump";
    const string PLAYER_BOOST = "Player_boost";
    const string PLAYER_HIT = "Player_hit";
    const string PLAYER_DEATH = "Player_death";
    const string PLAYER_ATTACK = "Player_attack";





    void Start()
    {   
        
        controlsManager = FindObjectOfType<ControlsManager>();
        player = GetComponent<Rigidbody2D>();
        jumpKey = false; 
        boostTime = boostTimeStart;
        boostCooldown = 2f;
        cooldown.SetMaxCooldown(boostCooldown);
        animator = GetComponent<Animator>();
        boostAnimationDone = true;

    }

 


     void Update()
    {
         if(Input.GetKeyDown(controlsManager.GetKey(PlayerID,ControlKeys.JumpKey)))
        {
            doubleJump++;
        }



    }

    
    //Animation


     public void ChangeAnimationState(string newState) 
        {
            if(currentState == newState) return;
            
           
            lastState = animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;

            if(newState == "Player_death")
            { 
            
                lastState="Player_death";
            
            }

            animator.Play(newState);
            currentState = newState;

        }


    public void LastAnimation()
        {     
            if(lastState != "Player_hit")
            {
            animator.Play(lastState);
            }
            
        }

    void Flip()
    {
        facingRight  = !facingRight;
        transform.Rotate(0f,180f,0f);
    }

   void boostDone()
    {
        boostAnimationDone=true;
    }


//Platform parent




    void FixedUpdate()
    {

        // Movement


    moveInputSideways = Input.GetAxisRaw("Horizontal");

    player.velocity = new Vector2(moveInputSideways * speed * Time.deltaTime , player.velocity.y);



if(animator.GetCurrentAnimatorClipInfo(0)[0].clip.name != "Player_death")
    {
        if(animator.GetCurrentAnimatorClipInfo(0)[0].clip.name != "Player_hit")
        {
            if(boostAnimationDone == true && GetComponent<PlayerCombat>().attacking == false)
            {   
                if(moveInputSideways != 0 && doubleJump == 0)
                {
                ChangeAnimationState(PLAYER_RUN);
                }
                else if(doubleJump == 0)
                {
                ChangeAnimationState(PLAYER_IDLE);
                }
                else if(doubleJump != 0)
                {
                if(doubleJump == 1)
                {
                ChangeAnimationState(PLAYER_JUMP);

                }
                else if(doubleJump == 3)
                {
                ChangeAnimationState(PLAYER_DOUBLEJUMP);
                }
                }
            }
        }    
    }
    else 
    {
        ChangeAnimationState("Player_death");
    }

    
        // FLipping


    if(facingRight == false && moveInputSideways > 0)
        {
            Flip();
        }

    else if(facingRight == true && moveInputSideways < 0)

        {
              Flip();  
        }


        //Boost


    if(boostCooldown >= 2f)

        {      
            
            boostUsed = false;
            boostCooldown = 0f;
           // boostTimeStart = 0.4f;
            //boostTime=boostTimeStart;
            boostCooldowns = false;
        }

    if(boostCooldowns == true)
        {   
            
            boostCooldown += Time.deltaTime;
            
            
        }
        
    cooldown.SetCooldown(boostCooldown);

    if(boostTime <= 0)
            {   
                boostCooldowns = true;
                speedBoost = false;
                //boostTime = boostTimeStart; 
            }


    if(boostUsed == false)
        {   
    
        if(Input.GetKeyDown(controlsManager.GetKey(PlayerID,ControlKeys.BoostKey)) && moveInputSideways != 0)
            {   
                Camera.SetTrigger("Shake");
                boostUsed = true;
                FindObjectOfType<AudioManager>().Play("Boost");
                boostAnimationDone = false;
                speedBoost = true;
                GetComponent<BushHit>().enabled = false;
                GetComponent<BoxCollider2D>().enabled = false;
                player.constraints =  RigidbodyConstraints2D.FreezePositionY;
            }
        }
        
        if(speedBoost == true)
            {
                    boostTime -= Time.deltaTime;
                    player.velocity = new Vector2(moveInputSideways * speed * Time.deltaTime * boostForce, player.velocity.y); 
                    ChangeAnimationState(PLAYER_BOOST);
                    boostAnimationTime = animator.GetCurrentAnimatorStateInfo(0).length;
                    Invoke("boostDone",boostAnimationTime);
                    
            }
            else
            {
                boostTime = boostTimeStart; 
                player.velocity = new Vector2(moveInputSideways * speed * Time.deltaTime , player.velocity.y);     
                GetComponent<BushHit>().enabled = true;
                GetComponent<BoxCollider2D>().enabled = true;
                player.constraints =  RigidbodyConstraints2D.None;
                player.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
        

       
       
    // Jumping & GroundCheck


    if(doubleJump == 3)   
        {
            player.velocity = Vector2.up * jumpForce2; 
            doubleJump++; 
            FindObjectOfType<AudioManager>().Play("Jump");
            ChangeAnimationState(PLAYER_DOUBLEJUMP);
            animator.SetInteger("IsJumping",3);
        }


    if(Physics2D.OverlapCircle(groundCheck.position, 0.01f, backgroundMask) == true && jumpKey == true)
     {  
        doubleJump = 0;
        jumpKey = false;
        

        return;

    }


    if(doubleJump == 1)
        {
            player.velocity = Vector2.up * jumpForce1;
            doubleJump++;
            FindObjectOfType<AudioManager>().Play("Jump");
            jumpKey = true;
            ChangeAnimationState(PLAYER_JUMP);
            animator.SetInteger("IsJumping",1);
        }
   
        

     }

}
