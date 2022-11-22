using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeText : MonoBehaviour
{
    // Start is called before the first frame update
       public Text label;

public void ChangeTheText(string text) 
    {
    label.text = text;
    }


}
