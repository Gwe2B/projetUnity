using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    public AudioClip clip;
    public AudioMixerGroup audioMixerGroup;

    public string name;

    [Range(0f, 1f)]
    public float volume;

    [Range(0f, 3f)]
    public float pitch;

    public bool loop = false;

    [HideInInspector]
    public AudioSource source;
}