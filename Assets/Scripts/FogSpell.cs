using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogSpell : MonoBehaviour
{
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
        if (_player.mana > _fogCost && Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, LayerMask.GetMask("Background")))
            {
                if (Vector3.Distance(transform.position, hit.point) < _fogDistance)
                {
                    SpawnFog(hit);
                }
            }
        }
    }

    public void SpawnFog(RaycastHit raycastHit)
    {
        //if (mana > _fogCost)
        {
            _player.mana -= _fogCost;
            Instantiate(_fogPrefab, raycastHit.point, Quaternion.identity);
        }
    }
}
