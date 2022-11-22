
using UnityEngine;
using UnityEngine.SceneManagement;

public class Start : MonoBehaviour
{
    // Start is called before the first frame update
    public void Star()
    {
    
    PlayerPrefs.DeleteKey("Red");
    PlayerPrefs.DeleteKey("Blue");
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);  

    }
}
