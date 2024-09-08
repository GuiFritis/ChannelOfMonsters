using System.Collections.Generic;
using UnityEngine;
using Utils.Singleton;

public enum GameplayScreenType
{
    PLAYER_HUD,
    MENU,
    GAME_OVER
}

public class ScreenManager : Singleton<ScreenManager>
{    
    public List<ScreenType> screens = new();

    public void ShowScreen(GameplayScreenType screenType, bool active = true)
    {
        screens.Find(i => i.type.Equals(screenType)).screen.SetActive(active);
    }

    public bool GetScreenStateByType(GameplayScreenType screenType)
    {
        return screens.Find(i => i.type.Equals(screenType)).screen.activeInHierarchy;
    }

    public void HideAllScreens()
    {
        screens.ForEach(i => i.screen.SetActive(false));
    }
}

[System.Serializable]
public struct ScreenType
{
    public GameplayScreenType type;
    public GameObject screen;
}
