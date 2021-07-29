using UnityEngine;
using UnityEngine.UI;

public class DebugLogger : MonoBehaviour
{
    [SerializeField] private Text _debugLabel;

    public void Log(string textToLog)
    {
        _debugLabel.text = textToLog;
    }
}
