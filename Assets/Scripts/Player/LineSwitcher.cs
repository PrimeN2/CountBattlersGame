using System;
using System.Collections;
using UnityEngine;

public class LineSwitcher : MonoBehaviour, ISwitcher
{
    public Func<int, IEnumerator> OnPlayerMoving;
    public Func<int, int, IEnumerator> OnPlayerTurning;
    public Line CurrentLine { get => _currentLine; }
    private Line _currentLine;

    private Coroutine _currentSwitchCoutine;
    private Coroutine _movingCoroutine;
    private Coroutine _turningCoroutine;

    private bool _isLineSwitching = false;

    public enum Line
    {
        Center = 0,
        Right = 1,
        Left = -1,
    }
    private void Awake()
    {
        _currentLine = Line.Center;

    }
    public bool TrySwitch(ColorSwitcher.VerticalDirection verticalDirection, Line horizontalDirection)
    {
        int sumLine = (int)_currentLine + (int)horizontalDirection;

        if (_isLineSwitching)
            StopSwitch();

        if (sumLine > -2 && sumLine < 2)
        {
            _currentLine = (Line)sumLine;
            _currentSwitchCoutine = StartCoroutine(SwitchLine(_currentLine, horizontalDirection));
            return true;
        }

        return false;
    }
    private IEnumerator SwitchLine(Line currentLine, Line direction)
    {
        if (OnPlayerMoving != null && OnPlayerTurning != null)
        {
            _isLineSwitching = true;

            _turningCoroutine = StartCoroutine(OnPlayerTurning.Invoke((int)currentLine, (int)direction));
            _movingCoroutine = StartCoroutine(OnPlayerMoving.Invoke((int)currentLine));
            yield return _movingCoroutine;

            _isLineSwitching = false;
        }
    }
    private void StopSwitch()
    {
        StopCoroutine(_turningCoroutine);
        StopCoroutine(_movingCoroutine);
        StopCoroutine(_currentSwitchCoutine);
    }
}
