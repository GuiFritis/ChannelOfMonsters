using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CannonGroup : MonoBehaviour
{
    [SerializeField] private Cannon PFB_Cannon;
    [SerializeField] private Transform _cannonPosition;
    private List<Cannon> _cannons = new();
    [SerializeField] private float _cannonsDistance;
    [SerializeField] private float _maxCannons;
    [SerializeField] private float _reloadSpeed = 3f;
    private float _reloadHelper = 0f;

    private void Start()
    {
        _reloadHelper = _reloadSpeed;
    }

    public void Shoot()
    {
        if(_cannons.Count > 0 && _reloadHelper >= _reloadSpeed)
        {
            _cannons.ForEach(c => c.Shoot());
            _reloadHelper = 0f;
            StartCoroutine(ReloadCoroutine());
        }
    }

    private IEnumerator ReloadCoroutine()
    {
        while(_reloadHelper < _reloadSpeed)
        {
            yield return new WaitForSeconds(.1f);
            _reloadHelper += .1f;
        }
    }

    public void BuyCannon(float damage, float shootSpeed)
    {
        if(_cannons.Count < _maxCannons)
        {
            if(_cannons.Count == 0)
            {
                _cannons.Add(Instantiate(
                    PFB_Cannon, _cannonPosition.position, _cannonPosition.rotation, transform
                ));
            }
            else
            {
                _cannons.ForEach(c => c.transform.position += transform.up * _cannonsDistance/2);
                _cannons.Add(Instantiate(
                    PFB_Cannon, _cannons[^1].transform.position - (transform.up * _cannonsDistance), _cannonPosition.rotation, transform
                ));
            }
            _cannons[^1].UpgradeDamage(damage);
            _cannons[^1].UpgradeShootSpeed(shootSpeed);
        }
    }

    public void UpgradeDamage(float damage)
    {
        _cannons.ForEach(c => UpgradeDamage(damage));
    }

    public void UpgradeShootSpeed(float shootSpeed)
    {
        _cannons.ForEach((c) => UpgradeShootSpeed(shootSpeed));
    }

    public void UpgradeReloadSpeed(float reloadSpeed)
    {
        _reloadSpeed = reloadSpeed;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, .1f);
        Gizmos.DrawWireSphere(transform.position + (transform.up * _cannonsDistance), .1f);
    }
}
