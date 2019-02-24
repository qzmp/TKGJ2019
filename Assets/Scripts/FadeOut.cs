using DG.Tweening;
using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : MonoBehaviour
{
    [SerializeField]
    private Player _player;

    public void fadeOut()
    {
        foreach (AIPath enemy in FindObjectsOfType<AIPath>())
        {
            enemy.canMove = false;
        }
        
        GetComponent<SpriteRenderer>().DOColor(new Color(255, 255, 255, 255), 2).SetEase(Ease.InSine).OnComplete(() => _player.Win());
    }
}
