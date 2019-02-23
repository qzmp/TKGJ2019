using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerAudioController : AudioController
{
    [SerializeField] protected AudioSource _detectedAudioSource;
    [SerializeField] protected AudioSource _deathAudioSource;
    
    
    public override void SetWalkAudio(Vector2 velocity)
    {
        if (Player.IsAlive)
        {
            if (velocity.magnitude > 0)
            {
                _walkAudioSource.loop = true;
                if (_walkAudioSource.loop && !_walkAudioSource.isPlaying)
                {
                    _walkAudioSource.Play();
                }
            }
            else
            {
                _walkAudioSource.loop = false;
            }
        }
    }
    
    
    public void PlayDetectedAudioSource()
    {
        if (!_detectedAudioSource.isPlaying && Player.IsAlive)
        {
            _detectedAudioSource.Play();
        }
    }
    public void PlayDeathAudioSource()
    {
        if (!_deathAudioSource.isPlaying)
        {
            _walkAudioSource.Stop();
            _detectedAudioSource.Stop();
            _deathAudioSource.Play();
        }
    }
}
