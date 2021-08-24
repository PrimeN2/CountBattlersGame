using System;
using System.Collections;
using UnityEngine;

public class LineSwitcher : MonoBehaviour
{
    public static Func<int, IEnumerator> OnPlayerMoving;
    public static Func<int, int, IEnumerator> OnPlayerTurning;
    public Lines CurrentLine { get => _currentLine; }
    private Lines _currentLine;

    private bool _isSwitchingLine = false;

    public enum Lines
    {
        Center = 0,
        Right = 1,
        Left = -1,
    }
    private void Awake()
    {
        _currentLine = Lines.Center;

    }
    public void TryChangeLine(Lines direction)
    {
        if (!_isSwitchingLine)
        {
            int sumLine = (int)_currentLine + (int)direction;
            if (sumLine > -2 && sumLine < 2)
            {
                _currentLine = (Lines)sumLine;
                StartCoroutine(SwitchLine(_currentLine, direction));
            }
        }
    }
    private IEnumerator SwitchLine(Lines currentLine, Lines direction)
    {
        if (OnPlayerMoving != null && OnPlayerTurning != null)
        {
            _isSwitchingLine = true;

            StartCoroutine(OnPlayerTurning.Invoke((int)currentLine, (int)direction));
            yield return StartCoroutine(OnPlayerMoving.Invoke((int)currentLine));

            _isSwitchingLine = false;
        }

    }
}
