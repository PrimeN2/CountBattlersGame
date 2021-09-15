using System;
using System.Collections;
using UnityEngine;

public class LineSwitcher : MonoBehaviour
{
    public Func<int, IEnumerator> OnPlayerMoving;
    public Func<int, int, IEnumerator> OnPlayerTurning;
    public Lines CurrentLine { get => _currentLine; }
    private Lines _currentLine;

    private Coroutine _currentSwitchCoutine;
    private Coroutine _movingCoroutine;
    private Coroutine _turningCoroutine;

    private bool _isLineSwitching = false;

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
        int sumLine = (int)_currentLine + (int)direction;

        if (_isLineSwitching)
            StopSwitch();

        if (sumLine > -2 && sumLine < 2)
        {
            _currentLine = (Lines)sumLine;
            _currentSwitchCoutine = StartCoroutine(SwitchLine(_currentLine, direction));
        }
    }
    private IEnumerator SwitchLine(Lines currentLine, Lines direction)
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
