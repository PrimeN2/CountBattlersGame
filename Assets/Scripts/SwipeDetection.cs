using System.Collections;
using UnityEngine;

public class SwipeDetection : MonoBehaviour
{
    [SerializeField] private LineSwitcher _lineSwitcher;
    [SerializeField] private ColorSwitcher _colorSwitcher;
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private float _minimumDistance = .015f;
    [SerializeField, Range(0f, 1f)]
    private float _directionThreshold = .7f;

    private Vector2 _startPosition;
    private Vector2 _currentPosition;
    private bool _isSwiped = false;

    private SwipeDirection _currentSwipeDirection;
    private Coroutine _swipeCoroutine;

    private void Awake()
    {
        _currentSwipeDirection = new SwipeDirection(_lineSwitcher, _colorSwitcher);
    }
    private void SwipeStart(Vector2 position)
    {
        _isSwiped = false;
        _startPosition = position;
        _swipeCoroutine = StartCoroutine(SwipeControls());
    }

    private void SwipeEnd(Vector2 position)
    {
        StopCoroutine(_swipeCoroutine);
    }

    private bool TryDetectSwipe()
    {
        if (Vector3.Distance(_startPosition, _currentPosition) >= _minimumDistance)
            return true;
        else
            return false;
    }

    private IEnumerator SwipeControls()
    {
        while (true)
        {
            _currentPosition = _inputManager.PrimaryPosition();
            if (CanSwipe())
            {
                _currentSwipeDirection.Define(_currentPosition, _startPosition, _directionThreshold);

                _currentSwipeDirection.Switcher.TrySwitch(_currentSwipeDirection.VerticalDirection, _currentSwipeDirection.Line);
                _isSwiped = true;
            }
            yield return null;
        }
    }

    private bool CanSwipe()
    {
        return _isSwiped == false && TryDetectSwipe();
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

public class SwipeDirection
{
    public ISwitcher Switcher { get; private set; }
    public LineSwitcher.Line Line { get; private set; }
    public ColorSwitcher.VerticalDirection VerticalDirection { get; private set; }

    private LineSwitcher _lineSwitcher;
    private ColorSwitcher _colorSwitcher;

    public SwipeDirection(LineSwitcher lineSwitcher, ColorSwitcher colorSwitcher)
    {
        _lineSwitcher = lineSwitcher;
        _colorSwitcher = colorSwitcher;
    }

    public void Define(Vector2 currentPosition, Vector2 startPosition, float directionThreshold)
    {
        Vector2 direction = (currentPosition - startPosition).normalized;

        if (Vector2.Dot(Vector2.left, direction) > directionThreshold)
        {
            SetDirection(LineSwitcher.Line.Left);
        }

        else if (Vector2.Dot(Vector2.right, direction) > directionThreshold)
        {
            SetDirection(LineSwitcher.Line.Right);
        }

        else if (Vector2.Dot(Vector2.up, direction) > directionThreshold)
        {
            SetDirection(ColorSwitcher.VerticalDirection.Up);
        }

        else if (Vector2.Dot(Vector2.down, direction) > directionThreshold)
        {
            SetDirection(ColorSwitcher.VerticalDirection.Down);
        }
    }

    private void SetDirection(LineSwitcher.Line line)
    {
        Line = line;
        Switcher = _lineSwitcher;
    }

    private void SetDirection(ColorSwitcher.VerticalDirection direction)
    {
        VerticalDirection = direction;
        Switcher = _colorSwitcher;
    }
}