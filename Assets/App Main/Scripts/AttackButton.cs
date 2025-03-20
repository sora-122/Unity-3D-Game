// AttackButton.cs
// UIアタックボタン
using UnityEngine;
using UnityEngine.UI;

public class AttackButton : MonoBehaviour
{
    public PlayerController playerController;
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnButtonClicked);
    }

    private void OnButtonClicked()
    {
        playerController.OnAttackButtonClicked();
    }
}
