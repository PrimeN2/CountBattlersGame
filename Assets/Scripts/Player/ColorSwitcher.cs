using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class ColorSwitcher : MonoBehaviour
{
    [SerializeField] private Renderer _playerRenderer;
    [SerializeField] private Material _playerMaterial;
    [SerializeField] private Material _newMaterial;

    [HideInInspector] public Color CurrentColor = Color.white;
    private float duration = 0;

    private void Start()
    {
        CurrentColor = Color.clear;
        _playerRenderer = gameObject.GetComponent<Renderer>();
        _playerRenderer.material = _playerMaterial;
    }

    public void ChangeColor(Material material)
    {
        _newMaterial = material;
        StartCoroutine(SwitchMaterial());
    }

    private IEnumerator SwitchMaterial()
    {
        duration = 0;
        _playerMaterial = _playerRenderer.material;

        while (duration < 1)
        {
            duration += Time.deltaTime;

            _playerMaterial.Lerp(_playerMaterial, _newMaterial, duration);

            yield return null;
        }
    } 
}
