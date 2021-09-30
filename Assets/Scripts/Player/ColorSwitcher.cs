using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer), typeof(PlayerMovement))]
public class ColorSwitcher : MonoBehaviour
{
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

    public void TryChangeColor(EmissionMaterial material)
    {
        if (_newMaterial.Equals(material))
            return;

        _newMaterial = material;

        if (_isColorSwitching)
            StopCoroutine(_currentCoroutine);
        _currentCoroutine = StartCoroutine(SwitchMaterial());
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
}
