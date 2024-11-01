using System.Collections;
using System.Collections.Generic;
using Enemies;
using UnityEngine;
using Utils;
using Utils.Singleton;

public class WaveSpawner : Singleton<WaveSpawner>
{
    [SerializeField] private SOInt _currentWave;
    [SerializeField] private float _StartEnemiesCount;
    [SerializeField] List<EnemyBase> _enemies;
    [SerializeField] float _timeBetweenEnemies;
    [SerializeField] private LayerMask _spawnAvoidLayers;
    [SerializeField] private EnemyPools _enemyPools;
    [SerializeField] private Player player;
    [SerializeField] private Vector2 _spawnArea;
    [SerializeField] private int _maxOnScreenEnemies = 9;
    private float _currentWaveEnemies;
    private float _currentWaveEnemiesKilled;
    private EnemyBase _currentEnemy;
    public System.Action OnWaveEnded;

    public void StartNextWave()
    {
        _currentWaveEnemies = _currentWaveEnemiesKilled = 0;
        _currentWave.Value++;
        StartCoroutine(WaveRunning());
    }

    private IEnumerator WaveRunning()
    {
        while(_currentWaveEnemies < _StartEnemiesCount + (_currentWave.Value * 3))
        {
            if(_currentWaveEnemies - _currentWaveEnemiesKilled < _maxOnScreenEnemies)
            {
                _currentEnemy = _enemyPools.GetEnemy(_enemies.GetRandom());
                _currentEnemy.Init(player, GetRandomPointInPerimeter(), _currentWave.Value);
                _currentEnemy.Health.OnDeath += EnemyKilled;
                _currentWaveEnemies++;
            }
            yield return new WaitForSeconds(_timeBetweenEnemies - (.02f * _currentWave.Value));
        }
    }

    private void EnemyKilled(HealthBase hp)
    {
        hp.OnDeath -= EnemyKilled;
        _currentWaveEnemiesKilled++;
        if(_currentWaveEnemiesKilled == _currentWaveEnemies)
        {
            EndWave();
        }
    }

    private void EndWave()
    {
        OnWaveEnded?.Invoke();
    }

    public void RespawnEnemy(EnemyBase enemy)
    {
        enemy.Reposition(GetRandomPointInPerimeter());
    }

    public Vector2 GetRandomPointInPerimeter()
    {
        Vector2 point;
        do
        {
            point = player.transform.position;
            if(Random.Range(0, 2) == 0)
            {
                point.x += _spawnArea.x/2 * (Random.Range(0, 2)==0?1:-1);
                point.y += Random.Range(-_spawnArea.y/2, _spawnArea.y/2);
            }
            else
            {
                point.x += Random.Range(-_spawnArea.x/2, _spawnArea.x/2);
                point.y += _spawnArea.y/2 * (Random.Range(0, 2)==0?1:-1);
            }
        } while(Physics2D.OverlapPoint(point, _spawnAvoidLayers) != null);
        return point;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, _spawnArea);
    }
}
