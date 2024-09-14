using UnityEngine.InputSystem;

namespace Dialogues
{
    public class DialogueStepMove : DialogueStep
    {
        protected bool _moved = false;

        public override void StartStep(Dialogue manager)
        {
            base.StartStep(manager);
            _dialogue = manager;
            EnablePlayerMove();
        }

        protected virtual void EnablePlayerMove()
        {
            _dialogue.Player.Inputs.Ship.MoveForward.Enable();
            _dialogue.Player.Inputs.Ship.MoveForward.performed += CheckPlayerMove;
        }
        
        protected virtual void DisablePlayerMove()
        {
            _dialogue.Player.Inputs.Ship.MoveForward.performed -= CheckPlayerMove;
            _dialogue.Player.Inputs.Ship.MoveForward.Disable();
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