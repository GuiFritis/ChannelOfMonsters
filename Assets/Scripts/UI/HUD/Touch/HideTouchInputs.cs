using System.Collections.Generic;
using UnityEngine;

public class HideTouchInputs : MonoBehaviour
{
    [SerializeField] private List<GameObject> _touchInputs;

    public void HideInputs()
    {
        foreach (GameObject touchInput in _touchInputs)
        {
            touchInput.SetActive(false);
        }
    }

    public void ShowInputs()
    {        
        foreach (GameObject touchInput in _touchInputs)
        {
            touchInput.SetActive(true);
        }
    }
}
