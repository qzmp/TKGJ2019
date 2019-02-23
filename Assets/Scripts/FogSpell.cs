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

    private Player _player;

    [SerializeField]
    private float _fogCost;

    [SerializeField]
    private float _fogDistance;

    private float _fogCount;

    [SerializeField]
    private float _fogMax;

    private float lastFogTime;
    public float fogCooldown;
    public bool canUseFog = false;

    void Awake()
    {
        this._player = GetComponent<Player>();
    }

    private void Start()
    {
        this.lastFogTime = float.NegativeInfinity;

        if (this.canUseFog)
        {
            AbilityDisplayController.Instance.ShowFogDisplay();
        }
    }

    // Update is called once per frame
    void Update()
    {

        AbilityDisplayController.Instance.SetFogDisplay((Time.time - this.lastFogTime) / this.fogCooldown);
        if (Input.GetAxis("Fire1") != 0 && _player.Mana > _fogCost && _fogCount < _fogMax)        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 50))
            {
                if (Vector3.Distance(transform.position, hit.point) < _fogDistance)
                {
                    SpawnFog(hit.point);
                }
            }
        }
    }

    private bool IsFogReady()
    {
        return Time.time > this.lastFogTime + this.fogCooldown;
    }

    public void SpawnFog(Vector3 spawnPosition)
    {
        //if (mana > _fogCost)
        {
            AbilityDisplayController.Instance.ActivateFogDisplay();
            this.lastFogTime = Time.time;
            //_player.Mana -= _fogCost;
            _fogCount++;
            Transform fogSpell = Instantiate(_fogPrefab, spawnPosition, Quaternion.identity);
            fogSpell.DOScale(0.0f, _durationTime).SetEase(Ease.InSine).OnComplete(() => { _fogCount--; Destroy(fogSpell.gameObject); });
        }
    }

    public void TeachFog()
    {
        this.canUseFog = true;
        AbilityDisplayController.Instance.ShowFogDisplay();

    }
}
