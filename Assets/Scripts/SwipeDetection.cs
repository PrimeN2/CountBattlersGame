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
        _currentSwipeDirection = new SwipeDirection();
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
                DefineSwipeDirection();

                if (_currentSwipeDirection.IsMaterialChanged)
                    _colorSwitcher.ChangeColor(_currentSwipeDirection.GetMaterial);
                else
                    _lineSwitcher.TryChangeLine(_currentSwipeDirection.GetLineDirection);

                _isSwiped = true;
            }
            yield return null;
        }
    }

    private void DefineSwipeDirection()
    {
        Vector2 direction = (_currentPosition - _startPosition).normalized;

        if (Vector2.Dot(Vector2.left, direction) > _directionThreshold)
        {
            _currentSwipeDirection.SetDirection(LineSwitcher.Lines.Left);
        }

        else if (Vector2.Dot(Vector2.right, direction) > _directionThreshold)
        {
            _currentSwipeDirection.SetDirection(LineSwitcher.Lines.Right);
        }

        else if (Vector2.Dot(Vector2.up, direction) > _directionThreshold)
        {
            _currentSwipeDirection.SetDirection(_darkPlayerMaterial);
        }

        else if(Vector2.Dot(Vector2.down, direction) > _directionThreshold)
        {
            _currentSwipeDirection.SetDirection(_lightPlayerMaterial);
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

    private EmissionMaterial _material = new EmissionMaterial(null, new Color(0, 0, 0, 1));
    private LineSwitcher.Lines _line;

    public SwipeDirection()
    {
        _line = LineSwitcher.Lines.Center;
        IsMaterialChanged = false;
    }

    public void SetDirection(LineSwitcher.Lines line)
    {
        _line = line;
        IsMaterialChanged = false;
    }

    public void SetDirection(EmissionMaterial material)
    {
        if (!_material.Equals(material))
        {
            _material = material;
            IsMaterialChanged = true;
            return;
        }
        IsMaterialChanged = false;
    }
}