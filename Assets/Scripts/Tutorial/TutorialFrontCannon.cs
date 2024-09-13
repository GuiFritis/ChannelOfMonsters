namespace Tutorial
{
    public class TutorialFrontCannon : TutorialMove
    {
        protected override void EnablePlayerMove()
        {
            _tutorialManager.Player.Inputs.Ship.ShootForward.Enable();
            _tutorialManager.Player.Inputs.Ship.ShootForward.performed += CheckPlayerMove;
        }
        
        protected override void DisablePlayerMove()
        {
            _tutorialManager.Player.Inputs.Ship.ShootForward.performed -= CheckPlayerMove;
            _tutorialManager.Player.Inputs.Ship.ShootForward.Disable();
        }
    }
}