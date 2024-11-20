using Dialogues;
using UnityEngine;

public class DialogueStepTouchForward : DialogueStep
{
    protected int _moved = 0;
    [SerializeField] private InputTouchMoveForward _input;

    public override void StartStep(Dialogue manager)
    {
        base.StartStep(manager);
        _dialogue = manager;
        ShowMoveForwardInput();
    }

    protected void ShowMoveForwardInput()
    {
        _input.gameObject.SetActive(true);
        InputTouchMoveForward.OnTouch += Touched;
    }
    
    protected virtual void HideMoveForwardInput()
    {
        _input.gameObject.SetActive(false);
        InputTouchMoveForward.OnTouch -= Touched;
    }

    private void Touched()
    {
        _moved++;
    }

    public override void EndStep()
    {
        HideMoveForwardInput();
        base.EndStep();
    }

    public override bool CheckStepCompleted()
    {
        return _moved >= 2;
    }
}
