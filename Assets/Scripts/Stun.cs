using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Stun : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        this.transform.DOScale(0, 0.1f).From().SetEase(Ease.Flash).OnComplete(() =>
        this.transform.DOScale(0, 1).SetEase(Ease.InOutQuad).OnComplete(() => Destroy(this.gameObject)));
    }
}
