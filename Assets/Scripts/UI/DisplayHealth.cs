using TMPro;
using UnityEngine;

public class DisplayHealth : MonoBehaviour
{
    [SerializeField] private PlayerLife _playerLife;
    [SerializeField] private TMP_Text _label;

    private void ChangeHealthLabel()
    {
        _label.text = $"Health: {_playerLife.PlayerHealth}";
    }

    private void Awake()
    {
        _label.text = $"Health: 3";
    }

    private void OnEnable()
    {
        _playerLife.OnPlayerDamaged += ChangeHealthLabel;
    }

    private void OnDisable()
    {
        _playerLife.OnPlayerDamaged -= ChangeHealthLabel;
    }
}
