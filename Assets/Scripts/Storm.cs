using DG.Tweening;
using UnityEngine;

public class Storm : MonoBehaviour
{
    [SerializeField] WaveSpawner _waveSpawner;
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private float _transitionDuration = 1f;
    [SerializeField] private float _fadeAmount = .4f;
    [SerializeField] private ParticleSystem _lightning;
    [SerializeField] private AudioClip _thunder;

    private void Awake()
    {
        _waveSpawner.OnWaveEnded += EndStorm;
    }
    
    public void StartStorm()
    {
        _sprite.DOFade(_fadeAmount, _transitionDuration).SetDelay(1f).OnComplete(
            () => {_lightning.Play(); _waveSpawner.StartNextWave();}
        );
    }

    public void EndStorm()
    {
        _lightning.Stop();
        _sprite.DOFade(0f, _transitionDuration);
    }
}
