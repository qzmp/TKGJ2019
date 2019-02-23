using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sway : MonoBehaviour
{
    private Vector3 _anchor;
    private Vector3 _offset;

    private Vector3 _currentOffsetTarget;

    public float swayRange = 1f;
    public float swayLerp = 0.1f;


    private Quaternion _currentRotationTarget;
    public float rotationLerp = 0.1f;
    public float rotationRange = 60f;

    private void Start()
    {
        _anchor = transform.position;
        _offset = Vector3.zero;
        _currentRotationTarget = Quaternion.identity;
    }

    private void Update()
    {
        var distancToTarget = Vector3.Distance(_offset, _currentOffsetTarget);

        if (distancToTarget < 0.1f)
        {
            _currentOffsetTarget = new Vector3(Random.Range(-swayRange, swayRange), Random.Range(-swayRange, swayRange), 0f);
        }

        if (Quaternion.Angle(transform.rotation, _currentRotationTarget) < 1f)
        {
            _currentRotationTarget = transform.rotation * Quaternion.Euler(0f, 0f, Random.Range(-rotationRange, rotationRange));
        }

        _offset = Vector3.Lerp(_offset, _currentOffsetTarget, swayLerp);

        transform.rotation = Quaternion.Lerp(transform.rotation, _currentRotationTarget, rotationLerp);
        transform.position = _anchor + _offset;
    }
}
