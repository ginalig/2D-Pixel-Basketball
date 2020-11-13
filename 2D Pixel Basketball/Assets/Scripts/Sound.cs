using System;
using UnityEngine;

[Serializable]
public class Sound
{
    public AudioClip clip;
    public string name;

    [Range(0f, 1f)] public float volume = 1;
    [Range(.1f, 3f)] public float pitch = 1;

    [HideInInspector] public AudioSource source;
    
}