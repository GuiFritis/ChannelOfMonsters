using System.Collections.Generic;
using UnityEngine.Pool;
using UnityEngine;
using Enemies;

[System.Serializable]
public class EnemyPools : MonoBehaviour
{
    [SerializeField] private List<EnemyPooling> _pools = new();

    protected void Awake()
    {
        _pools.ForEach(i => i.Init());
    }

    public EnemyBase GetEnemy(EnemyBase enemy)
    {
        EnemyPooling pooling = _pools.Find(i => i.enemy_pfb.Equals(enemy));
        if(pooling == null)
        {
            pooling = new EnemyPooling{
                enemy_pfb = enemy
            };
            pooling.Init();
            _pools.Add(pooling);
        }
        enemy = pooling.GetItem();
        enemy.transform.SetParent(gameObject.transform);
        return enemy;
    }
}

[System.Serializable]
public class EnemyPooling
{
    public EnemyBase enemy_pfb;
    private ObjectPool<EnemyBase> _pool;

    public void Init()
    {
        _pool = new ObjectPool<EnemyBase>(
            CreateEnemy,
            OnGetFromPool,
            OnReleaseToPool
        );
    }

    public EnemyBase GetItem()
    {
        return _pool.Get();
    }

    private EnemyBase CreateEnemy()
    {
        var enemy = GameObject.Instantiate(enemy_pfb);
        enemy.ObjectPool = _pool;
        return enemy;
    }

    private void OnGetFromPool(EnemyBase enemy)
    {
        enemy.gameObject.SetActive(true);
    }

    private void OnReleaseToPool(EnemyBase enemy)
    {
        enemy.gameObject.SetActive(false);
    }
}
