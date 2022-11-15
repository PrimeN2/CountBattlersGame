using TMPro;
using UnityEngine;

[DefaultExecutionOrder(1)]
public class DisplayBestScore : MonoBehaviour
{
    [SerializeField] private TMP_Text _label;
    [SerializeField] private SessionDataManager _sessionData;

    private void OnEnable()
    {
        SetScore();

        _sessionData.OnScoreChanged += SetScore;
    }

    private void OnDisable()
    {
        _sessionData.OnScoreChanged -= SetScore;
    }

    private void SetScore()
    {
        _label.text = $"Coins: {_sessionData.PlayerScore}";
    }
}
