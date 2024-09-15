using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    public SOInt soCoins;
    [SerializeField] private int _upgradeValue;
    [SerializeField] private Button _button;
    [SerializeField] private Image _priceTag;

    private void Start()
    {
        if(gameObject != null)
        {
            soCoins.OnValueChanged += CheckUpgradeValue;
        }
    }

    public void MaxLevel()
    {
        soCoins.OnValueChanged -= CheckUpgradeValue;
        DisabledButton();
    }

    private void CheckUpgradeValue(int coins)
    {
        if(_upgradeValue <= coins)
        {
            EnableButton();
        }
        else
        {
            DisabledButton();
        }
    }

    public void Buy()
    {
        soCoins.Value -= _upgradeValue;
    }

    
    private void DisabledButton()
    {
        _button.interactable = false;
        _priceTag.color = Color.white * .5f;
    }

    private void EnableButton()
    {
        if(_upgradeValue <= soCoins.Value)
        {
            _button.interactable = true;
            _priceTag.color = Color.white;
        }
    }

    public void ShowButton()
    {
        CheckUpgradeValue(soCoins.Value);
        gameObject.SetActive(true);
        _button.DOKill();
        _priceTag.DOKill();
        _button.image.DOFade(0, .3f).From(true);
        _priceTag.DOFade(0, .3f).From(true);
    }

    public void HideButton()
    {
        Color btnColor = _button.image.color;
        Color priceTagColor = _priceTag.color;        
        _button.DOKill();
        _priceTag.DOKill();
        _button.image.DOFade(0, .3f).OnComplete(
            () => {
                gameObject.SetActive(false);
                _button.image.color = btnColor;
                _priceTag.color = priceTagColor;
            }
        );
        _priceTag.DOFade(0, .3f);
    }

    private void OnDestroy()
    {
        soCoins.OnValueChanged -= CheckUpgradeValue;
    }
}
