using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMode : MonoBehaviour
{    
    [SerializeField] private CinemachineVirtualCamera _upgradeCamera;
    [SerializeField] private float _upgradeModeDuration;
    [SerializeField] private SpriteRenderer _sail;
    [SerializeField] private MusicPlayer _musicPlayer;
    [SerializeField] private List<UpgradeButton> _upgradeBtns = new();
    public System.Action OnEndUpgradeTime;

    private void Awake()
    {
        if(_upgradeBtns == null || _upgradeBtns.Count == 0)
        {
            _upgradeBtns = GetComponentsInChildren<UpgradeButton>().ToList();
        }
        _musicPlayer.enabled = false;
    }

    public void EnterUpgradeMode()
    {
        _musicPlayer.enabled = true;
        _upgradeCamera.enabled = true;
        _sail.DOColor(Color.clear, 1f);
        gameObject.SetActive(true);
        _upgradeBtns.ForEach(i => i.ShowButton());
        StartCoroutine(CooldownUpgradeMode());
    }

    private IEnumerator CooldownUpgradeMode()
    {
        float _upgradeModeCooldown = 0f;
        while(_upgradeModeCooldown < _upgradeModeDuration)
        {
            _upgradeModeCooldown += .1f;
            yield return new WaitForSeconds(.1f);
        }
        ExitUpgradeMode();
    }

    public void ExitUpgradeMode()
    {
        StopAllCoroutines();
        _musicPlayer.enabled = false;
        _upgradeCamera.enabled = false;
        _upgradeBtns.ForEach(i => i.HideButton());
        _sail.DOColor(Color.white, 1f).OnComplete(() => {
            gameObject.SetActive(false);
        });
        OnEndUpgradeTime?.Invoke();
    }
}

public struct ImageColor
{
    public Image image;
    public Color color;
}