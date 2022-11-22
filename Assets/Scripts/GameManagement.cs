using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManagement : MonoBehaviour
{

    public float restartDelay;
    private bool gameHasEnded = false;

    public void GameOver()
    {
        gameHasEnded=true;

        if(gameHasEnded==true)
        {   
            Invoke("Restart",restartDelay);
        }
    }


    public void Restart()
    {
        SceneManager.LoadScene("Game");
    }
}
