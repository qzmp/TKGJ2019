using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityDisplayController : MonoBehaviour
{
    public static AbilityDisplayController Instance;

    public AbilityDisplay dashDisplay;
    public AbilityDisplay fogDisplay;
    public AbilityDisplay stunDisplay;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    public void ShowDashDisplay()
    {
        this.dashDisplay.Show();
    }

    public void ActivateDashDisplay()
    {
        this.dashDisplay.Activate();
    }

    public void SetDashDisplay(float fill)
    {
        this.dashDisplay.SetCooldown(fill);
    }

    public void ShowFogDisplay()
    {
        this.fogDisplay.Show();
    }

    public void ActivateFogDisplay()
    {
        this.fogDisplay.Activate();
    }

    public void SetFogDisplay(float fill)
    {
        this.fogDisplay.SetCooldown(fill);
    }

    public void ShowStunDisplay()
    {
        this.stunDisplay.Show();
    }

    public void ActivateStunDisplay()
    {
        this.stunDisplay.Activate();
    }

    public void SetStunDisplay(float fill)
    {
        this.stunDisplay.SetCooldown(fill);
    }

}
