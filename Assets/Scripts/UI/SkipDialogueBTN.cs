using Dialogues;
using UnityEngine;

public class SkipDialogueBTN : MonoBehaviour
{
    private Dialogue _manager;

    public void Enable(Dialogue manager)
    {
        _manager = manager;
        gameObject.SetActive(true);
    }

    public void Disable()
    {
        _manager = null;
        gameObject.SetActive(false);
    }

    public void SkipDialogue()
    {
        _manager.SkipDialogue();
    }
}
