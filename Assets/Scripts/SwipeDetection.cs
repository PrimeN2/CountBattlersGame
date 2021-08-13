using System.Collections;
using UnityEngine;

public class SwipeDetection : MonoBehaviour
{
    [SerializeField] private LineSwitcher _lineSwitcher;
    [SerializeField] private ColorSwitcher _colorSwitcher;
    [SerializeField] private DebugLogger Logger;
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private float _minimumDistance = .015f;
    [SerializeField, Range(0f, 1f)]
    private float _directionThreshold = .7f;

    private Vector2 _startPosition;
    private Vector2 _currentPosition;
    private bool _isSwiped = false;
    private LineSwitcher.Lines _newLine = LineSwitcher.Lines.Center;
    private Color _newColor = Color.white;
    private Coroutine _swipeCoroutine;

    private void SwipeStart(Vector2 position)
    {
        Logger.Log("SwipeStart");
        _isSwiped = false;
        _startPosition = position;
        _swipeCoroutine = StartCoroutine(SwipeControls());
    }
    private IEnumerator SwipeControls()
    {
        while (true)
        {
            _currentPosition = _inputManager.PrimaryPosition();
            if (CanSwipe())
            {
                _lineSwitcher.ChangeLine(_newLine);
                _colorSwitcher.ChangeColor(_newColor);
                _isSwiped = true;
            }
            yield return null;
        }
    }
    private void SwipeEnd(Vector2 position)
    {
        StopCoroutine(_swipeCoroutine);
    }

    private bool DetectSwipe()
    {
        if (Vector3.Distance(_startPosition, _currentPosition) >= _minimumDistance)
            return true;
        else
            return false;
    }
    private void SetSwipeDirection()
    {
        Vector2 direction = (_currentPosition - _startPosition).normalized;

        if (Vector2.Dot(Vector2.left, direction) > _directionThreshold)
        {
            Logger.Log("Left Swipe");
            _newLine = LineSwitcher.Lines.Left;
        }

        else if (Vector2.Dot(Vector2.right, direction) > _directionThreshold)
        {
            Logger.Log("Right Swipe");
            _newLine = LineSwitcher.Lines.Right;
        }

        else if (Vector2.Dot(Vector2.up, direction) > _directionThreshold)
        {
            Logger.Log("Up Swipe");
            _newLine = LineSwitcher.Lines.Center;
            _newColor = Color.black;
        }

        else if(Vector2.Dot(Vector2.down, direction) > _directionThreshold)
        {
            Logger.Log("Down Swipe");
            _newLine = LineSwitcher.Lines.Center;
            _newColor = Color.white;
        }

        else
        {
            Logger.Log("No Direction Swipe");
            _newLine = LineSwitcher.Lines.Center;
        }
    }

    private bool CanSwipe()
    {
        SetSwipeDirection();
        return _isSwiped == false && DetectSwipe() && _newLine != LineSwitcher.Lines.Center;
    }

    private void OnEnable()
    {
        _inputManager.OnTouchStarted += SwipeStart;
        _inputManager.OnTouchEnded += SwipeEnd;
    }

    private void OnDisable()
    {
        _inputManager.OnTouchStarted -= SwipeStart;
        _inputManager.OnTouchEnded -= SwipeEnd;
    }
}
