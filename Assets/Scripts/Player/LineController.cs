using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class LineController : MonoBehaviour
{
    public static Func<int, IEnumerator> OnPlayerMoving;
    public Lines CurrentLine { get => _currentLine; }
    private Lines _currentLine;

    [SerializeField] private float _speed = 0.05f;
    [SerializeField] private Rigidbody _playerRigidbody;
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
        _playerRigidbody = GetComponent<Rigidbody>();
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
                StartCoroutine(SwitchLine(_currentLine, line));
            }
        }
    }
    private IEnumerator SwitchLine(Lines currentLine, Lines line)
    {
        float rotation = 1;
        _isSwitchingLine = true;
        _targetPosition = new Vector3(0.83f * (int)currentLine, transform.position.y, transform.position.z);
        StartCoroutine(OnPlayerMoving?.Invoke((int)line));

        //while (transform.position.x != _targetPosition.x)
        //{
        //    _targetPosition = new Vector3(0.83f * (int)currentLine, transform.position.y, transform.position.z);
        //    //transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _speed);
        //    // rotation -= _speed; 
        //    
        //}
        yield return new WaitForFixedUpdate();
        _isSwitchingLine = false;
    }
}
