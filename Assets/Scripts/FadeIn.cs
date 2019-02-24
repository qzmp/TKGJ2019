using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.DOColor(new Color(0, 0, 0, 0), 3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
