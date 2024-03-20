using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;
    public AudioMixerGroup SFX;
    public AudioMixerGroup Musique;
    // Start is called before the first frame update
    void Awake()
    {

        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            
            if (s.SFX)
            {
                s.source.outputAudioMixerGroup = SFX;
                s.source.playOnAwake = false;
            }
            if (s.Music)
            {
                s.source.outputAudioMixerGroup = Musique;
            }
        }

    }
    private void Update()
    {
/*        if (Vector3.Distance(gameObject.transform.position, Beuverie_GameManager.GM_instance.Player.transform.position) > 60)
        {
            foreach (Sound s in sounds)
            {
                s.source.enabled = false;
            }
        }
        else
        {
            foreach (Sound s in sounds)
            {
                s.source.enabled = true;
            }
        }*/
    }
    public void Play (string name)
    {
       Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            return;
        }
        s.source.Play();
    }
    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            return;
        }
        s.source.Stop();
    }
    public void PlayOneShot(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            return;
        }
        s.source.PlayOneShot(s.clip);
    }
}
