using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Utils;
using Utils.StateMachine;

namespace Enemies{
    [RequireComponent(typeof(HealthBase))]
    public class EnemyBase : MonoBehaviour
    { 
        public HealthBase health;
        [SerializeField] private float _speed = .5f;
        [SerializeField] private float _damage = 2;
        [SerializeField] private int _coins = 2;
        public int Coins {get {return _coins;}}
        private Player player;
        [SerializeField] private SpriteRenderer _sprite;
        protected StateMachineBase<EnemyStates> _stm;
        private ObjectPool<EnemyBase> _objectPool;
        public ObjectPool<EnemyBase> ObjectPool {set => _objectPool = value;}

        void OnValidate()
        {
            health = GetComponent<HealthBase>();
            _sprite = GetComponent<SpriteRenderer>();
        }

        protected virtual void Awake()
        {
            InitStateMachine();
        }

        private void InitStateMachine()
        {
            _stm = new();
            _stm.RegisterStates(EnemyStates.MOVING, new EnemyStateMoving(this));
            _stm.RegisterStates(EnemyStates.STUNNED, new EnemyStateStunned(this));
            _stm.RegisterStates(EnemyStates.DEAD, new EnemyStateDead(this));
            _stm.SwitchState(EnemyStates.MOVING);
        }

        protected virtual void Start()
        {
            health.ResetLife();
            health.OnDeath += OnDeath;
        }

        protected virtual void Update()
        {
            _stm.Update();
        }

        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            if(other.gameObject.Equals(player.gameObject))
            {
                player.Health.TakeDamage(_damage);
                health.Die();
            }
        }

        #region STATE_MOVING
        public virtual void Move()
        {
            transform.position += _speed * Time.deltaTime * (player.transform.position - transform.position).normalized;
        }
        #endregion

        #region DEATH
        private void OnDeath(HealthBase hp)
        {
            _objectPool.Release(this);
            _stm.SwitchState(EnemyStates.DEAD);
        }
        #endregion

        public enum EnemyStates
        {
            MOVING,
            STUNNED,
            DEAD
        }
    }
}
