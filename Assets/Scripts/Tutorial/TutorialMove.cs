using UnityEngine.InputSystem;

namespace Tutorial
{
    public class TutorialMove : TutorialStep
    {
        protected TutorialManager _tutorialManager;
        protected bool _moved = false;

        public override void StartStep(TutorialManager manager)
        {
            base.StartStep(manager);
            _tutorialManager = manager;
            EnablePlayerMove();
        }

        protected virtual void EnablePlayerMove()
        {
            _tutorialManager.Player.Inputs.Ship.MoveForward.Enable();
            _tutorialManager.Player.Inputs.Ship.MoveForward.performed += CheckPlayerMove;
        }
        
        protected virtual void DisablePlayerMove()
        {
            _tutorialManager.Player.Inputs.Ship.MoveForward.performed -= CheckPlayerMove;
            _tutorialManager.Player.Inputs.Ship.MoveForward.Disable();
        }

        protected virtual void CheckPlayerMove(InputAction.CallbackContext ctx)
        {
            _moved = true;
        }

        public override void EndStep()
        {
            DisablePlayerMove();
            base.EndStep();
        }

        public override bool CheckStepCompleted()
        {
            return _moved;
        }
    }
}