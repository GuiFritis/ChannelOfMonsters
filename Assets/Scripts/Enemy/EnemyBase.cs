using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;
using Utils.StateMachine;

namespace Enemies{
    [RequireComponent(typeof(HealthBase))]
    public class EnemyBase : MonoBehaviour
    { 
        [SerializeField] private HealthBase _health;
        public HealthBase Health {get { return _health;}}
        [SerializeField] protected float _speed = .5f;
        [SerializeField] private float _damage = 2;
        [SerializeField] private int _coins = 2;
        public int Coins {get {return _coins;}}
        public SOInt soCoins;
        protected Player _player;
        [SerializeField] protected float _maxDistanceToPlayer = 5f;
        private List<SpriteRenderer> _sprites;
        protected StateMachineBase<EnemyStates> _stm;
        private ObjectPool<EnemyBase> _objectPool;
        public ObjectPool<EnemyBase> ObjectPool {set => _objectPool = value;}
        private float _baseHealth;
        private float _baseDamage;
        private float _baseSpeed;
        private int _level = 0;

        void OnValidate()
        {
            if(_health != null)
            {
                _health = GetComponent<HealthBase>();
            }
            if(_sprites != null)
            {
                _sprites = GetComponentsInChildren<SpriteRenderer>().ToList();
            }
        }

        protected virtual void Awake()
        {
            InitStateMachine();
            _health.OnDeath += OnDeath;
            _baseDamage = _damage;
            _baseSpeed = _speed;
            _baseHealth = _health.baseHealth;
        }

        private void InitStateMachine()
        {
            _stm = new();
            _stm.RegisterStates(EnemyStates.MOVING, new EnemyStateMoving(this));
            _stm.RegisterStates(EnemyStates.STUNNED, new EnemyStateStunned(this));
            _stm.RegisterStates(EnemyStates.DEAD, new EnemyStateDead(this));
        }

        protected virtual void Update()
        {
            _stm.Update();
        }

        public void Init(Player player, Vector3 position, int level)
        {
            this._player = player;
            transform.position = position;
            _level = level;
            _damage = _baseDamage + _level * 3;
            _speed = _baseSpeed + _level * .1f;
            _health.baseHealth = _baseHealth + _level * .2f;
            _health.ResetLife();
            _stm.SwitchState(EnemyStates.MOVING);
        }

        protected void FlipSprites()
        {
            _sprites?.ForEach(s => s.flipX = !s.flipX);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if(other.gameObject.Equals(_player.gameObject))
            {
                _player.Health.TakeDamage(_damage);
                _player.Hit(other.GetContact(0).point, _damage);
                _stm.SwitchState(EnemyStates.STUNNED, 1f);
            }
        }

        #region STATE_MOVING
        public virtual void StartMoving(){}

        public virtual void Move()
        {
            if(Vector2.Distance(transform.position, _player.transform.position) > _maxDistanceToPlayer)
            {
                GameManager.Instance.WaveSpawner.RespawnEnemy(this);
            }
        }

        public virtual void StopMoving(){}
        #endregion

        #region STUNNED
        public void Stunned(float duration)
        {
            StartCoroutine(StunCountdown(duration));
        }

        private IEnumerator StunCountdown(float duration)
        {
            yield return new WaitForSeconds(duration);
            _stm.SwitchState(EnemyStates.MOVING);
        }
        #endregion

        #region DEATH
        private void OnDeath(HealthBase hp)
        {
            soCoins.Value += _coins;
            _objectPool.Release(this);
            StopAllCoroutines();
            _stm.SwitchState(EnemyStates.DEAD);
        }
        #endregion

        public enum EnemyStates
        {
            MOVING,
            STUNNED,
            DEAD
        }

        protected void OnDrawGizmosSelected()
        {
            Gizmos.color = new Color(1f, .6f, 0f);
            Gizmos.DrawWireSphere(transform.position, _maxDistanceToPlayer);
        }
    }
}
