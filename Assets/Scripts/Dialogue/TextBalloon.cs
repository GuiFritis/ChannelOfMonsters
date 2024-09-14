using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class TextBalloon : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMesh;
    [SerializeField] private Image _balloon;
    [SerializeField] private Image _balloonTip;

    public void ShowText(string text)
    {
        _textMesh.text = text;
        _balloon.DOKill();
        _balloonTip.DOKill();
        _textMesh.DOKill();
        _balloon.DOFade(1f, .2f);
        _balloonTip.DOFade(1f, .2f);
        _textMesh.DOFade(1f, .2f);
    }

    public void HideText()
    {
        _balloon.DOKill();
        _balloonTip.DOKill();
        _textMesh.DOKill();
        _balloon.DOFade(0f, .2f);
        _balloonTip.DOFade(0f, .2f);
        _textMesh.DOFade(0f, .2f);
    }
}
