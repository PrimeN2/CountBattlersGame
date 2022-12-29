using UnityEngine;
using UnityEngine.EventSystems;

public class OutterHandler : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private ChromaPanel _chromaPanel;

    public void OnPointerDown(PointerEventData eventData)
    {
        _chromaPanel.Toggle();
    }
}
