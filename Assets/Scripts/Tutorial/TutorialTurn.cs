namespace Tutorial
{
    public class TutorialTurn : TutorialMove
    {
        protected override void EnablePlayerMove()
        {
            _tutorialManager.Player.Inputs.Ship.TurnShip.Enable();
            _tutorialManager.Player.Inputs.Ship.TurnShip.performed += CheckPlayerMove;
        }
        
        protected override void DisablePlayerMove()
        {
            _tutorialManager.Player.Inputs.Ship.TurnShip.performed -= CheckPlayerMove;
            _tutorialManager.Player.Inputs.Ship.TurnShip.Disable();
        }
    }
}