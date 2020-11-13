using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(menuName = "Content/AudioSystem")]
public class AudioSystem : ScriptableObject
{
    [SerializeField] private AudioMixerGroup _audioMixerGroup = null;
        
    public Sound[] sounds;

    public void Init(GameObject gameObject)
    {
        foreach (var sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.outputAudioMixerGroup = _audioMixerGroup;
        }
    }
    
    public void Play(string soundName)
    {
        var s = Array.Find(sounds, sound => sound.name == soundName);
        if (s == null)
        {
            Debug.Log("Звук с таким именем не найден!");
            return;
        }

        s.source.Play();
    }
}
