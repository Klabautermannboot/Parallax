using System.Linq.Expressions;
using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Score : MonoBehaviour
{
    private int redDeathCount;
    private bool playerDied=false;
    private int blueDeathCount;
    
    public Text score;

    public void Start()
    {
       
       

        score.text = PlayerPrefs.GetInt("Red",0).ToString("") +  " : " + PlayerPrefs.GetInt("Blue",0).ToString("");
        redDeathCount = PlayerPrefs.GetInt("Red");
        blueDeathCount = PlayerPrefs.GetInt("Blue");

    }



    public void blueHasKilled()
    {
        blueDeathCount++;
        playerDied = true;
    }
    public void redHasKilled()
    {
        redDeathCount++;
        playerDied = true;
    }
    // Update is called once per frame
    void Update()
    {   
        if(Input.GetKeyDown(KeyCode.Backspace))
        {
            redDeathCount = 3;
        }

        if(playerDied)
        {
            score.text = redDeathCount.ToString("") +  " : " + blueDeathCount.ToString("");
            playerDied = false;
        
        if( redDeathCount < 3 && blueDeathCount < 3)
        {
            FindObjectOfType<AudioManager>().Play("Kill");
            Invoke("GameOver",2f);
            

        }
        else 
        {
            FindObjectOfType<AudioManager>().PlayDelayed("Victory",0.1f);
            
            Invoke("GoMenu", 4f);
            
        }
        
        }
        
      
     
    }

     public void OnDestroy() 
    {   
        PlayerPrefs.SetInt("Red",redDeathCount);//+PlayerPrefs.GetInt("Red"));
        PlayerPrefs.SetInt("Blue",blueDeathCount);//+PlayerPrefs.GetInt("Blue"));

    }

    void GameOver()
    {
        GameObject.FindObjectOfType<GameManagement>().GameOver();

    }
    void GoMenu()
    {   
        SceneManager.LoadScene("Menu");
    }
}
