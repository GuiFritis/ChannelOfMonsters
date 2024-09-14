using UnityEngine;

public class RestartTutorialBTN : MonoBehaviour
{
    private string _tutorialStrPref = "_TutorialMode";

    private void Start()
    {
        if(PlayerPrefs.GetInt(_tutorialStrPref, -1) <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void RestartTutorial()
    {
        PlayerPrefs.SetInt(_tutorialStrPref, 0);
    }
}
