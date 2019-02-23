using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Pickup : MonoBehaviour
{
    public UnityEvent onPickup;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(onPickup != null)
        {
            onPickup.Invoke();
        }
    }
}
