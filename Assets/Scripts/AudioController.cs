using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.Serialization;
using Vector2 = UnityEngine.Vector2;

public class AudioController : MonoBehaviour
{
    [SerializeField] protected AudioSource _walkAudioSource;
    [SerializeField] private AudioSource _detectedAudioSource;

    protected float pitchOffset = 0.05f;
    protected void Start()
    {
        StartCoroutine(ChangeWalkPitch());
    }

    public virtual void SetWalkAudio(Vector2 velocity)
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
   
    public void PlayDetectedAudioSource()
    {
        if (!_detectedAudioSource.isPlaying && Player.IsAlive)
        {
            _detectedAudioSource.Play();
        }
    }

    protected IEnumerator ChangeWalkPitch()
    {
        while (true)
        {
            if (_walkAudioSource.isPlaying)
            {
                _walkAudioSource.pitch = Random.Range(1 - pitchOffset, 1 + pitchOffset);
            }
        yield return null;
        }
    }
}
