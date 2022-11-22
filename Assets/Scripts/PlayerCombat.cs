using System.Numerics;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class PlayerCombat : MonoBehaviour
{

    [SerializeField]
    int PlayerID;
    ControlsManager controlsManager;
    public Animator animator;
    public Rigidbody2D enemy;
    public Transform opponent;
    public float knockbackForce;
    public float knockbackDuration;

    private string[] sounds = {"Punch1", "Punch2", "Punch3", "Punch"};
    public Transform attackPoint;
    public Transform attackPoint2;
    public float attackRange = 0.5f;
    public LayerMask Players;

    public int attackDamage = 40;

    public float attackRate = 2f;
    private float nextAttackTime=0f;
    public bool attacking;
    private float animTime;

    // Start is called before the first frame update

    //Play attack animation
    //Detect Enemies in range
    //Damage Enemies

    void Start()
    {
        controlsManager = FindObjectOfType<ControlsManager>();
        attacking = false;
    }

    // Update is called once per frame
    void Update()
    {
       // if(Input.GetKeyDown(controlsManager.GetKey(PlayerID,ControlKeys.AttackKey)))
          if(Time.time>= nextAttackTime)
       {
         if(Input.GetKeyDown(controlsManager.GetKey(PlayerID,ControlKeys.AttackKey)) && !Input.GetKeyDown(controlsManager.GetKey(PlayerID,ControlKeys.BoostKey)))
        {
            Attack();
            int IndexNumber = UnityEngine.Random.Range(0, sounds.Length);
            FindObjectOfType<AudioManager>().Play(sounds[IndexNumber]);
            attacking = true;
            nextAttackTime = Time.time + 1f / attackRate;
            GetComponent<PlayerMovement>().ChangeAnimationState("Player_attack");
            animTime = animator.GetCurrentAnimatorClipInfo(0).Length;
            Invoke("notAttacking",(animTime/6));
        }
    }
    }

    public void Attack()
    {   
        
        
        Collider2D[] hitPlayers  = Physics2D.OverlapAreaAll(attackPoint2.position,attackPoint.position, Players);
        foreach(Collider2D player in hitPlayers)
        {
            //player.GetComponent<PlayerLife2>().TakeDamage(attackDamage);
            player.GetComponent<PlayerLife2>().TakeDamage(attackDamage);
            StartCoroutine(Knockback(knockbackDuration,knockbackForce,opponent));
        }
        
    
    }


    public IEnumerator Knockback(float knockbackDuration, float knockbackForce, Transform obj)
    {
        float timer = 0;

        while(knockbackDuration > timer)
        {
            timer += Time.deltaTime;
            UnityEngine.Vector2 direction =  (this.transform.position-obj.transform.position).normalized;
            enemy.AddForce(-direction * knockbackForce);
        }   
        yield return 0;
    }

    void notAttacking()
    {
        attacking = false;
        
        GetComponent<PlayerMovement>().LastAnimation();
    }
}
