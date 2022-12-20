using TMPro;
using UnityEngine;

public class DisplayBalance : MonoBehaviour
{
    [SerializeField] private TMP_Text _label;
    [SerializeField] private SessionData _sessionData;

    private void OnEnable() => 
        _sessionData.OnScoreChanged += SetScore;

    private void OnDisable() =>
        _sessionData.OnScoreChanged -= SetScore;

    private void SetScore() =>
        _label.text = _sessionData.PlayerScore.ToString();
}
