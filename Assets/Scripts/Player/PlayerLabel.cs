using TMPro;
using UnityEngine;

public class PlayerLabel : MonoBehaviour
{
    private TextMeshPro _charactersAmount;

    private void Awake()
    {
        _charactersAmount = GetComponentInChildren<TextMeshPro>();
        SetAmount(0);
    }

    public void SetAmount(int amount)
    {
        _charactersAmount.text = $"{amount}";
    }

    public void HideLabel()
    {
        gameObject.SetActive(false);
    }

    public void ShowLabel()
    {
        gameObject.SetActive(true);
    }
}
