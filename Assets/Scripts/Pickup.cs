using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class Pickup : MonoBehaviour
{
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
            }

            this.transform.DOScale(0, 1).OnComplete(() => Destroy(this.gameObject)).SetEase(Ease.InCubic);
        }
    }
}
