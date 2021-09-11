using TMPro;
using UnityEngine;

public class DisplayHealth : MonoBehaviour
{
    [SerializeField] private SessionData _sessionData;
    [SerializeField] private TMP_Text _label;

    private void ChangeHealthLabel()
    {
        _label.text = $"Health: {_sessionData.PlayerHealth}";
    }

    private void Awake()
    {
        _label.text = $"Health: 3";
    }

    private void OnEnable()
    {
        _sessionData.OnPlayerDamaged += ChangeHealthLabel;
    }

    private void OnDisable()
    {
        _sessionData.OnPlayerDamaged -= ChangeHealthLabel;
    }
}
