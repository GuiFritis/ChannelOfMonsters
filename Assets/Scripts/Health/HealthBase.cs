using System;
using UnityEngine;

public class HealthBase : MonoBehaviour
{
    public Action<HealthBase, float> OnDamage;
    public Action<HealthBase> OnDeath;
    public bool destroyOnDeath = false;
    public float baseHealth = 10f;
    private float _curHealth;
    public float CurrentHealth {get => _curHealth;}
    private bool dead = false;

    public void ResetLife()
    {
        ResetLife(baseHealth);
    }

    public void ResetLife(float life)
    {
        _curHealth = life;
        dead = false;
    }

    protected virtual void Death()
    {
        dead = true;
        OnDeath?.Invoke(this);
        if(destroyOnDeath)
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(float damage)
    {
        _curHealth -= damage;
        OnDamage?.Invoke(this, damage);
        if(_curHealth <= 0 && !dead)
        {
            Death();
        }
    }

    public void Die()
    {
        _curHealth = 0;
        Death();
    }

    public void IncreaseMaxHealth(float increaseAmount)
    {
        baseHealth += increaseAmount;
        TakeDamage(-increaseAmount);
    }

    private void OnDestroy()
    {
        OnDamage = null;
        OnDeath = null;
    }
}
