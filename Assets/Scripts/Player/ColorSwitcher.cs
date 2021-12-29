using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer), typeof(PlayerMovement))]
public class ColorSwitcher : MonoBehaviour, ISwitcher
{
    public enum VerticalDirection
    {
        Up = 1,
        Down = -1,
        Center = 0
    }

    public static readonly Color BlueColor = new Color(0, 0.6588f, 1, 1);
    public static readonly Color OrangeColor = new Color(1, 0.447f, 0, 1);

    public Color CurrentColor { get; private set; }

    [SerializeField] private float _duration = 3;

    private bool _isColorSwitching = false;

    private PlayerMovement _playerMovement;
    private Renderer _playerRenderer;

    private Color _newColor;
    private Coroutine _currentCoroutine;

    private void Start()
    {
        _playerRenderer = GetComponent<Renderer>();
        _playerMovement = GetComponent<PlayerMovement>();
        CurrentColor = GetComponent<Renderer>().material.color;
        _newColor = CurrentColor;
        _isColorSwitching = false;
    }

    public bool TrySwitch(VerticalDirection direction, LineSwitcher.Line line)
    {
        Color color = DefineColor(direction);

        if (_newColor == color)
            return false;

        _newColor = color;

        if (_isColorSwitching)
            StopCoroutine(_currentCoroutine);
        _currentCoroutine = StartCoroutine(SwitchMaterial());

        return true;
    }

    private IEnumerator SwitchMaterial()
    {
        float time = 0;

        _isColorSwitching = true;

        while (time < _duration)
        {
            time += Time.deltaTime * _playerMovement.PlayerSpeed;
            _playerRenderer.material.color = Color.Lerp(CurrentColor, _newColor, time / _duration);

            yield return null;
        }
        _isColorSwitching = false;
        CurrentColor = _newColor;
    }

    private Color DefineColor(VerticalDirection direction)
    {
        if (direction is VerticalDirection.Up)
        {
            return BlueColor;
        }
        else if (direction is VerticalDirection.Down)
        {
            return OrangeColor;
        }
        else
        {
            return CurrentColor;
        }
    }
}
