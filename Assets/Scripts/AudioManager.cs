using System;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    
    public static AudioManager instance;

    public void Awake()
    {

        DontDestroyOnLoad(gameObject);

        if( instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

   
        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.priority = s.priority;
            s.source.outputAudioMixerGroup = s.output;
        } 
    }

    public void Start() 
    {
        Play("Theme");   
    }

   public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
        {
        Debug.LogWarning("Sound: " + name + " was not found!");
        return;
        }
        s.source.Play();
    }

    public void PlayDelayed(string name, float delay)
    {
         Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
        {
        Debug.LogWarning("Sound: " + name + " was not found!");
        return;
        }
        s.source.PlayDelayed(delay);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.N))
        {
            Play("Easy");
        }

        if(Input.GetKeyDown(KeyCode.M))
        {
            Play("Haha");
        }   
    }
}
