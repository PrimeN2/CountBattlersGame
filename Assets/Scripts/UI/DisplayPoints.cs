using TMPro;
using UnityEngine;

public class DisplayPoints : MonoBehaviour
{
    [SerializeField] private SessionDataManager _sessionData;
    [SerializeField] private TMP_Text _label;

    private void ChangeScoreLabel()
    {
        _label.text = $"Score: {_sessionData.PlayerScore}";
    }

    private void Awake()
    {
        _label.text = $"Score: 0";
    }

    private void OnEnable()
    {
        _sessionData.OnScoreIncreased += ChangeScoreLabel;
    }

    private void OnDisable()
    {
        _sessionData.OnScoreIncreased -= ChangeScoreLabel;
    }
}
