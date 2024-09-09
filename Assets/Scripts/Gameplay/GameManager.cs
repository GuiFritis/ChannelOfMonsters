using UnityEngine;
using Utils.Singleton;

public class GameManager : Singleton<GameManager>
{
    public SOInt coinsSO;
    public SOInt currentWave;
    [SerializeField] private int _startCoins = 100;
    [SerializeField] private WaveSpawner _waveSpawner;
    [SerializeField] private UpgradeMode _upgradeMode;

    protected override void Awake()
    {
        base.Awake();
        currentWave.Value = -1;
        coinsSO.Value = _startCoins;
    }

    private void Start()
    {
        _waveSpawner.OnWaveEnded += EnterUpgradeMode;
        _upgradeMode.OnEndUpgradeTime += ExitUpgradeMode;
    }

    private void EnterUpgradeMode()
    {
        _upgradeMode.EnterUpgradeMode();
    }

    private void ExitUpgradeMode()
    {
        _waveSpawner.StartNextWave();
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }
}
