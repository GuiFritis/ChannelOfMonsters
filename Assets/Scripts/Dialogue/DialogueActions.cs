using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Dialogues
{
    public class DialogueActions : MonoBehaviour
    {
        [SerializeField] private NextStep _nextStep;
        [SerializeField] private NextStepTouch _nextStepTouch;
        [SerializeField] private SkipDialogueBTN _skipDialogueButton;
        [SerializeField] private List<TextMeshProUGUI> _textHelpers;

        public void StartDialogue(Dialogue dialogue)
        {
            _nextStep.Enable(dialogue);
            _skipDialogueButton.Enable(dialogue);
            _nextStepTouch.SetDialogue(dialogue);
            foreach (TextMeshProUGUI textHelper in _textHelpers)
            {        
                if(textHelper == null)
                {
                    continue;
                }        
                textHelper.DOKill();
                textHelper.gameObject.SetActive(true);
                textHelper.DOFade(1, .2f);
            }
        }

        public void EndDialogue()
        {
            _nextStep.Disable();
            _skipDialogueButton.Disable();
            foreach (TextMeshProUGUI textHelper in _textHelpers)
            {
                if(textHelper == null)
                {
                    continue;
                }   
                textHelper.DOFade(0, .2f).OnComplete(() => textHelper.gameObject.SetActive(false));
            }
        }
    }
}