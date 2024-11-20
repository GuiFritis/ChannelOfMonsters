using UnityEngine;

public class HideWhenTouch : MonoBehaviour
{
    [Tooltip("If true hides when touch suported, if false hides when not touch suported")]
    [SerializeField] private bool _hideWhenTouch = true;

    private void Awake()
    {
        #if !UNITY_EDITOR
        if ((_hideWhenTouch && Input.touchSupported) || (!_hideWhenTouch && !Input.touchSupported))
        {
            Destroy(gameObject);
        }
        #else
        if(_hideWhenTouch)
        {
            Destroy(gameObject);
        }
        #endif

    }
}
