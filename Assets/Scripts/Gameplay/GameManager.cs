using System.Collections.Generic;
using UnityEngine;
using Utils;
using Utils.Singleton;

public class GameManager : Singleton<GameManager>
{
    public SOInt coinsSO;
    public SOInt currentWave;
    [SerializeField] private int _startCoins = 100;
    [SerializeField] private WaveSpawner _waveSpawner;
    [SerializeField] private UpgradeMode _upgradeMode;
    [SerializeField] private Player _player;
    [SerializeField] private List<Transform> _spawnPoints = new();
    public List<Transform> SpawnPoints { get { return _spawnPoints; } }

    private void OnValidate()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    protected override void Awake()
    {
        base.Awake();
        currentWave.Value = -1;
        coinsSO.Value = _startCoins;
        _player.transform.position = _spawnPoints.GetRandom().position;
    }

    private void Start()
    {
        _waveSpawner.OnWaveEnded += EnterUpgradeMode;
        _upgradeMode.OnEndUpgradeTime += ExitUpgradeMode;
        EnterUpgradeMode();
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
