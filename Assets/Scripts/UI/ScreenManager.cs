using System.Collections.Generic;
using UnityEngine;
using Utils.Singleton;

public enum GameplayScreenType
{
    PLAYER_HUD,
    MENU,
    GAME_OVER,
    WIN
}

public class ScreenManager : Singleton<ScreenManager>
{    
    public List<ScreenType> screens = new();
    [SerializeField] private HideTouchInputs _touchInputs;

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

    public void ShowTouchInputs()
    {
        _touchInputs.ShowInputs();
    }

    public void HideTouchInputs()
    {
        _touchInputs.HideInputs();
    }
}

[System.Serializable]
public struct ScreenType
{
    public GameplayScreenType type;
    public GameObject screen;
}
