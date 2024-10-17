using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Dialogues
{
    public class Dialogue : MonoBehaviour
    {
        public List<DialogueStep> dialogueSteps = new();
        [SerializeField] private Player _player;
        [SerializeField] private DialogueActions _dialogueActions;
        public Player Player {get {return _player;}}
        public Action OnDialogueEnd;
        public Action<DialogueStep> OnStepStart;
        [SerializeField] private bool _disabledControls = true;
        private int _currentStep = 0;

        private void OnValidate()
        {
            if(dialogueSteps == null || dialogueSteps.Count == 0)
            {
                dialogueSteps = GetComponentsInChildren<DialogueStep>().ToList();
            }
        }

        public void StartFirstStep()
        {
            _dialogueActions.StartDialogue(this);
            if(_disabledControls)
            {
                _player.DisableControls();
            }
            OnStepStart?.Invoke(dialogueSteps[_currentStep]);
            dialogueSteps[0].StartStep(this);
        }

        public void NextStep()
        {
            if(CheckCurrentStep())
            {
                dialogueSteps[_currentStep].EndStep();
                _currentStep++;
                if(_currentStep < dialogueSteps.Count)
                {
                    OnStepStart?.Invoke(dialogueSteps[_currentStep]);
                    dialogueSteps[_currentStep].StartStep(this);
                }
                else
                {
                    EndDialogue();
                }
            }
        }

        public void SkipDialogue()
        {
            dialogueSteps[_currentStep].EndStep();
            EndDialogue();
        }

        private void EndDialogue()
        {
            if(_disabledControls)
            {
                _player.EnableControls();
            }
            _dialogueActions.EndDialogue();
            gameObject.SetActive(false);
            OnDialogueEnd?.Invoke();
            OnDialogueEnd = null;
        }

        public bool CheckCurrentStep()
        {
            return dialogueSteps[_currentStep].CheckStepCompleted();
        }
    }
}