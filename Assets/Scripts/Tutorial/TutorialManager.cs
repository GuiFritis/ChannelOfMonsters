using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tutorial
{
    public class TutorialManager : MonoBehaviour
    {
        public List<TutorialStep> tutorialSteps = new();
        [SerializeField] private Player _player;
        public Player Player {get {return _player;}}
        public Action OnEndTutorial;
        private int _currentStep = 0;
        private string _tutorialStrPref = "_TutorialMode";

        void Start()
        {
            if(PlayerPrefs.GetInt(_tutorialStrPref, -1) <= 0)
            {
                tutorialSteps[_currentStep].StartStep(this);
                _player.DisableControls();
            }
            else
            {
                EndTutorial();
            }
        }

        public void NextStep()
        {
            if(CheckCurrentStep())
            {
                tutorialSteps[_currentStep].EndStep();
                _currentStep++;
                if(_currentStep < tutorialSteps.Count)
                {
                    tutorialSteps[_currentStep].StartStep(this);
                }
                else
                {
                    PlayerPrefs.SetInt(_tutorialStrPref, 1);
                    _player.EnableControls();
                    EndTutorial();

                }
            }
        }

        private void EndTutorial()
        {
            gameObject.SetActive(false);
            OnEndTutorial?.Invoke();
            OnEndTutorial = null;
        }

        public bool CheckCurrentStep()
        {
            return tutorialSteps[_currentStep].CheckStepCompleted();
        }
    }
}