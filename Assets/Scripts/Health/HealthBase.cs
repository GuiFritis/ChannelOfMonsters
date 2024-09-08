using System;
using System.Collections;
using UnityEngine;

public class HealthBase : MonoBehaviour
{
    public Action<HealthBase, float> OnDamage;
    public Action<HealthBase> OnDeath;
    public bool destroyOnDeath = false;
    public float baseHealth = 10f;
    public float damageMultiplier = 1f;
    [SerializeField]
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
        _curHealth -= GetCalculatedDamage(damage);
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

    public void ResetResistance()
    {
        damageMultiplier = 1f;
    }

    private float GetCalculatedDamage(float damage)
    {
        return damage * damageMultiplier;
    }

    public void IncreaseResistance(float damageResistance, float duration)
    {
        StartCoroutine(IncreaseResistanceCoroutine(damageResistance, duration));
    }

    private IEnumerator IncreaseResistanceCoroutine(float damageResistance, float duration)
    {
        damageMultiplier += damageResistance;
        yield return new WaitForSeconds(duration);
        damageMultiplier -= damageResistance;
    }
}