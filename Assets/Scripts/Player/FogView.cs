using System.Collections;
using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class FogView : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _fogCamera;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            _fogCamera.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            _fogCamera.enabled = false;
        }
    }
}
