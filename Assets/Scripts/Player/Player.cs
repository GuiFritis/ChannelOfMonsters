using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Rigidbody2D _rigdbody;
    [SerializeField] private Collider2D _collider;
    [SerializeField] private SpriteRenderer[] _sprites;
    [Header("Hit")]
    [SerializeField] private float _imuneTime;
    [SerializeField] private LayerMask _imuneLayers;
    [Header("Health")]
    [SerializeField] private HealthBase _health;
    public HealthBase Health{get{return _health;}}
    [SerializeField] private float _resistenceUpgrade;
    [SerializeField] private float _maxResistence;
    [SerializeField] private AudioClip _sfxSinking;
    [Header("Cannons")]
    [SerializeField] private CannonGroup _frontCannons;
    [SerializeField] private CannonGroup _leftCannons;
    [SerializeField] private CannonGroup _rightCannons;
    [Header("Move Speed")]
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _moveSpeedUpgrade;
    [SerializeField] private float _maxMoveSpeed;
    [Header("Turn Speed")]
    [SerializeField] private float _turnSpeed;
    [SerializeField] private float _turnSpeedUpgrade;
    [SerializeField] private float _maxTurnSpeed;
    [Header("Damage")]
    [SerializeField] private float _damage;
    [SerializeField] private float _damageUpgrade;
    [SerializeField] private float _maxDamage;
    [Header("Shoot Speed")]
    [SerializeField] private float _shootSpeed;
    [SerializeField] private float _shootSpeedUpgrade;
    [SerializeField] private float _maxShootSpeed;
    
    [Header("Reload Speed")]
    [SerializeField] private float _reloadSpeed;
    [SerializeField] private float _ReloadSpeedUpgrade;
    [SerializeField] private float _minReloadSpeed;

    private Gameplay _inputs;
    private bool _isMoving = false;
    private float _turnDirection = 0f;

    private void OnValidate()
    {
        if(_health == null)
        {
            _health = gameObject.GetComponent<HealthBase>();
        }
        if(_rigdbody == null)
        {
            _rigdbody = gameObject.GetComponent<Rigidbody2D>();
        }
        if(_collider == null)
        {
            _collider = gameObject.GetComponent<Collider2D>();
        }
        if(_sprites == null)
        {
            _sprites = GetComponentsInChildren<SpriteRenderer>();
        }
    }

    private void Awake()
    {
        SetInputs();
        SetupCannons();
        _health.OnDeath += OnDeath;
    }

    private void SetupCannons()
    {        
        _frontCannons.UpgradeReloadSpeed(_reloadSpeed);
        _leftCannons.UpgradeReloadSpeed(_reloadSpeed);
        _rightCannons.UpgradeReloadSpeed(_reloadSpeed);
        _frontCannons.BuyCannon(_damage, _shootSpeed);
        _leftCannons.BuyCannon(_damage, _shootSpeed);
        _rightCannons.BuyCannon(_damage, _shootSpeed);
    }

    private void SetInputs()
    {
        _inputs = new Gameplay();
        _inputs.Enable();
        _inputs.Ship.MoveForward.started += ctx => MoveForward(true);
        _inputs.Ship.MoveForward.canceled += ctx => MoveForward(false);
        _inputs.Ship.TurnShip.performed += ctx => TurnShip(ctx.ReadValue<float>());
        _inputs.Ship.TurnShip.canceled += ctx => TurnShip(0);
        _inputs.Ship.ShootForward.performed += ctx => ShootForward();
        _inputs.Ship.ShootLeft.performed += ctx => ShootLeft();
        _inputs.Ship.ShootRight.performed += ctx => ShootRight();
    }

    public void EnableControls()
    {
        _inputs.Enable();
    }

    public void DisableControls()
    {
        _inputs.Disable();
    }

    private void Start()
    {
        _health.ResetLife();
    }

    private void Update()
    {
        if(_turnDirection != 0)
        {
            transform.Rotate(Vector3.forward, _turnDirection * _turnSpeed * Time.deltaTime);
        }
    }

    private void FixedUpdate()
    {
        if(_isMoving)
        {
            _rigdbody.AddForce(_moveSpeed * Time.fixedDeltaTime * transform.up, ForceMode2D.Force);
        }
        
    }

    private void OnDeath(HealthBase hp)
    {
        SFX_Pool.Instance.Play(_sfxSinking);
    }

    #region HIT
    public void Hit(Vector3 position, float damage)
    {
        _collider.excludeLayers = _imuneLayers;
        _rigdbody.AddForce(.4f * damage * (transform.position - position), ForceMode2D.Impulse);
        foreach (var s in _sprites)
        {
            s.DOFade(.3f, _imuneTime/6).SetLoops(6, LoopType.Yoyo);
        }
        StartCoroutine(ImuneTime());
    }

    private IEnumerator ImuneTime()
    {
        yield return new WaitForSeconds(_imuneTime);
        _collider.excludeLayers = 0;
    }
    #endregion

    #region MOVE
    private void MoveForward(bool isMoving)
    {
        _isMoving = isMoving;
    }

    private void TurnShip(float turnDirection)
    {
        _turnDirection = turnDirection;
    }
    #endregion

    #region CANNONS
    private void ShootForward()
    {
        _frontCannons.Shoot();
    }

    private void ShootLeft()
    {
        _leftCannons.Shoot();
    }

    private void ShootRight()
    {
        _rightCannons.Shoot();
    }
    #endregion

    #region UPGRADES
    public bool UpgradeResistence()
    {
        _health.IncreaseMaxHealth(_resistenceUpgrade);
        return _health.baseHealth < _maxResistence;
    }

    public bool UpgradeSpeed()
    {
        _moveSpeed += _moveSpeedUpgrade;
        return _moveSpeed < _maxMoveSpeed;
    }

    public bool UpgradeTurnSpeed()
    {
        _turnSpeed += _turnSpeedUpgrade;
        return _turnSpeed < _maxTurnSpeed;
    }

    public bool UpgradeDamage()
    {
        _damage += _damageUpgrade;
        _frontCannons.UpgradeDamage(_damage);
        _leftCannons.UpgradeDamage(_damage);
        _rightCannons.UpgradeDamage(_damage);
        return _damage < _maxDamage;
    }

    public bool UpgradeShootSpeed()
    {
        _shootSpeed += _shootSpeedUpgrade;
        _frontCannons.UpgradeShootSpeed(_shootSpeed);
        _leftCannons.UpgradeShootSpeed(_shootSpeed);
        _rightCannons.UpgradeShootSpeed(_shootSpeed);
        return _shootSpeed < _maxShootSpeed;
    }

    public bool UpgradeReloadSpeed()
    {
        _reloadSpeed -= _ReloadSpeedUpgrade;
        _frontCannons.UpgradeReloadSpeed(_reloadSpeed);
        _leftCannons.UpgradeReloadSpeed(_reloadSpeed);
        _rightCannons.UpgradeReloadSpeed(_reloadSpeed);
        return _reloadSpeed > _minReloadSpeed;
    }

    public bool BuyFrontCannon()
    {
        _frontCannons.BuyCannon(_damage, _shootSpeed);
        return _frontCannons.CanBuyMoreCannons();
    }

    public bool BuyLeftCannon()
    {
        _leftCannons.BuyCannon(_damage, _shootSpeed);
        return _leftCannons.CanBuyMoreCannons();
    }

    public bool BuyRightCannon()
    {
        _rightCannons.BuyCannon(_damage, _shootSpeed);
        return _rightCannons.CanBuyMoreCannons();
    }
    #endregion
}
