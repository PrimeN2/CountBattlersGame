using System.Collections;
using UnityEngine;

public class SwipeDetection : MonoBehaviour
{
    [SerializeField] private LineController _lineController;
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private float _minimumDistance = .015f;
    [SerializeField, Range(0f, 1f)]
    private float _directionThreshold = .7f;

    [SerializeField] private DebugLogger Logger;

    private Vector2 _startPosition;
    private Vector2 _currentPosition;
    private bool _isSwiped = false;
    private LineController.Lines _swipedLine = LineController.Lines.Center;
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
                _lineController.ChangeLine(_swipedLine);
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
    private LineController.Lines SwipeDirection()
    {
        Vector2 direction = (_currentPosition - _startPosition).normalized;

        if (Vector2.Dot(Vector2.left, direction) > _directionThreshold)
        {
            Logger.Log("Left Swipe");
            return LineController.Lines.Left;
        }
        else if (Vector2.Dot(Vector2.right, direction) > _directionThreshold)
        {
            Logger.Log("Right Swipe");
            return LineController.Lines.Right;
        }
        else
        {
            Logger.Log("No Direction Swipe");
            return LineController.Lines.Center;
        }
    }

    private bool CanSwipe()
    {
        _swipedLine = SwipeDirection();
        return _isSwiped == false && DetectSwipe() && _swipedLine != LineController.Lines.Center;
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
