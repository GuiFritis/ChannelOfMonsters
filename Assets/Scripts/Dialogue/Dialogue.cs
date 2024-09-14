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
        [SerializeField] private NextStepButton _nextStepButton;
        public Player Player {get {return _player;}}
        public Action OnDialogueEnd;
        [SerializeField] private bool _disabledControls = true;
        private int _currentStep = 0;

        private void OnValidate()
        {
            if(dialogueSteps == null || dialogueSteps.Count == 0)
            {
                try
                {
                    dialogueSteps = GetComponentsInChildren<DialogueStep>().ToList();
                } 
                catch(Exception ex){}
            }
        }

        public void StartFirstStep()
        {
            if(_disabledControls)
            {
                _player.DisableControls();
            }
            dialogueSteps[0].StartStep(this);
            _nextStepButton.Enable(this);
        }

        public void NextStep()
        {
            if(CheckCurrentStep())
            {
                dialogueSteps[_currentStep].EndStep();
                _currentStep++;
                if(_currentStep < dialogueSteps.Count)
                {
                    _nextStepButton.SetStep(dialogueSteps[_currentStep]);
                    dialogueSteps[_currentStep].StartStep(this);
                }
                else
                {
                    if(_disabledControls)
                    {
                        _player.EnableControls();
                    }
                    EndTutorial();
                }
            }
        }

        private void EndTutorial()
        {
            _nextStepButton.Disable();
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