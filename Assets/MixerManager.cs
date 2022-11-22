using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Reflection.Emit;
using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class MixerManager : MonoBehaviour
{
    public AudioMixer main;
    public Toggle toggle;
    private float volume = 0f;

void Awake()
{   
    FindObjectOfType<Toggle>();

    if(PlayerPrefs.GetInt("On") == 0)
    {
        toggle.isOn = true;
        FindObjectOfType<ChangeText>().ChangeTheText("On");
    }
    else
    {
        toggle.isOn = false;
        FindObjectOfType<ChangeText>().ChangeTheText("Off");
    }
}
    public void SetAmbienceLevel(float SfxLvl)
    {
        main.SetFloat("Ambience", SfxLvl);
    }

 public void SetMusicLevel(float musicLvl)
    {
        main.SetFloat("MusicVol", musicLvl);
    }

public void SetSoundOff(bool Sound)
    {
        if(Sound)
        {
            main.SetFloat("Volume", 0f);
            FindObjectOfType<ChangeText>().ChangeTheText("On");
            PlayerPrefs.SetInt("On",0);
        }
        else
        {
            main.SetFloat("Volume", -80f);  
            FindObjectOfType<ChangeText>().ChangeTheText("Off");
            PlayerPrefs.SetInt("On",1);
        }
    }

}
