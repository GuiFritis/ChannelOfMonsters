using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] private CannonBall _cannonBallPFB;
    private CannonBall _shot;
    private float _shootSpeed;
    private float _damage;

    public void Shoot()
    {
        if(CannonBallPool.Instance != null)
        {
            CannonBallPool.Instance.GetPoolItem(out _shot);
            _shot.transform.position = transform.position;
            _shot.transform.rotation = transform.rotation;
            _shot.Shoot(_damage, _shootSpeed);
        }
    }

    public void UpgradeDamage(float damage)
    {
        _damage = damage;
    }

    public void UpgradeShootSpeed(float shootSpeed)
    {
        _shootSpeed = shootSpeed;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(transform.position, transform.position + transform.right);
    }
}
