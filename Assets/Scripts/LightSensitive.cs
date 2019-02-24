using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Assertions;

[RequireComponent(typeof(CircleCollider2D))]
public class LightSensitive : MonoBehaviour
{
    private static LightSource[] _lightSources = null;

    public float delay = 1f;

    private CircleCollider2D _collider;

    private bool _isInLight = false;

    public bool IsInLight {
        get {
            return _isInLight;
        }
    }

    public event System.Action<bool> LightStatusChanged;

    public Junk.BoolEvent LightStatusChangedEvent;

    public LayerMask occludingLayers;

    void Start()
    {
        _collider = GetComponent<CircleCollider2D>();

        if (_lightSources == null)
        {
            _lightSources = FindObjectsOfType<LightSource>();
        }

        StartCoroutine(Routine());
    }

    private IEnumerator Routine()
    {
        while (true)
        {
            bool isInLight = CheckIsInLight();
            if (_isInLight != isInLight)
            {
                if (LightStatusChanged != null) LightStatusChanged(isInLight);
                LightStatusChangedEvent.Invoke(isInLight);
                _isInLight = isInLight;
            }

            yield return new WaitForSeconds(delay);
        }
    }

    private bool CheckIsInLight()
    {
        Debug.Log("Checking if in light...");
        foreach (var lightSource in _lightSources)
        {
            if (CheckLightSource(lightSource))
            {
                return true;
            }
        }

        return false;
    }

    private bool CheckLightSource(LightSource source)
    {
        Debug.Log("Checking light source " + source.gameObject.name);
        float distance = Vector3.Distance(source.transform.position, transform.position);
        if (!source.on || distance > source.size)
        {
            Debug.Log("Light source is off or out of range, returning false");
            return false;
        }

        Vector3 dirToLight = source.transform.position - this.transform.position;
        Vector3 rightVector = Vector3.Normalize(Quaternion.Euler(0, 0, 90) * dirToLight);
        Vector3 leftVector = -rightVector;
        Vector3 rightPoint = this.transform.position + rightVector * this._collider.radius;
        Vector3 leftPoint = this.transform.position + leftVector * this._collider.radius;

        Vector3 dirToRightPoint = rightPoint - source.transform.position;
        Vector3 dirToLeftPoint = leftPoint - source.transform.position;

        Debug.DrawRay(source.transform.position, dirToRightPoint);
        Debug.DrawRay(source.transform.position, dirToLeftPoint);
        Debug.DrawRay(source.transform.position, -dirToLight);


        if (!Physics2D.Raycast(source.transform.position, dirToRightPoint, distance, this.occludingLayers))
        {
            Debug.Log("A raycast succeded returning true");
            return true;
        }
        if (!Physics2D.Raycast(source.transform.position, dirToLeftPoint, distance, this.occludingLayers))
        {
            Debug.Log("A raycast succeded returning true");
            return true;
        }
        if (!Physics2D.Raycast(source.transform.position, -dirToLight, distance, this.occludingLayers))
        {
            Debug.Log("A raycast succeded returning true");
            return true;
        }

        Debug.Log("All raycasts failed returning false");
        return false;
    }
}
