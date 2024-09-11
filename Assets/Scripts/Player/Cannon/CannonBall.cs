using System.Collections;
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
    [SerializeField] private Animator _vfx;

    private void Start()
    {
        _vfx = Instantiate(_vfx);
        _vfx.gameObject.SetActive(false);
    }

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
        _speed = _damage = 0f;
        gameObject.SetActive(false);
    }

    public void Shoot(float damage, float speed)
    {
        _damage = damage;
        _speed = speed;
        StartCoroutine(HitTheWater());
    }

    private IEnumerator HitTheWater()
    {
        yield return new WaitForSeconds(_duration);
        CannonBallPool.Instance.ReturnPoolItem(this);
        if(Physics2D.OverlapPoint(transform.position) != null)
        {
            PlayExplosion();
        }
        else
        {
            _vfx.transform.position = transform.position;
            _vfx.gameObject.SetActive(true);
            _vfx.SetTrigger("WaterSplash");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((_hitLayer.value & (1 << other.gameObject.layer)) > 0)
        {
            if(other.gameObject.TryGetComponent<HealthBase>(out HealthBase hp))
            {
                hp.TakeDamage(_damage);
            }
            StopAllCoroutines();
            PlayExplosion();
            CannonBallPool.Instance.ReturnPoolItem(this);
        }
    }

    private void PlayExplosion()
    {
        _vfx.transform.position = transform.position;
        _vfx.gameObject.SetActive(true);
        _vfx.SetTrigger("Explode");
    }
}
