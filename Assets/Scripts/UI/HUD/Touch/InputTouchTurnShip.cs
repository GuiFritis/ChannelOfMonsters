using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InputTouchTurnShip : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private float _turnDirection;
    [SerializeField] private Player _player;
    [SerializeField] private Color _activeColor;
    [SerializeField] private List<Image> _sprites;
    private Color _inactiveColor;

    private void Start()
    {
        _inactiveColor = _sprites[0].color;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _player.TurnShip(_turnDirection);
        RecolorSprites(_activeColor);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _player.TurnShip(0);
        RecolorSprites(_inactiveColor);
    }

    private void RecolorSprites(Color color)
    {
        foreach (var item in _sprites)
        {
            item.color = color;
        }
    }
}
