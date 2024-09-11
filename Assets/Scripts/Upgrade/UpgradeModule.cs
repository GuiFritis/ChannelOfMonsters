using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeModule : MonoBehaviour
{
    [SerializeField] private Player _player;
    public SOInt coins;

    public void UpgradeResistence(Button button)
    {
        if(!_player.UpgradeResistence())
        {
            button.interactable = false;
            button.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "Max Level";
        }
    }

    public void UpgradeDamage(Button button)
    {
        if(!_player.UpgradeDamage())
        {
            button.interactable = false;
            button.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "Max Level";
        }
    }

    public void UpgradeSpeed(Button button)
    {
        if(!_player.UpgradeSpeed())
        {
            button.interactable = false;
            button.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "Max Level";
        }
    }

    public void UpgradeTurnSpeed(Button button)
    {
        if(!_player.UpgradeTurnSpeed())
        {
            button.interactable = false;
            button.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "Max Level";
        }
    }

    public void UpgradeShootSpeed(Button button)
    {
        if(!_player.UpgradeShootSpeed())
        {
            button.interactable = false;
            button.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "Max Level";
        }
    }

    public void UpgradeReloadSpeed(Button button)
    {
        if(!_player.UpgradeReloadSpeed())
        {
            button.interactable = false;
            button.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "Max Level";
        }
    }

    public void BuyFrontCannon(Button button)
    {
        if(!_player.BuyFrontCannon())
        {
            button.interactable = false;
            button.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "Max Cannons";
        }
    }

    public void BuyLeftCannon(Button button)
    {
        if(!_player.BuyLeftCannon())
        {
            button.interactable = false;
            button.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "Max Cannons";
        }
    }

    public void BuyRightCannon(Button button)
    {
        if(!_player.BuyRightCannon())
        {
            button.interactable = false;
            button.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "Max Cannons";
        }
    }
}
