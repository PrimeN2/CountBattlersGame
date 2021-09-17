using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer), typeof(PlayerMovement))]
public class ColorSwitcher : MonoBehaviour
{
    public EmissionMaterial CurrentMaterial { get; private set; }

    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private Renderer _playerRenderer;
    [SerializeField] private Material _playerMaterial;

    private float duration = 0f;

    private bool _isColorSwitching = false;

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

        _newMaterial = material;

        if (_isColorSwitching)
            StopCoroutine(_currentCoroutine);
        _currentCoroutine = StartCoroutine(SwitchMaterial());
    }

    private IEnumerator SwitchMaterial()
    {
        _playerMaterial = _playerRenderer.material;
        _isColorSwitching = true;
        duration = 0;

        while (duration < 1)
        {
            duration += Time.deltaTime * _playerMovement.PlayerSpeed;
            _playerMaterial.Lerp(CurrentMaterial.Material, _newMaterial.Material, duration);

            yield return null;
        }
        _isColorSwitching = false;
        CurrentMaterial = _newMaterial;
    }
}
