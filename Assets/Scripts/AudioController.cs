using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class AudioController : MonoBehaviour
{
    [SerializeField] protected AudioSource _walkAudioSource;
    protected float pitchOffset = 0.05f;
    protected void Start()
    {
        StartCoroutine(ChangeWalkPitch());
    }

    public void SetLoopOnWalkAudio(bool value)
    {
        _walkAudioSource.loop = value;
        if (value && !_walkAudioSource.isPlaying)
        {
            _walkAudioSource.Play();
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
