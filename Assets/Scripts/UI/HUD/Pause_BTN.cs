using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause_BTN : MonoBehaviour
{
    public void PauseGame()
    {
        ScreenManager.Instance.HideAllScreens();
        ScreenManager.Instance.ShowScreen(GameplayScreenType.MENU);
        GameManager.Instance.PauseGame();
    }
}
