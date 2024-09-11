using UnityEngine;
using UnityEngine.UI;

public class UpgradeMoneyCheck : MonoBehaviour
{
    public SOInt soCoins;
    [SerializeField] private int _upgradeValue;
    [SerializeField] private Button _button;

    private void Start()
    {
        soCoins.OnValueChanged += CheckUpgradeValue;
    }

    private void CheckUpgradeValue(int coins)
    {
        _button.interactable = _upgradeValue < coins;
    }

    public void Buy()
    {
        soCoins.Value -= _upgradeValue;
    }
}
