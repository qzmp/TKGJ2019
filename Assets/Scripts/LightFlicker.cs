using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LightFlicker : MonoBehaviour
{
    public float highDuration = 0.5f;
    public float lowDuration = 2f;

    private LightSource light;

    private bool highIntensity = false;
    private Tween currentTween;

    private void Awake()
    {
        this.light = GetComponent<LightSource>();
    }

    private void Start()
    {
        this.PulseUp();
    }

    public void SetIntensity(bool high)
    {
        if(this.highIntensity == high)
        {
            return;
        }

        if(this.currentTween != null)
        {
            this.currentTween.Kill();
        }
        this.highIntensity = high;

        PulseUp();
    }

    private void PulseUp()
    {
        float maxIntensity = this.highIntensity ? 5 : 2f;
        float duration = this.highIntensity ? this.highDuration : this.lowDuration;

        this.currentTween = DOTween.To(() => light.intensity, x => light.intensity = x, maxIntensity, duration).OnComplete(PulseDown).SetEase(Ease.InOutExpo);
    }

    private void PulseDown()
    {
        float maxIntensity = this.highIntensity ? 4 : 1.5f;
        float duration = this.highIntensity ? this.highDuration : this.lowDuration;

        this.currentTween = DOTween.To(() => light.intensity, x => light.intensity = x, maxIntensity, duration).OnComplete(PulseUp).SetEase(Ease.InOutExpo);
    }
}
