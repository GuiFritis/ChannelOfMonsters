using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Dialogues
{
    public class DialogueActions : MonoBehaviour
    {
        [SerializeField] private NextStep _nextStep;
        [SerializeField] private SkipDialogueBTN _skipDialogueButton;
        [SerializeField] private TextMeshProUGUI _textHelper;
        [SerializeField] private GameObject _touchControls;

        public void StartDialogue(Dialogue dialogue)
        {
            _nextStep.Enable(dialogue);
            _skipDialogueButton.Enable(dialogue);
            _textHelper.DOKill();
            _textHelper.gameObject.SetActive(true);
            _textHelper.DOFade(1, .2f);
            _touchControls.SetActive(false);
        }

        public void EndDialogue()
        {
            _nextStep.Disable();
            _skipDialogueButton.Disable();
            _textHelper.DOFade(0, .2f).OnComplete(() => _textHelper.gameObject.SetActive(false));
            #if UNITY_EDITOR
                _touchControls.SetActive(true);
            #else
            if(Input.touchSupported)
            {
                _touchControls.SetActive(true);
            }
            #endif
        }
    }
}