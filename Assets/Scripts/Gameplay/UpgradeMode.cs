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
    [SerializeField] private GameObject _upgradeMenu;
    private List<ImageColor> _buttonImages;
    private List<TextMeshProUGUI> _buttonTexts;
    public System.Action OnEndUpgradeTime;

    private void Start()
    {
        _buttonImages = new();
        foreach(Image img in _upgradeMenu.GetComponentsInChildren<Image>())
        {
            _buttonImages.Add(new ImageColor{
                image = img,
                color = img.color
            });
            img.color = Color.clear;
        }
        _buttonTexts = _upgradeMenu.GetComponentsInChildren<TextMeshProUGUI>().ToList();
        _buttonTexts.ForEach(t => t.color = Color.clear);
    }

    public void EnterUpgradeMode()
    {
        _upgradeCamera.enabled = true;
        _sail.DOColor(Color.clear, 1f);
        _upgradeMenu.SetActive(true);
        _buttonImages.ForEach(i => i.image.DOColor(i.color, 1f));
        _buttonTexts.ForEach(t => t.DOColor(Color.black, 1f));
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
        _buttonImages.ForEach(i => i.image.DOColor(Color.clear, 1f));
        _buttonTexts.ForEach(t => t.DOColor(Color.clear, 1f));
        _sail.DOColor(Color.white, 1f).OnComplete(() => _upgradeMenu.SetActive(false));
        OnEndUpgradeTime?.Invoke();
    }
}

public struct ImageColor
{
    public Image image;
    public Color color;
}