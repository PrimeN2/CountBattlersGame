using System.Collections;
using UnityEngine;

public class SwipeDetection : MonoBehaviour
{
    [SerializeField] private LineSwitcher _lineSwitcher;
    [SerializeField] private ColorSwitcher _colorSwitcher;
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private float _minimumDistance = .015f;
    [SerializeField] private EmissionMaterial _darkPlayerMaterial;
    [SerializeField] private EmissionMaterial _lightPlayerMaterial;
    [SerializeField, Range(0f, 1f)]
    private float _directionThreshold = .7f;

    private Vector2 _startPosition;
    private Vector2 _currentPosition;
    private bool _isSwiped = false;

    private SwipeDirection _currentSwipeDirection;
    private Coroutine _swipeCoroutine;

    private void Awake()
    {
        _currentSwipeDirection = new SwipeDirection(_darkPlayerMaterial, _lightPlayerMaterial);
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

    private bool DetectSwipe()
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

                if (_currentSwipeDirection.IsMaterialChanged)
                {
                    _colorSwitcher.TryChangeColor(_currentSwipeDirection.GetMaterial);
                }

                else
                {
                    _lineSwitcher.TryChangeLine(_currentSwipeDirection.GetLineDirection);
                }

                _isSwiped = true;
            }
            yield return null;
        }
    }

    private bool CanSwipe()
    {
        return _isSwiped == false && DetectSwipe();
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
    public LineSwitcher.Lines GetLineDirection => _line;
    public EmissionMaterial GetMaterial => _material;
    public bool IsMaterialChanged;

    private EmissionMaterial _darkPlayerMaterial;
    private EmissionMaterial _lightPlayerMaterial;
    private EmissionMaterial _material;
    private LineSwitcher.Lines _line;

    public SwipeDirection(EmissionMaterial darkPlayerMaterial, EmissionMaterial lightPlayerMaterial)
    {
        _line = LineSwitcher.Lines.Center;
        _material = new EmissionMaterial(null, new Color(0, 0, 0, 1));
        IsMaterialChanged = false;
        _lightPlayerMaterial = lightPlayerMaterial;
        _darkPlayerMaterial = darkPlayerMaterial;
    }

    public void Define(Vector2 currentPosition, Vector2 startPosition, float directionThreshold)
    {
        Vector2 direction = (currentPosition - startPosition).normalized;

        if (Vector2.Dot(Vector2.left, direction) > directionThreshold)
        {
            SetDirection(LineSwitcher.Lines.Left);
        }

        else if (Vector2.Dot(Vector2.right, direction) > directionThreshold)
        {
            SetDirection(LineSwitcher.Lines.Right);
        }

        else if (Vector2.Dot(Vector2.up, direction) > directionThreshold)
        {
            SetDirection(_darkPlayerMaterial);
        }

        else if (Vector2.Dot(Vector2.down, direction) > directionThreshold)
        {
            SetDirection(_lightPlayerMaterial);
        }
    }

    private void SetDirection(LineSwitcher.Lines line)
    {
        _line = line;
        IsMaterialChanged = false;
    }

    private void SetDirection(EmissionMaterial material)
    {
        _material = material;
        IsMaterialChanged = true;
    }
}