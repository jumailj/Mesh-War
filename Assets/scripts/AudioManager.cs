using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{

    public Sound[] sound;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        foreach(Sound s in sound)
        {
           s.source =  gameObject.AddComponent<AudioSource>();

            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;

           // s.source.Play();
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sound, sound => sound.name == name);
           if(s == null)
        {
            return;
        }
        s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sound, sound => sound.name == name);
        s.source.Stop();
    }


}
