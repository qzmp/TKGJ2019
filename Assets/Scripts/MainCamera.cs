using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class MainCamera : MonoBehaviour
{
    public static MainCamera Instance;
    [SerializeField] private PostProcessProfile _postProcessProfile;
    [SerializeField] private float _contrastValue;
    [SerializeField] private float _grainValue;
    void Start()
    {
        Instance = this;
        StartCoroutine(ContrastFadeOut());
        StartCoroutine(GrainFadeOut());
    }

    public void OnInLight()
    {
        SetContrast(_contrastValue);
        SetGrain(_grainValue);
    }
    
    private void SetContrast(float intensity)
    {
        ColorGrading colorGrading;
        _postProcessProfile.TryGetSettings(out colorGrading);

        if (colorGrading)
        {
            colorGrading.contrast.value = intensity;
        }
    }
    
    private void SetGrain(float intensity)
    {
        Grain grain;
        _postProcessProfile.TryGetSettings(out grain);

        if (grain)
        {
            grain.intensity.value = intensity;
        }
    }

    private IEnumerator ContrastFadeOut()
    {
        Grain grain;
        _postProcessProfile.TryGetSettings(out grain);
        while (true)
        {
            yield return new WaitUntil(() => grain.intensity.value != 0f);
            grain.intensity.Interp(grain.intensity.value,0.5f,Time.deltaTime);
        }
    }
    private IEnumerator GrainFadeOut()
    {
        ColorGrading colorGrading;
        _postProcessProfile.TryGetSettings(out colorGrading);
        while (true)
        {
            yield return new WaitUntil(() => colorGrading.contrast.value != 0f);
            colorGrading.contrast.Interp(colorGrading.contrast.value,0f,Time.deltaTime);
        }
    }


}