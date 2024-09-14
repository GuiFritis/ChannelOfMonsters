namespace Dialogues
{
    public class DialogueStepTurn : DialogueStepMove
    {
        protected override void EnablePlayerMove()
        {
            _dialogue.Player.Inputs.Ship.TurnShip.Enable();
            _dialogue.Player.Inputs.Ship.TurnShip.performed += CheckPlayerMove;
        }
        
        protected override void DisablePlayerMove()
        {
            _dialogue.Player.Inputs.Ship.TurnShip.performed -= CheckPlayerMove;
            _dialogue.Player.Inputs.Ship.TurnShip.Disable();
        }
    }
}