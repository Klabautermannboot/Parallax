using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Options : MonoBehaviour
{
    // Start is called before the first frame update
    public void Option()
    {
    
    SceneManager.LoadScene("Options", LoadSceneMode.Single);  

    }
}