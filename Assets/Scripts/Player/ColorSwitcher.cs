using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class ColorSwitcher : MonoBehaviour
{
    public EmissionMaterial NewMaterial { get; private set; }

    [SerializeField] private Renderer _playerRenderer;
    [SerializeField] private Material _playerMaterial;

    private float duration = 0;
    private bool _isSwitching = false;
    private Coroutine _currentCoroutine;

    private void Start()
    {
        _playerRenderer = gameObject.GetComponent<Renderer>();
        _playerRenderer.material = _playerMaterial;
        NewMaterial = new EmissionMaterial(_playerMaterial, Color.cyan);
        _isSwitching = false;
    }

    public void ChangeColor(EmissionMaterial material)
    {
        NewMaterial = material;
        if (_isSwitching)
        {
            StopCoroutine(_currentCoroutine);
            _currentCoroutine = StartCoroutine(SwitchMaterial());
            return;
        }
        _currentCoroutine = StartCoroutine(SwitchMaterial());
    }

    private IEnumerator SwitchMaterial()
    {
        duration = 0;
        _playerMaterial = _playerRenderer.material;
        _isSwitching = true;

        while (duration < 1)
        {
            duration += Time.deltaTime;

            _playerMaterial.Lerp(_playerMaterial, NewMaterial.Material, duration);

            yield return null;
        }
        _isSwitching = false;
    } 
}
