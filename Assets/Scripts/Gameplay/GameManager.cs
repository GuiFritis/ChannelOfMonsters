using System.Collections.Generic;
using Dialogues;
using UnityEngine;
using Utils;
using Utils.Singleton;

public class GameManager : Singleton<GameManager>
{
    public SOInt coinsSO;
    public SOInt currentWave;
    [SerializeField] private int _startCoins = 100;
    [SerializeField] private int _wavesCount = 10;
    [SerializeField] private Dialogue _tutorial;
    [SerializeField] private Dialogue _initialSpeech;
    [SerializeField] private List<Dialogue> _waveEndDialogues;
    [SerializeField] private WaveSpawner _waveSpawner;
    public WaveSpawner WaveSpawner { get { return _waveSpawner;}}
    [SerializeField] private CollectablesSpawner _collectablesSpawner;
    [SerializeField] private Storm _storm;
    [SerializeField] private UpgradeMode _upgradeMode;
    [SerializeField] private GameObject _hideWhileUpgrade;
    [SerializeField] private Player _player;
    [SerializeField] private List<Transform> _spawnPoints = new();
    public List<Transform> SpawnPoints { get { return _spawnPoints; } }
    private string _tutorialStrPref = "_TutorialMode";

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
        if(PlayerPrefs.GetInt(_tutorialStrPref, -1) <= 0)
        {
            _tutorial.StartFirstStep();
            _tutorial.OnDialogueEnd += EndTutorial;
        }
        else
        {
            _initialSpeech.StartFirstStep();
            _initialSpeech.OnDialogueEnd += EnterUpgradeMode;
        }
    }

    private void EndTutorial()
    {
        PlayerPrefs.SetInt(_tutorialStrPref, 1);
        _initialSpeech.StartFirstStep();
        _initialSpeech.OnDialogueEnd += EnterUpgradeMode;
    }

    private void WaveEnded()
    {
        if(currentWave.Value >= _wavesCount)
        {            
            _waveEndDialogues[currentWave.Value].StartFirstStep();
            _waveEndDialogues[currentWave.Value].OnDialogueEnd += EnterUpgradeMode;
            PauseGame();
            ScreenManager.Instance.HideAllScreens();
            ScreenManager.Instance.ShowScreen(GameplayScreenType.WIN);
        }
        else
        {
            _collectablesSpawner.StopSpawning();
            _hideWhileUpgrade.SetActive(false);
            _storm.EndStorm();
            _waveEndDialogues[currentWave.Value].StartFirstStep();
            _waveEndDialogues[currentWave.Value].OnDialogueEnd += EnterUpgradeMode;
        }
    }

    private void EnterUpgradeMode()
    {
        _player.DisableControls();
        _upgradeMode.EnterUpgradeMode();
    }

    private void ExitUpgradeMode()
    {
        _player.EnableControls();
        _collectablesSpawner.StartSpawning();
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
        Time.timeScale = 1;
    }
}
