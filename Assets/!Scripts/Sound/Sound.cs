using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;


namespace Sound
{
    [Serializable]
    public class Sound
    {
        [SerializeField] private AudioClip clip;
        private AudioSource _audioSource;
        private const float VOLUME = 0.5f;

        public void SetAudioSource(AudioSource audioSource)
        {
            _audioSource = audioSource;
        }
        
        public void Play()
        {
            _audioSource.PlayOneShot(clip, VOLUME);
        }
    }
}

