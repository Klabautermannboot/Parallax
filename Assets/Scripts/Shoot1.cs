using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class Shoot1 : MonoBehaviour
{
    public Transform firePoint;
    ControlsManager controlsManager;
    [SerializeField]
    int PlayerID;
    public GameObject bullet;
    public float cooldown;
    private float rate = 0.75f;
    public Animator animator;
    private float animTime;
    bool shooting;

    
    // Start is called before the first frame update
    void Start()
    {
        controlsManager = FindObjectOfType<ControlsManager>();
        shooting = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= cooldown)
        {
        if(Input.GetKeyDown(controlsManager.GetKey(PlayerID,ControlKeys.AttackKey)) && Input.GetKeyDown(controlsManager.GetKey(PlayerID,ControlKeys.BoostKey)))
         {
            shooting = true;
            FindObjectOfType<AudioManager>().Play("Shoot");
            cooldown = Time.time +1 / rate;
            //Shooting();
            GetComponent<PlayerMovement2>().ChangeAnimationState("Player_shoot");
            animTime = animator.GetCurrentAnimatorClipInfo(0).Length;
            Invoke("notShooting",(animTime/4f));
            Invoke("Shooting",(animTime/3f));
         }
        }
    }

    void Shooting()
    {
        Instantiate(bullet,firePoint.position,firePoint.rotation);
        
    }

    void notShooting()
    {
        shooting = false;
        GetComponent<PlayerMovement2>().LastAnimation();
    }

   
}
