using Dialogues;
using UnityEngine;
using UnityEngine.UI;

public class NextStepButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    private DialogueStep _tutorialStep;
    private Dialogue _manager;

    public void Enable(Dialogue manager)
    {
        _manager = manager;
        gameObject.SetActive(true);
    }

    public void SetStep(DialogueStep step)
    {
        _tutorialStep = step;
    }
    
    private void Update()
    {
        if(_tutorialStep != null)
        {
            _button.interactable = _tutorialStep.CheckStepCompleted();
        }
    }

    public void NextStep()
    {
        _tutorialStep = null;
        _manager.NextStep();
    }

    public void Disable()
    {
        _manager = null;
        _tutorialStep = null;
        gameObject.SetActive(false);
    }
}
