using Unity.VisualScripting;
using UnityEngine;

namespace Dialogues
{
    public class DialogueStep : MonoBehaviour
    {
        [Min(0.3f)]
        [SerializeField] protected float _dismissTime = 1f;
        [SerializeField] protected TextBalloon _balloon;
        [TextArea]
        [SerializeField] protected string _text;
        [SerializeField] protected Color _color = Color.white;
        protected Dialogue _dialogue;
        protected float _timer = 0f;

        protected virtual void Update()
        {
            _timer += Time.deltaTime;
        }

        public virtual void StartStep(Dialogue manager)
        {
            _dialogue = manager;
            gameObject.SetActive(true);
            if(_text != null)
            {
                _balloon.ShowText("<color=#"+_color.ToHexString()+">"+_text+"</color>");
            }
        }

        public virtual void EndStep()
        {
            gameObject.SetActive(false);
            _balloon.HideText();
        }

        public virtual bool CheckStepCompleted()
        {
            return _timer >= _dismissTime;
        }
    }
}
