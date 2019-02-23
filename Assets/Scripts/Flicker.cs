using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(LightSource))]
public class Flicker : MonoBehaviour
{
    public bool flickerTransformScale = false;
    private Vector2 _noiseScroller;

    public Vector2 scrollingSpeed = Vector2.one;

    private Vector2 _randomOffsetStart = Vector2.one;

    public float multiplier = 1f;

    private LightSource _lightSource;

    private void Start()
    {
        _lightSource = GetComponent<LightSource>();
        _randomOffsetStart = new Vector2(Random.Range(0, 10), Random.Range(0, 10));
    }

    void Update()
    {
        _noiseScroller += scrollingSpeed;

        var noiseOffset = _randomOffsetStart + _noiseScroller;

        float value = Mathf.PerlinNoise(noiseOffset.x, noiseOffset.y) * multiplier;

        _lightSource.intensity = value;

        if (flickerTransformScale)
        {
            transform.localScale = Vector3.one * value;
        }
    }
}
