using Dialogues;
using UnityEngine;

public class DialogueStepTouchTurn : DialogueStep
{
    protected bool _moved = false;
    [SerializeField] private InputTouchTurnShip[] _inputs;

    public override void StartStep(Dialogue manager)
    {
        base.StartStep(manager);
        _dialogue = manager;
        ShowTurnInput();
    }

    protected void ShowTurnInput()
    {
        foreach (var item in _inputs)
        {            
            item.gameObject.SetActive(true);
        }
        InputTouchTurnShip.OnTouch += Touched;
    }
    
    protected virtual void HideTurnInput()
    {
        foreach (var item in _inputs)
        {            
            item.gameObject.SetActive(false);
        }
        InputTouchTurnShip.OnTouch -= Touched;
    }

    private void Touched()
    {
        _moved = true;
    }

    public override void EndStep()
    {
        HideTurnInput();
        base.EndStep();
    }

    public override bool CheckStepCompleted()
    {
        return _moved;
    }
}
