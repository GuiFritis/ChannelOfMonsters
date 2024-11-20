using System.Collections;
using System.Collections.Generic;
using Dialogues;
using UnityEngine;
using UnityEngine.EventSystems;

public class NextStepTouch : MonoBehaviour, IPointerClickHandler
{
    private Dialogue _dialogue;

    public void SetDialogue(Dialogue dialogue)
    {
        _dialogue = dialogue;
        _dialogue.OnDialogueEnd += DialogueEnd;
    }

    private void DialogueEnd()
    {
        _dialogue = null;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _dialogue?.NextStep();
    }
}
