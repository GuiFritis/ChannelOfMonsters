using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeModule : MonoBehaviour
{
    [SerializeField] private Player _player;
    public SOInt coins;

    public void UpgradeResistence(UpgradeButton button)
    {
        if(!_player.UpgradeResistence())
        {
            button.MaxLevel();
            button.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "Max Level";
        }
    }

    public void UpgradeDamage(UpgradeButton button)
    {
        if(!_player.UpgradeDamage())
        {
            button.MaxLevel();
            button.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "Max Level";
        }
    }

    public void UpgradeSpeed(UpgradeButton button)
    {
        if(!_player.UpgradeSpeed())
        {
            button.MaxLevel();
            button.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "Max Level";
        }
    }

    public void UpgradeTurnSpeed(UpgradeButton button)
    {
        if(!_player.UpgradeTurnSpeed())
        {
            button.MaxLevel();
            button.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "Max Level";
        }
    }

    public void UpgradeShootSpeed(UpgradeButton button)
    {
        if(!_player.UpgradeShootSpeed())
        {
            button.MaxLevel();
            button.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "Max Level";
        }
    }

    public void UpgradeReloadSpeed(UpgradeButton button)
    {
        if(!_player.UpgradeReloadSpeed())
        {
            button.MaxLevel();
            button.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "Max Level";
        }
    }

    public void BuyFrontCannon(UpgradeButton button)
    {
        if(!_player.BuyFrontCannon())
        {
            button.MaxLevel();
            button.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "Max Cannons";
        }
    }

    public void BuyLeftCannon(UpgradeButton button)
    {
        if(!_player.BuyLeftCannon())
        {
            button.MaxLevel();
            button.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "Max Cannons";
        }
    }

    public void BuyRightCannon(UpgradeButton button)
    {
        if(!_player.BuyRightCannon())
        {
            button.MaxLevel();
            button.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "Max Cannons";
        }
    }
}
