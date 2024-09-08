using Enemies;
using UnityEngine;

[RequireComponent(typeof(EnemyBase))]
public class OnDeathMoney : MonoBehaviour
{
    [SerializeField] private EnemyBase _enemy;
    public SOInt coinsSO;

    private void OnValidate()
    {
        if(_enemy == null)
        {
            _enemy = GetComponent<EnemyBase>();
        }
    }

    private void Start()
    {
        _enemy.health.OnDeath += OnDeath;
    }

    private void OnDeath(HealthBase hp)
    {
        coinsSO.Value += _enemy.Coins;
    }
}
