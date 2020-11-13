using System;
using UnityEngine;
using UnityEngine.Audio;

namespace Resources.Scripts
{
    public class AudioManager : MonoBehaviour
    {

        public AudioSystem audioSystem;

        private void Start()
        {
            audioSystem.Init(gameObject);
        }
    }
}