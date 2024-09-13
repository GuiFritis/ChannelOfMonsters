using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Utils;

public class Storm : MonoBehaviour
{
    [SerializeField] WaveSpawner _waveSpawner;
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private float _transitionDuration = 1f;
    [SerializeField] private float _fadeAmount = .4f;
    [SerializeField] private ParticleSystem _lightning;
    [SerializeField] private ParticleSystem _rain;
    [SerializeField] private List<AudioClip> _thunder;
    [SerializeField] private List<MusicPlayer> _musicPlayers;
    private Coroutine _lightningCoroutine;

    private void Awake()
    {
        _waveSpawner.OnWaveEnded += EndStorm;
        foreach (var item in _musicPlayers)
        {
            item.enabled = false;
        }
    }
    
    public void StartStorm()
    {
        _sprite.DOFade(_fadeAmount, _transitionDuration).SetDelay(1f).OnComplete(
            () => {
                _lightning.Play(); 
                _rain.Play();
                _waveSpawner.StartNextWave();
            }
        );
        _musicPlayers.ForEach(i => i.enabled = true);
        _lightningCoroutine = StartCoroutine(SummonLightning());
    }

    private IEnumerator SummonLightning()
    {
        while(true)
        {
            SFX_Pool.Instance.Play(_thunder.GetRandom());
            yield return new WaitForSeconds(Random.Range(6f, 15f));
        }
    }

    public void EndStorm()
    {
        StopCoroutine(_lightningCoroutine);
        _musicPlayers.ForEach(i => i.enabled = false);
        _lightning.Stop();
        _rain.Stop();
        _sprite.DOFade(0f, _transitionDuration);
    }
}
