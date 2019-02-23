using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class MainCamera : MonoBehaviour
{
    public static MainCamera Instance;
    [SerializeField] private PostProcessProfile _postProcessProfile;

    void Start()
    {
        Instance = this;
    }

    public void SetVignetteIntensity(FloatParameter intensity)
    {
        Vignette vignette;
        _postProcessProfile.TryGetSettings(out vignette);

        if (vignette)
        {
            vignette.intensity = intensity;
        }
    }
}