using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance = null;

    public Sound[] sounds;

    void Awake()
    {
        if(instance == null) { instance = this; }
        else { 
            Destroy(gameObject);
            return;
        }

        //DontDestroyOnLoad(gameObject);

        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;

            s.source.outputAudioMixerGroup = s.audioMixerGroup;
        }
    }

    private void Start()
    {
        Play("Theme");
    }

    private void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s != null && s.source.isPlaying) { s.source.Stop(); }
        else if(s == null) { Debug.LogWarning("Sound : " + name + " not found !"); }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s != null) { s.source.Play(); }
        else { Debug.LogWarning("Sound : " + name +" not found !"); }
    }
}
