using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer), typeof(PlayerMovement))]
public class ColorSwitcher : MonoBehaviour
{
    public EmissionMaterial CurrentMaterial { get; private set; }

    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private Renderer _playerRenderer;
    [SerializeField] private Material _playerMaterial;

    private float duration = 0;
    private bool _isColorSwitching = false;
    private bool _hasPreviousSwitchingEnded = true;

    private EmissionMaterial _newMaterial;
    private Coroutine _currentCoroutine;

    private void Start()
    {
        _playerRenderer = GetComponent<Renderer>();
        _playerMovement = GetComponent<PlayerMovement>();
        _playerRenderer.material = _playerMaterial;
        CurrentMaterial = new EmissionMaterial(_playerMaterial, Color.cyan);
        _newMaterial = new EmissionMaterial(_playerMaterial, Color.cyan);
        _isColorSwitching = false;
    }

    public void TryChangeColor(EmissionMaterial material)
    {
        if (_newMaterial.Equals(material))
            return;

        if (_hasPreviousSwitchingEnded && _isColorSwitching)
        {
            StartCoroutine(WaitForEndOfSwitch(material));
            return;
        }

        if (!_isColorSwitching)
        {
            _newMaterial = material;
            _currentCoroutine = StartCoroutine(SwitchMaterial());
        }
    }

    private IEnumerator SwitchMaterial()
    {
        duration = 0;
        _playerMaterial = _playerRenderer.material;
        _isColorSwitching = true;

        while (duration < 1)
        {
            duration += Time.deltaTime;

            _playerMaterial.Lerp(_playerMaterial, _newMaterial.Material, duration * _playerMovement.PlayerSpeed / 8);

            yield return null;
        }
        CurrentMaterial = _newMaterial;
        _isColorSwitching = false;
    }

    private IEnumerator WaitForEndOfSwitch(EmissionMaterial material)
    {
        _hasPreviousSwitchingEnded = false;

        yield return _currentCoroutine;

        TryChangeColor(material);
        _hasPreviousSwitchingEnded = true;
    }
}
