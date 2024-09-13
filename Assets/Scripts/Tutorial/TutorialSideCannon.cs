namespace Tutorial
{
    public class TutorialSideCannon : TutorialMove
    {


        protected override void EnablePlayerMove()
        {
            _tutorialManager.Player.Inputs.Ship.ShootLeft.Enable();
            _tutorialManager.Player.Inputs.Ship.ShootLeft.performed += CheckPlayerMove;
            _tutorialManager.Player.Inputs.Ship.ShootRight.Enable();
            _tutorialManager.Player.Inputs.Ship.ShootRight.performed += CheckPlayerMove;
        }
        
        protected override void DisablePlayerMove()
        {
            _tutorialManager.Player.Inputs.Ship.ShootLeft.performed -= CheckPlayerMove;
            _tutorialManager.Player.Inputs.Ship.ShootLeft.Disable();
            _tutorialManager.Player.Inputs.Ship.ShootRight.performed -= CheckPlayerMove;
            _tutorialManager.Player.Inputs.Ship.ShootRight.Disable();
        }
    }
}