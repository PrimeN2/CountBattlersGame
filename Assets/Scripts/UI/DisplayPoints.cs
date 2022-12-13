using TMPro;
using UnityEngine;

public class DisplayPoints : MonoBehaviour
{
    [SerializeField] private SessionData _sessionData;
    [SerializeField] private TMP_Text _label;

    private void ChangeScoreLabel()
    {
        _label.text = $"Score: {_sessionData.PlayerScore}";
    }

    private void Awake()
    {
        ChangeScoreLabel();
    }

    private void OnEnable()
    {
        _sessionData.OnScoreChanged += ChangeScoreLabel;
    }

    private void OnDisable()
    {
        _sessionData.OnScoreChanged -= ChangeScoreLabel;
    }
}
