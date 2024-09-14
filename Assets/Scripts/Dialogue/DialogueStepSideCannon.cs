namespace Dialogues
{
    public class DialogueStepSideCannon : DialogueStepMove
    {


        protected override void EnablePlayerMove()
        {
            _dialogue.Player.Inputs.Ship.ShootLeft.Enable();
            _dialogue.Player.Inputs.Ship.ShootLeft.performed += CheckPlayerMove;
            _dialogue.Player.Inputs.Ship.ShootRight.Enable();
            _dialogue.Player.Inputs.Ship.ShootRight.performed += CheckPlayerMove;
        }
        
        protected override void DisablePlayerMove()
        {
            _dialogue.Player.Inputs.Ship.ShootLeft.performed -= CheckPlayerMove;
            _dialogue.Player.Inputs.Ship.ShootLeft.Disable();
            _dialogue.Player.Inputs.Ship.ShootRight.performed -= CheckPlayerMove;
            _dialogue.Player.Inputs.Ship.ShootRight.Disable();
        }
    }
}