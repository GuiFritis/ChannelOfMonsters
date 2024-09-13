using System.Collections.Generic;
using UnityEngine;
using Utils;

public class PlayAudio : MonoBehaviour
{
    [SerializeField] private List<AudioClip> _sfx;

    public void PlaySFX()
    {
        SFX_Pool.Instance.Play(_sfx.GetRandom());
    }
}
