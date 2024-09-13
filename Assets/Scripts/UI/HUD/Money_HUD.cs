using UnityEngine;
using TMPro;

public class Money_HUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMesh;
    public SOInt soCoins;

    private void Start()
    {
        soCoins.OnValueChanged += UpdateJoyHUD;
        UpdateJoyHUD(soCoins.Value);
    }

    private void UpdateJoyHUD(int coins)
    {
        _textMesh.text = coins.ToString();
    }

    private void OnDestroy()
    {
        soCoins.OnValueChanged -= UpdateJoyHUD;
    }
}
