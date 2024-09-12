using System.Collections;
using System.Collections.Generic;
using Enemies;
using UnityEngine;
using Utils;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private SOInt _currentWave;
    [SerializeField] private float _StartEnemiesCount;
    [SerializeField] List<EnemyBase> _enemies;
    [SerializeField] float _timeBetweenEnemies;
    [SerializeField] private EnemyPools _enemyPools;
    [SerializeField] private Player player;
    [SerializeField] private Vector2 _spawnArea;
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
        while(_currentWaveEnemies < _StartEnemiesCount + (_currentWave.Value * 4))
        {
            _currentEnemy = _enemyPools.GetEnemy(_enemies.GetRandom());
            _currentEnemy.Init(player, GetRandomPointInPerimeter(), _currentWave.Value);
            _currentEnemy.Health.OnDeath += EnemyKilled;
            yield return new WaitForSeconds(_timeBetweenEnemies - .02f * _currentWave.Value);
            _currentWaveEnemies++;
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

    private Vector2 GetRandomPointInPerimeter()
    {
        Vector2 point = (Vector2)player.transform.position;
        while(Physics2D.OverlapPoint(point) != null)
        {
            if(Random.Range(0, 2) == 0)
            {
                point.x += _spawnArea.x/2 * (Random.Range(0, 2)==0?1:-1);
                point.y += Random.Range(0, _spawnArea.y/2) * (Random.Range(0, 2)==0?1:-1);
            }
            else
            {
                point.x += Random.Range(0, _spawnArea.x/2) * (Random.Range(0, 2)==0?1:-1);
                point.y += _spawnArea.y/2 * (Random.Range(0, 2)==0?1:-1);
            }
        }
        return point;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, _spawnArea);
    }
}
