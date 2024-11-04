using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InputTouchMoveForward : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Player _player;
    [SerializeField] private Color _activeColor;
    [SerializeField] private List<Image> _sprites;
    private Color _inactiveColor;
    private bool _isMoving = false;

    private void Start()
    {
        _inactiveColor = _sprites[0].color;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _player.ToggleMoveForward();
        RecolorSprites(_player.IsMoving() ? _activeColor : _inactiveColor);
    }

    private void RecolorSprites(Color color)
    {
        foreach (var item in _sprites)
        {
            item.color = color;
        }
    }

    private void OnDisable()
    {
        _isMoving = _player.IsMoving();
        if(_isMoving)
        {
            _player.ToggleMoveForward();
            RecolorSprites(_inactiveColor);
        }
    }

    private void OnEnable()
    {
        if(_isMoving)
        {
            _player.ToggleMoveForward();
            RecolorSprites(_activeColor);
        }
    }
}
