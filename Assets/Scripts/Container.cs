using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public interface IOpenable
{
    void Open();
}

public class Container : MonoBehaviour, IOpenable
{
    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private GameObject _itemToSpawnPrefab;

    [SerializeField]
    private Transform _spawnPoint;

    private bool _isOpened = false;
    private bool _hasSpawnedItem = false;

    void Start()
    {
        Assert.IsNotNull(_animator);
        Assert.IsNotNull(_spawnPoint);
        Assert.IsNotNull(_itemToSpawnPrefab);
    }

    public void Open()
    {
        if (_isOpened)
        {
            Debug.LogWarning("Trying to open an opend container");
            return;
        }
        _animator.SetTrigger("open");
        SpawnItem();
    }

    public void SpawnItem()
    {
        if (_hasSpawnedItem)
        {
            Debug.LogWarning("Trying to spawn an already spawned item");
            return;
        }

        var item = Instantiate(_itemToSpawnPrefab) as GameObject;
        item.transform.position = _spawnPoint.position;
    }
}
