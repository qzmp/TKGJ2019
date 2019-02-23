using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class AbilityDisplay : MonoBehaviour
{
    private Image image;
    public Image cooldownImage;

    // Start is called before the first frame update
    void Start()
    {
        this.image = GetComponent<Image>();
    }

    public void Show()
    {

    }

    public void Activate()
    {
        this.image.DOFade(0.5f, 0.5f);
        this.transform.DOPunchScale(new Vector3(-0.3f, -0.3f, -0.3f), 0.5f);
    }

    public void SetCooldown(float value)
    {
        if(value >= 1)
        {
            this.image.DOFade(1, 0.5f);
        }
        this.cooldownImage.fillAmount = 1 - value;
    }
}
