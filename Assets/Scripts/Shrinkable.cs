using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Collider2D))]
public class Shrinkable : MonoBehaviour
{
    private struct ChildEntry
    {
        public ChildEntry(GameObject child, Vector3 baseScale)
        {
            this.child = child;
            this.baseScale = baseScale;
        }

        public GameObject child;
        public Vector3 baseScale;
    }

    private ChildEntry[] _children;

    private bool _isShrinked = false;

    private Tween _currentTween = null;

    public float shrinkingTimeSeconds = 1f;

    private float _currentScale = 1f;

    private Collider2D _collider;

    private void Start()
    {
        _children = Enumerable
            .Range(0, transform.childCount)
            .Select(i => transform.GetChild(i).gameObject)
            .Select(g => new ChildEntry(g, g.transform.localScale))
            .ToArray();

        _collider = GetComponent<Collider2D>();
    }

    public void Shrink()
    {
        if (_isShrinked)
        {
            Debug.LogWarning("Attempting to shrink a shrinked object");
            return;
        }

        _currentTween = DOTween.To(() => _currentScale, ApplyScale, 0f, shrinkingTimeSeconds).OnComplete(() => OnComplete(true) ).SetEase(Ease.InOutExpo);
    }

    public void UnShrink()
    {
        if (!_isShrinked)
        {
            Debug.LogWarning("Attempting to unshrink an object that is not shrinked");
            return;
        }
        _currentTween = DOTween.To(() => _currentScale, ApplyScale, 1f, shrinkingTimeSeconds).OnComplete(() => OnComplete(false) ).SetEase(Ease.InOutExpo);
    }

    private void OnComplete(bool isShrinked)
    {
        _isShrinked = isShrinked;
        _collider.enabled = !isShrinked;
    }

    private void ApplyScale(float x)
    {
        foreach (var entry in _children)
        {
            entry.child.transform.localScale = entry.baseScale * x;
        }

        _currentScale = x;
    }
}
