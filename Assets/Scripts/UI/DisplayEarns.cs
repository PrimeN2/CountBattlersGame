using TMPro;
using UnityEngine;

public class DisplayEarns : MonoBehaviour
{
    [SerializeField] private TMP_Text _label;
    [SerializeField] private SessionData _sessionData;

    private void OnEnable() =>
        _sessionData.OnScoreIncreased += SetScore;

    private void OnDisable() =>
        _sessionData.OnScoreIncreased -= SetScore;

    private void SetScore(int increase) =>
        _label.text = $"Coins earned\n{increase}";
}
