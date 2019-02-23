﻿using System.Collections;
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

    public void SetBrightness(FloatParameter intensity)
    {
        ColorGrading colorGrading;
        _postProcessProfile.TryGetSettings(out colorGrading);

        if (colorGrading)
        {
            colorGrading.brightness = intensity;
        }
    }
}