using UnityEngine;
using Utils.Singleton;

public class GameManager : Singleton<GameManager>
{
    public SOInt coinsSO;
    public SOInt currentWave;

    protected override void Awake()
    {
        base.Awake();
        currentWave.Value = -1;
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
