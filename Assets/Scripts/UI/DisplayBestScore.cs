using TMPro;
using UnityEngine;

[DefaultExecutionOrder(1)]
public class DisplayBestScore : MonoBehaviour
{
    [SerializeField] private TMP_Text _label;
    [SerializeField] private SessionDataManager _sessionData;

    private void OnEnable()
    {
        _label.text = $"Best score: {_sessionData.BestScore}";
    }
}
