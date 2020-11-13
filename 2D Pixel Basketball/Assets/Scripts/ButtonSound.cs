using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{
    public AudioClip sound;

    private Button button => GetComponent<Button>();
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = sound;
        audioSource.playOnAwake = false;
        
        button.onClick.AddListener(PlaySound);
    }

    private void PlaySound()
    {
        audioSource.PlayOneShot(sound);
    }
}
