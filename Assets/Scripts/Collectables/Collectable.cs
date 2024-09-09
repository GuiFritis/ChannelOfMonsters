using UnityEngine;
using Utils;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
public class Collectable : MonoBehaviour, IPoolItem
{
    public SOInt _coins;
    public SOInt _currentWave;
    [SerializeField] private int _minValue;
    [SerializeField] private int _maxValue;
    [Space]
    [SerializeField] private float _minSpeed;
    [SerializeField] private float _maxSpeed;
    private Vector2 _direction;
    private float _speed;
    private CollectablesPool _pool;
    public CollectablesPool Pool {get { return _pool; } }

    private void Awake()
    {
        _direction = new Vector2();
    }

    public void Init()
    {
        _direction.x = Random.Range(-1f, 1f);
        _direction.y = Random.Range(-1f, 1f);
        _speed = Random.Range(_minSpeed, _maxSpeed);
    }

    private void Update()
    {
        transform.Translate(_direction * Time.deltaTime * _speed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            _coins.Value += Random.Range(_minValue, _maxValue) * (_currentWave.Value + 1);
            _pool.ReturnPoolItem(this);
        }
        else
        {
            Init();
        }
    }

    public void GetFromPool()
    {
        Init();
        gameObject.SetActive(true);
    }

    public void ReturnToPool()
    {
        gameObject.SetActive(false);
    }
}
