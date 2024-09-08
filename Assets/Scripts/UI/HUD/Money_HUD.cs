using UnityEngine;
using UnityEngine.UIElements;

public class Money_HUD : MonoBehaviour
{
    [SerializeField] private UIDocument _uiDocument;
    private Label _moneyLabel;
    public SOInt coinsSO;

    private void Start()
    {
        coinsSO.OnValueChanged += UpdateJoyHUD;
        _moneyLabel = _uiDocument.rootVisualElement.Q<Label>("joy");
        UpdateJoyHUD(coinsSO.Value);
    }

    private void UpdateJoyHUD(int joy)
    {
        _moneyLabel.text = joy.ToString();
    }
}
