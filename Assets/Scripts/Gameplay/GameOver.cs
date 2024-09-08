using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public GameObject loseScreen;
    public GameObject winScreen;
    public HealthBase coreHP;

    private void Start()
    {
        if(loseScreen != null)
        {
            loseScreen.SetActive(false);
        }
        if(winScreen != null)
        {
            winScreen.SetActive(false);
        }
        if(coreHP != null)
        {
            coreHP.OnDeath += GameLost;
        }
    }

    private void GameWon()
    {
        winScreen.SetActive(true);
        if(GameManager.Instance != null)
        {
            GameManager.Instance.PauseGame();
        }
    }

    private void GameLost(HealthBase hp)
    {
        loseScreen.SetActive(true);
        if(GameManager.Instance != null)
        {
            GameManager.Instance.PauseGame();
        }
    }
}
