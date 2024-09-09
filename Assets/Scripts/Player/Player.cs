using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private HealthBase _health;
    [SerializeField] private Rigidbody2D _rigdbody;
    public HealthBase Health{get{return _health;}}
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
        if(_rigdbody != null)
        {
            _rigdbody = gameObject.GetComponent<Rigidbody2D>();
        }
    }

    private void Awake()
    {
        SetInputs();
        SetupCannons();
    }

    private void SetupCannons()
    {        
        _frontCannons.UpgradeReloadSpeed(_reloadSpeed);
        _leftCannons.UpgradeReloadSpeed(_reloadSpeed);
        _rightCannons.UpgradeReloadSpeed(_reloadSpeed);
        _frontCannons.BuyCannon(_damage, _shootSpeed);
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
    public void UpgradeSpeed()
    {
        _moveSpeed += _moveSpeedUpgrade;
    }

    public void UpgradeTurnSpeed()
    {
        _turnSpeed += _turnSpeedUpgrade;
    }

    public void UpgradeDamage()
    {
        _damage += _damageUpgrade;
        _frontCannons.UpgradeDamage(_damage);
        _leftCannons.UpgradeDamage(_damage);
        _rightCannons.UpgradeDamage(_damage);
    }

    public void UpgradeShootSpeed()
    {
        _shootSpeed += _shootSpeedUpgrade;
        _frontCannons.UpgradeShootSpeed(_shootSpeed);
        _leftCannons.UpgradeShootSpeed(_shootSpeed);
        _rightCannons.UpgradeShootSpeed(_shootSpeed);
    }

    public void UpgradeReloadSpeed()
    {
        _reloadSpeed -= _ReloadSpeedUpgrade;
        _frontCannons.UpgradeReloadSpeed(_reloadSpeed);
        _leftCannons.UpgradeReloadSpeed(_reloadSpeed);
        _rightCannons.UpgradeReloadSpeed(_reloadSpeed);
    }
    #endregion
}
