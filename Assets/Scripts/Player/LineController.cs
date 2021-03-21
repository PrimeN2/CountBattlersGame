using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    public Lines CurrentLine { get => _currentLine; }
    private Lines _currentLine;

    [SerializeField] private float _speed = 5f;
    private Dictionary<float, Lines> _linesPosition = new Dictionary<float, Lines>();
    private bool _isSwitchingLine = false;
    private Vector3 _currentPosition = new Vector3(0, 0, 0);

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
                StartCoroutine(SwitchLine(line));
            }
        }
    }
    private IEnumerator SwitchLine(Lines line)
    {
        _isSwitchingLine = true;
        _currentPosition = Vector3.right * transform.position.x;
        for (int i = 0; i < 83; ++i)
        {
            transform.position = Vector3.Lerp(_currentPosition, Vector3.right * 0.83f * (int)line, (Mathf.Sin(_speed * Time.time) + 1.0f) / 2.0f);
            yield return null;
        }
        _isSwitchingLine = false;
    }
}
