using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerAudioController : AudioController
{
    [SerializeField] protected AudioSource _detectedAudioSource;
    [SerializeField] protected AudioSource _deathAudioSource;
    public void PlayDetectedAudioSource()
    {
        if (!_detectedAudioSource.isPlaying)
        {
            _detectedAudioSource.Play();
        }
    }
    public void PlayDeathAudioSource()
    {
        if (!_deathAudioSource.isPlaying)
        {
            _deathAudioSource.Play();
        }
    }
}
