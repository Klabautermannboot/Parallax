using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.Audio;


public class StartLevel : MonoBehaviour
{
    public Animator animator;
    public GameObject player1;
    public GameObject player2;

    //Lights

    public UnityEngine.Rendering.Universal.Light2D redLight;
    public UnityEngine.Rendering.Universal.Light2D blueLight;
    public UnityEngine.Rendering.Universal.Light2D redPlayerLight;
    public UnityEngine.Rendering.Universal.Light2D bluePlayerLight;


     //Mixers & Sounds
     
     
     public AudioMixer ambience;
     



     //Canvases


    public Canvas score;

    public Canvas blue;
    public Canvas red; 
    public CanvasGroup black;

    public float animTime;



    void Start()
    {
     if(PlayerPrefs.GetInt("Red") == 0 && PlayerPrefs.GetInt("Blue") == 0)
     {
        player2.GetComponent<PlayerCombat2>().enabled = false;
        player2.GetComponent<Shoot1>().enabled = false;  
        player2.GetComponent<PlayerMovement2>().enabled = false;
        player1.GetComponent<PlayerCombat>().enabled = false;
        player1.GetComponent<Shoot>().enabled = false;  
        player1.GetComponent<PlayerMovement>().enabled = false;

     



        redPlayerLight.gameObject.SetActive(false);
        bluePlayerLight.gameObject.SetActive(false);
        blueLight.gameObject.SetActive(false);
        redLight.gameObject.SetActive(false);

        black.gameObject.SetActive(true);
        score.gameObject.SetActive(false);
        
        animator.SetTrigger("Out");
        Invoke("BeginAnim", .8f);
     }
    }

    void BeginAnim()
    {
        StartCoroutine(BeginLevel());
    }

   IEnumerator BeginLevel()
   {

          FindObjectOfType<MixerManager>().SetAmbienceLevel(-60f);
          FindObjectOfType<MixerManager>().SetMusicLevel(-10f);
          redLight.gameObject.SetActive(true);
          FindObjectOfType<AudioManager>().PlayDelayed("Easy",0.5f);
          red.gameObject.SetActive(true);
          animator.SetTrigger("Start");


       yield return new WaitForSeconds(animTime);
        
        StartCoroutine(Blue());
        

   }

   IEnumerator Blue()
   {
        animator.SetTrigger("Start");
        red.gameObject.SetActive(false);
        redLight.gameObject.SetActive(false);
        FindObjectOfType<AudioManager>().PlayDelayed("Haha",0.5f);
        blueLight.gameObject.SetActive(true);
        blue.gameObject.SetActive(true);

       yield return new WaitForSeconds(animTime);
       
       
        StartCoroutine(StartGame());

   }
     IEnumerator StartGame()
   {
        
        animator.SetTrigger("Start");
        blue.gameObject.SetActive(false);
        blueLight.gameObject.SetActive(false);

        yield return new WaitForSeconds(.7f);

        Begin();

   }

   void Begin()
   {    
        ambience.SetFloat("Ambience",-42f);
          FindObjectOfType<MixerManager>().SetMusicLevel(4f);
        player2.GetComponent<PlayerCombat2>().enabled = true;
        player2.GetComponent<Shoot1>().enabled = true;  
        player2.GetComponent<PlayerMovement2>().enabled = true;
        player1.GetComponent<PlayerCombat>().enabled = true;
        player1.GetComponent<Shoot>().enabled = true;  
        player1.GetComponent<PlayerMovement>().enabled = true;

        redPlayerLight.gameObject.SetActive(true);
        bluePlayerLight.gameObject.SetActive(true);
        blueLight.gameObject.SetActive(true);
        redLight.gameObject.SetActive(true);

        score.gameObject.SetActive(true);
   }
}
