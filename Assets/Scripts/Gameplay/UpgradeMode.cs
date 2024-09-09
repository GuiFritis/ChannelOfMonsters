using System.Collections;
using Cinemachine;
using DG.Tweening;
using UnityEngine;

public class UpgradeMode : MonoBehaviour
{    
    [SerializeField] private CinemachineVirtualCamera _upgradeCamera;
    [SerializeField] private float _upgradeModeDuration;
    [SerializeField] private SpriteRenderer _sail;
    [SerializeField] private GameObject _upgradeMenu;
    public System.Action OnEndUpgradeTime;

    public void EnterUpgradeMode()
    {
        _upgradeCamera.enabled = true;
        _sail.DOColor(Color.clear, 1f);
        _upgradeMenu.SetActive(true);
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

    private void ExitUpgradeMode()
    {
        _upgradeCamera.enabled = false;
        _sail.DOColor(Color.white, 1f);
        _upgradeMenu.SetActive(false);
        OnEndUpgradeTime?.Invoke();
    }
}
