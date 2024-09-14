namespace Dialogues
{
    public class DialogueStepFrontCannon : DialogueStepMove
    {
        protected override void EnablePlayerMove()
        {
            _dialogue.Player.Inputs.Ship.ShootForward.Enable();
            _dialogue.Player.Inputs.Ship.ShootForward.performed += CheckPlayerMove;
        }
        
        protected override void DisablePlayerMove()
        {
            _dialogue.Player.Inputs.Ship.ShootForward.performed -= CheckPlayerMove;
            _dialogue.Player.Inputs.Ship.ShootForward.Disable();
        }
    }
}