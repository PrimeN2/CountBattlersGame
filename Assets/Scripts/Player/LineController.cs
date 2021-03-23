using System.Collections;
using UnityEngine;

public class LineController : MonoBehaviour
{
    public Lines CurrentLine { get => _currentLine; }
    private Lines _currentLine;

    [SerializeField] private float _speed = 0.05f;
    private bool _isSwitchingLine = false;
    private Vector3 _targetPosition = new Vector3(0, 0, 0);

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
        if (!_isSwitchingLine)
        {
            int sumLine = (int)_currentLine + (int)line;
            if (sumLine >= -1 && sumLine <= 1)
            {
                _currentLine = (Lines)sumLine;
                StartCoroutine(SwitchLine(_currentLine));
            }
        }
    }
    private IEnumerator SwitchLine(Lines line)
    {
        _isSwitchingLine = true;
        _targetPosition = new Vector3(0.83f * (int)line, transform.position.y, transform.position.z);
        for (int i = 0; i < 20; ++i)
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _speed);
            yield return null;
        }
        _isSwitchingLine = false;
    }
}
