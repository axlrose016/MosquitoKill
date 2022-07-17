using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    void Awake()
    {
        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.loop = s.isLoop;
            s.source.pitch = s.pitch;
            s.source.volume = s.volume;

        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.ClipName == name);
        s.source.Play();
    }
}
