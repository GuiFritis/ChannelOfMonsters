using UnityEngine;
using UnityEngine.UI;

public class UpgradeMoneyCheck : MonoBehaviour
{
    public SOInt _coins;
    [SerializeField] private int _upgradeValue;
    [SerializeField] private Button _button;

    private void Start()
    {
        _coins.OnValueChanged += CheckUpgradeValue;
    }

    private void CheckUpgradeValue(int coins)
    {
        _button.interactable = _upgradeValue < coins;
    }
}
