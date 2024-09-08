using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resume_BTN : MonoBehaviour
{
    public void ResumeGame()
    {
        GameManager.Instance.ResumeGame();
        ScreenManager.Instance.HideAllScreens();
        ScreenManager.Instance.ShowScreen(GameplayScreenType.PLAYER_HUD);
    }
}
