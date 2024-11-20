using System;
using UnityEngine;

public class InputTouchCannonFire : MonoBehaviour
{
    public static Action OnShot;

    public void OnCannonShot()
    {
        OnShot?.Invoke();
    }
}
