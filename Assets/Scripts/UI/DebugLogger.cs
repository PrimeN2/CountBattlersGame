using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DebugLogger : MonoBehaviour
{
    [SerializeField] private TMP_Text _debugLabel;

    public void Log(string textToLog)
    {
        _debugLabel.text = textToLog;
    }
}
