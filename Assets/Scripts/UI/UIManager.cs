using TMPro;
using UnityEngine;
using Utils.Singleton;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private Player _player;
    [SerializeField] private GameObject _descriptionPanel;
    [SerializeField] private TextMeshProUGUI _descriptionTitle;
    [SerializeField] private TextMeshProUGUI _descriptionText;

    private void OnValidate()
    {
        _player ??= FindObjectOfType<Player>();
    }

    private void Start()
    {
        if(_player != null)
        {
            // _player.OnMissclick += CloseAll;
        }
    }

    private void CloseAll()
    {
        // Invoke(nameof(HideTowerMenu), Time.deltaTime * 2);
        _descriptionPanel.SetActive(false);
        // _towerMenu_UI.HideMenu();
    }

    // public void ShowDescription(EnemySO enemySO)
    // {
    //     _descriptionTitle.text = enemySO.enemyName;
    //     _descriptionText.text = enemySO.description;
    //     _descriptionPanel.SetActive(true);
    // }
}
