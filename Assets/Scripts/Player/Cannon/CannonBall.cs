using UnityEngine;
using Utils;

[RequireComponent(typeof(Collider2D))]
public class CannonBall : MonoBehaviour, IPoolItem
{
    [SerializeField] private float _speed;
    [SerializeField] private float _damage;
    [Tooltip("The time the cannon ball takes to hit the water and be disabled")]
    [SerializeField] private float _duration;
    [SerializeField] private LayerMask _hitLayer;
    private CannonBallPool _pool;

    private void Update()
    {
        if(_speed > 0)
        {
            transform.Translate(_speed * Time.deltaTime * Vector2.right);
        }
    }

    public void GetFromPool()
    {
        gameObject.SetActive(true);
    }

    public void ReturnToPool()
    {
        CancelInvoke();
        _speed = _damage = 0f;
        gameObject.SetActive(false);
    }

    public void Shoot(float damage, float speed)
    {
        _damage = damage;
        _speed = speed;
        Invoke(nameof(HitTheWater), _duration);
    }

    private void HitTheWater()
    {
        CannonBallPool.Instance.ReturnPoolItem(this);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.gameObject.layer & (1 << _hitLayer)) != 0)
        {
            if(TryGetComponent<HealthBase>(out HealthBase hp))
            {
                hp.TakeDamage(_damage);
            }
            _pool.ReturnPoolItem(this);
        }
    }
}
