using UnityEngine;
using Utils;

public class SFX_Pool : PoolBase<AudioPlayer, SFX_Pool>
{
    public void Play(AudioClip clip)
    {
        if(clip != null)
        {
            var item = GetPoolItem();
            item.pool = this;
            item.PlayClip(clip);
        }
    }
}