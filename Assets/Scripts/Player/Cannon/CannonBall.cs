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
        if ((_hitLayer.value & (1 << other.gameObject.layer)) > 0)
        {
            if(other.gameObject.TryGetComponent<HealthBase>(out HealthBase hp))
            {
                hp.TakeDamage(_damage);
            }
            CannonBallPool.Instance.ReturnPoolItem(this);
        }
    }
}
