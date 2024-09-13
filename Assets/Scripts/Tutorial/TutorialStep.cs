using TMPro;
using UnityEngine;

namespace Tutorial
{
    public class TutorialStep : MonoBehaviour
    {
        [Min(0.3f)]
        [SerializeField] protected float _dismissTime = 1f;
        [SerializeField] protected TextBalloon _balloon;
        [TextArea]
        [SerializeField] protected string _text;

        protected float _timer = 0f;

        protected virtual void Update()
        {
            _timer += Time.deltaTime;
        }

        public virtual void StartStep(TutorialManager manager)
        {
            gameObject.SetActive(true);
            if(_text != null)
            {
                _balloon.ShowText(_text);
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
