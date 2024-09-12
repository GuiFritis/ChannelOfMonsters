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
        soCoins.OnValueChanged += CheckUpgradeValue;
        CheckUpgradeValue(soCoins.Value);
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
        _button.interactable = true;
        _priceTag.color = Color.white;
    }
}
