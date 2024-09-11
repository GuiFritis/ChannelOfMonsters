using UnityEngine;

public class EndUpgrades : MonoBehaviour
{
    [SerializeField] private UpgradeMode _upgradeMode;
    public void EndUpgradeMode()
    {
        _upgradeMode.ExitUpgradeMode();
    }
}
