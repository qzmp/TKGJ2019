using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class Pickup : MonoBehaviour
{
    public AudioSource pickupAudioSource;
    public UnityEvent onPickup;
    public bool activated = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && !this.activated)
        {
            this.activated = true;
            if (onPickup != null)
            {
                onPickup.Invoke();
                OnPickupSound();
            }

            this.transform.DOScale(0, 1).OnComplete(() => Destroy(this.gameObject)).SetEase(Ease.InCubic);
        }
    }

    private void OnPickupSound()
    {
        pickupAudioSource.Play();
    }
}
