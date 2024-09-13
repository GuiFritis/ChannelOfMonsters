using UnityEngine;

public class RotateWithPlayer : MonoBehaviour
{
    [SerializeField] private GameObject _player;

    private void OnValidate()
    {
        if (_player == null)
        {
            _player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    private void Update()
    {
        if(_player != null)
        {
            transform.rotation = _player.transform.rotation;
            transform.position = Camera.main.WorldToScreenPoint(_player.transform.position);
        }
    }
}
