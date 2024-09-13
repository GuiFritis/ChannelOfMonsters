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
    [SerializeField] private Storm _storm;
    [SerializeField] private UpgradeMode _upgradeMode;
    [SerializeField] private GameObject _hideWhileUpgrade;
    [SerializeField] private Player _player;
    [SerializeField] private List<Transform> _spawnPoints = new();
    public List<Transform> SpawnPoints { get { return _spawnPoints; } }

    private void OnValidate()
    {
        if(_player == null)
        {
            var obj = GameObject.FindGameObjectWithTag("Player");
            if(obj != null)
            {
                _player = obj.GetComponent<Player>();
            }
        }
    }

    protected override void Awake()
    {
        base.Awake();
        _player.transform.position = _spawnPoints.GetRandom().position;
    }

    private void Start()
    {
        currentWave.Value = -1;
        coinsSO.Value = _startCoins;
        _waveSpawner.OnWaveEnded += WaveEnded;
        _upgradeMode.OnEndUpgradeTime += ExitUpgradeMode;
        _player.Health.OnDeath += hp => GameOver();
        WaveEnded();
    }

    private void WaveEnded()
    {
        if(currentWave.Value > _wavesCount)
        {
            PauseGame();
            ScreenManager.Instance.HideAllScreens();
            ScreenManager.Instance.ShowScreen(GameplayScreenType.WIN);
        }
        else
        {
            _player.DisableControls();
            _upgradeMode.EnterUpgradeMode();
            _hideWhileUpgrade.SetActive(false);
        }
    }

    private void ExitUpgradeMode()
    {
        _player.EnableControls();
        _storm.StartStorm();
        _hideWhileUpgrade.SetActive(true);
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

    private void OnDestroy()
    {
        _waveSpawner.OnWaveEnded -= WaveEnded;
        _upgradeMode.OnEndUpgradeTime -= ExitUpgradeMode;
        _player.Health.OnDeath -= hp => GameOver();
    }
}
