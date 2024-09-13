using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ReloadCannonDisplay : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private CannonGroup _cannonGroup;
    [SerializeField] private AudioClip _sfxReloaded;

    private void Start()
    {
        _cannonGroup.ReloadTick += ReloadTimeChanged;
        _icon.color = Color.clear;
    }

    private void ReloadTimeChanged(float percentage)
    {
        _icon.fillAmount = percentage;
        if(percentage >= 1)
        {
            _icon.DOKill();
            _icon.DOFade(0, .9f);
            SFX_Pool.Instance.Play(_sfxReloaded);
        }
        if(percentage < .1f)
        {
            _icon.DOKill();
            _icon.DOFade(.5f, .6f);
        }
    }
}
