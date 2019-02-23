using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class RandomizeParameter : MonoBehaviour
{
    public string paramName;
    public float max = 1f;
    public float min = 0f;

    public float lerp = 0.1f;

    private float _current;

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        _current = Mathf.Lerp(_current, Random.Range(min, max), lerp);

        _animator.SetFloat(paramName, _current);
    }
}
