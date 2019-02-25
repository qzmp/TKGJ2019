using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.SceneManagement;

public class PlayerAudioController : AudioController
{
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
    public void PlayDeathAudioSource()
    {
        if (!_deathAudioSource.isPlaying)
        {
	        _walkAudioSource.loop = false;
			_walkAudioSource.Stop();
			_deathAudioSource.Play();
            StartCoroutine(WaitForDeathAudioEnd(3));
        }
    }

    private IEnumerator WaitForDeathAudioEnd(float time)
    {
        float startTime = Time.time;
        while(startTime + time > Time.time)
        {
            yield return null;
        }
        SceneManager.LoadScene(3);
    }
}
