using System.Collections;
using UnityEngine;
using Utils;

public class CollectablesSpawner : MonoBehaviour
{
    [SerializeField] private CollectablesPool _pool;
    [SerializeField] private int _maxCollectables;
    [SerializeField] private float _timeBetweenSpawns;
    private Collectable _collectable;
    private int _collectablesCount;

    private void Start()
    {
        SpawnCollectable();
        StartCoroutine(SpawnsCollectableCoroutine());
    }

    private IEnumerator SpawnsCollectableCoroutine()
    {
        while(true)
        {
            yield return new WaitForSeconds(_timeBetweenSpawns);
            SpawnCollectable();
        }
    }

    private void SpawnCollectable()
    {
        if(_collectablesCount < _maxCollectables)
        {
            _collectable = _pool.GetPoolItem();
            _collectable.transform.position = WaveSpawner.Instance.GetRandomPointInPerimeter();
            _collectable.Init();
            _collectable.OnCollect += Collected;
            _collectablesCount++;
        }
    }

    private void Collected(Collectable collectable)
    {
        collectable.OnCollect -= Collected;
        _collectablesCount--;
    }
}
