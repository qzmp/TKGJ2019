using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogSpell : MonoBehaviour
{
    [SerializeField]
    private float _durationTime;

    [SerializeField]
    private Transform _fogPrefab;

    [SerializeField]
    private Player _player;

    [SerializeField]
    private float _fogCost;

    [SerializeField]
    private float _fogDistance;

    [SerializeField]
    private float _fogCount;

    [SerializeField]
    private float _fogMax;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_player.Mana > _fogCost && _fogCount < _fogMax && Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (Vector3.Distance(transform.position, hit.point) < _fogDistance)
                {
                    SpawnFog(hit.point);
                }
            }
        }
    }

    public void SpawnFog(Vector3 spawnPosition)
    {
        //if (mana > _fogCost)
        {
            _player.Mana -= _fogCost;
            _fogCount++;
            Transform fogSpell = Instantiate(_fogPrefab, spawnPosition, Quaternion.identity);
            fogSpell.DOScale(0.0f, _durationTime).SetEase(Ease.InSine).OnComplete(() => { _fogCount--; Destroy(fogSpell.gameObject); });
        }
    }
}
