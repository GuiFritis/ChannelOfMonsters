using Dialogues;
using UnityEngine;

public class DialogueStepCannons : DialogueStep
{
    private bool _shot = false;
    [SerializeField] private GameObject[] _inputs;

    public override void StartStep(Dialogue manager)
    {
        base.StartStep(manager);
        _dialogue = manager;
        ShowCannonInputs();
    }

    protected void ShowCannonInputs()
    {
        foreach (var item in _inputs)
        {            
            item.SetActive(true);
        }
        InputTouchCannonFire.OnShot += Shot;
    }
    
    protected virtual void HideCannonInputs()
    {
        foreach (var item in _inputs)
        {            
            item.SetActive(false);
        }
        InputTouchCannonFire.OnShot -= Shot;
    }

    private void Shot()
    {
        _shot = true;
    }

    public override void EndStep()
    {
        HideCannonInputs();
        base.EndStep();
    }

    public override bool CheckStepCompleted()
    {
        return _shot;
    }
}
