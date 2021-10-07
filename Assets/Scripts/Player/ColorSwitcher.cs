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

    public EmissionMaterial CurrentMaterial { get; private set; }
    [SerializeField] private EmissionMaterial _lightPlayerMaterial;
    [SerializeField] private EmissionMaterial _darkPlayerMaterial;

    [SerializeField] private Material _playerMaterial;

    [SerializeField] private float _duration = 3;

    private bool _isColorSwitching = false;

    private PlayerMovement _playerMovement;
    private Renderer _playerRenderer;

    private EmissionMaterial _newMaterial;
    private Coroutine _currentCoroutine;

    private void Start()
    {
        _playerRenderer = GetComponent<Renderer>();
        _playerMovement = GetComponent<PlayerMovement>();
        CurrentMaterial = new EmissionMaterial(_playerMaterial, Color.cyan);
        _newMaterial = new EmissionMaterial(_playerMaterial, Color.cyan);
        _isColorSwitching = false;
    }

    public bool TrySwitch(VerticalDirection direction, LineSwitcher.Line line)
    {
        EmissionMaterial material = DefineMaterial(direction);

        if (_newMaterial.Equals(material))
            return false;

        _newMaterial = material;

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
            _playerRenderer.material.Lerp(CurrentMaterial.Material, _newMaterial.Material, time / _duration);

            yield return null;
        }
        _isColorSwitching = false;
        CurrentMaterial = _newMaterial;
    }

    private EmissionMaterial DefineMaterial(VerticalDirection direction)
    {
        if (direction is VerticalDirection.Up)
        {
            return _darkPlayerMaterial;
        }
        else if (direction is VerticalDirection.Down)
        {
            return _lightPlayerMaterial;
        }
        else
        {
            return CurrentMaterial;
        }
    }
}
