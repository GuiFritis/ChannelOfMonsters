using UnityEngine;
using DG.Tweening;
using Utils;
using UnityEngine.UI;

public class HealthBar_UI : MonoBehaviour, IPoolItem
{
    [SerializeField] private Image _image;
    private HealthBase _health;

    public void SetHealth(HealthBase hp)
    {
        _health = hp;
        _health.OnDamage += OnDamageTaken;
    }

    public void GetFromPool()
    {
        gameObject.SetActive(false);
    }

    public void ReturnToPool()
    {
        _health.OnDamage -= OnDamageTaken;
        gameObject.SetActive(false);
        _health = null;
    }

    private void OnDamageTaken(HealthBase hp, float damage)
    {
        gameObject.SetActive(true);
        UpdateBar();
    }

    private void UpdateBar()
    {        
        _image.DOKill();
        _image.DOFillAmount(_health.CurrentHealth/_health.baseHealth, .25f).SetEase(Ease.OutCirc);
    }
}
