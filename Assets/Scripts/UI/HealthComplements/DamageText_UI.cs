using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;
using DG.Tweening;
using TMPro;
using UnityEngine.Pool;

public class DamageText_UI : MonoBehaviour, IPoolItem
{
    [SerializeField] private TextMeshProUGUI _textMesh;
    private ObjectPool<DamageText_UI> _pool;
    public ObjectPool<DamageText_UI> Pool { get => _pool; set => _pool = value; }
    public TextMeshProUGUI TextMesh { get => _textMesh; set => _textMesh = value; }

    public void GetFromPool()
    {
        gameObject.SetActive(true);
    }

    public void ReturnToPool()
    {
        gameObject.SetActive(false);
    }

    public void Play()
    {
        _textMesh.DOColor(Color.clear, .2f).SetEase(Ease.OutCirc).From(true);
        transform.DOMoveY(30f, .5f).SetEase(Ease.OutCirc).SetRelative(true);
        transform.DOMoveX(Random.Range(-1, 2) * 10f, .5f).SetEase(Ease.OutCirc).SetRelative(true);
        _textMesh.DOColor(Color.clear, .35f).SetDelay(.25f).SetEase(Ease.InQuad).OnComplete(
            () => _pool.Release(this)
        );
    }
}
