using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class AbilityDisplay : MonoBehaviour
{
    private Image image;
    public Image cooldownImage;

    public bool isReversed = false;

    // Start is called before the first frame update
    void Start()
    {
        this.image = GetComponent<Image>();
    }

    public void Show()
    {
        this.gameObject.SetActive(true);
        this.transform.DOScale(0, 0.5f).From().SetEase(Ease.OutBounce);
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
        if (isReversed)
        {
            this.cooldownImage.fillAmount = value;
        }
        else
        {
            this.cooldownImage.fillAmount = 1 - value;
        }
    }
}
