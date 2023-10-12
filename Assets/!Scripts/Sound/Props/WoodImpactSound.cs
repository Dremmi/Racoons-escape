using System;
using System.Collections.Generic;
using Extensions;
using UnityEngine;

namespace Sound
{
    public class WoodImpactSound : ImpactSound
    {
        [SerializeField] private List<Sound> _crashSounds;

        private AudioSource _audioSource;
        
        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();

            foreach (var sound in _crashSounds)
            {
                sound.SetAudioSource(_audioSource);
            }
        }

        protected override void OnAnyCollision(Collision collision)
        {
            base.OnAnyCollision(collision);
            
            _crashSounds.RandomItem().Play();
        }
    }
}