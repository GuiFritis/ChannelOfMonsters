using Dialogues;
using UnityEngine;

public class NextStep : MonoBehaviour
{
    private DialogueStep _tutorialStep;
    private Dialogue _manager;
    private Gameplay _inputs;

    private void Awake()
    {
        SetInputs();
    }

    private void SetInputs()
    {
        _inputs = new Gameplay();
        _inputs.Ship.ShootForward.performed += ctx => EndStep();
    }

    public void Enable(Dialogue manager)
    {
        _manager = manager;
        _inputs.Enable();
        _manager.OnStepStart += SetStep;
    }

    public void SetStep(DialogueStep step)
    {
        _tutorialStep = step;
    }

    public void EndStep()
    {
        if(_tutorialStep.CheckStepCompleted())
        {
            _tutorialStep = null;
            _manager.NextStep();
        }
    }

    public void Disable()
    {
        _manager = null;
        _tutorialStep = null;
        _inputs.Disable();
    }
}
