using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    private Dictionary<float, Lines> _linesPosition = new Dictionary<float, Lines>();
    public Lines CurrentLine { get => _currentLine; }
    private Lines _currentLine;

    public enum Lines
    {
        Center = 0,
        Right = 1,
        Left = -1
    }
    private void Awake()
    {
        _currentLine = Lines.Center;
    }
    public void ChangeLine(Lines line)
    {
        int sumLine = (int)_currentLine + (int)line;
        if (sumLine >= -1 && sumLine <= 1)
        {
            _currentLine = (Lines)sumLine;
            StartCoroutine(SwitchLine(line));
        }
    }
    private IEnumerator SwitchLine(Lines line)
    {
        for (int i = 0; i < 83; ++i)
        {
            transform.position += new Vector3(0.01f, 0, 0) * (int)line;
            if (i <= 40)
                transform.Rotate(new Vector3(0, -1, 0));
            else
                transform.Rotate(new Vector3(0, 1, 0));
            yield return null;
        }
    }
}
