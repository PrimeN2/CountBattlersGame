using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class ColorSwitcher : MonoBehaviour
{
    public EmissionMaterial NewMaterial { get; private set; }

    [SerializeField] private Renderer _playerRenderer;
    [SerializeField] private Material _playerMaterial;

    private float duration = 0;

    private void Start()
    {
        _playerRenderer = gameObject.GetComponent<Renderer>();
        _playerRenderer.material = _playerMaterial;
    }

    public void ChangeColor(EmissionMaterial material)
    {
        NewMaterial = material;
        StartCoroutine(SwitchMaterial());
    }

    private IEnumerator SwitchMaterial()
    {
        duration = 0;
        _playerMaterial = _playerRenderer.material;

        while (duration < 1)
        {
            duration += Time.deltaTime;

            _playerMaterial.Lerp(_playerMaterial, NewMaterial.Material, duration);

            yield return null;
        }
    } 
}
