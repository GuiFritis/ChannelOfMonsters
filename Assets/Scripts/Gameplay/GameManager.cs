using System.Collections.Generic;
using UnityEngine;
using Utils;
using Utils.Singleton;

public class GameManager : Singleton<GameManager>
{
    public SOInt coinsSO;
    public SOInt currentWave;
    [SerializeField] private int _startCoins = 100;
    [SerializeField] private int _wavesCount = 10;
    [SerializeField] private WaveSpawner _waveSpawner;
    public WaveSpawner WaveSpawner { get { return _waveSpawner;}}
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
        _waveSpawner.OnWaveEnded += WaveEnded;
        _upgradeMode.OnEndUpgradeTime += ExitUpgradeMode;
        _player.Health.OnDeath += hp => GameOver();
        WaveEnded();
    }

    private void WaveEnded()
    {
        if(currentWave.Value > _wavesCount)
        {
            ScreenManager.Instance.HideAllScreens();
            //WIN
        }
        else
        {
            _upgradeMode.EnterUpgradeMode();
        }
    }

    private void ExitUpgradeMode()
    {
        _waveSpawner.StartNextWave();
    }

    private void GameOver()
    {
        PauseGame();
        ScreenManager.Instance.HideAllScreens();
        ScreenManager.Instance.ShowScreen(GameplayScreenType.GAME_OVER);
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
