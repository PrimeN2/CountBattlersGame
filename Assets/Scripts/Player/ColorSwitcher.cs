using UnityEngine;

public class ColorSwitcher : MonoBehaviour
{
    [SerializeField] private Material playerMaterial;
    private Color _currentColor = Color.white;

    private void Start()
    {
        _currentColor = Color.white;
        playerMaterial = gameObject.GetComponent<Renderer>().material;
    }

    public void ChangeColor(Color color)
    {
        _currentColor = color;
        playerMaterial.SetColor("_EmissionColor", _currentColor);
        Debug.Log(1);
    }
}
