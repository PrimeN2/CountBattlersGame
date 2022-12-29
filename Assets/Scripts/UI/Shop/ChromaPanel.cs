using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ChromaPanel : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private GameObject _chromasPanel;
    [SerializeField] private OutterHandler _outter;

    private Image _chromaView;

    private bool _isActive = false;
    private bool _isOpened = false;

    private void Awake() =>
        _chromaView = GetComponent<Image>();

    public void Toggle()
    {
        _isOpened = !_isOpened;
        _chromasPanel.SetActive(_isOpened);
        _outter.gameObject.SetActive(_isOpened);
    }

    public void UpdateState(bool isActive, Color ActiveColour)
    {
        _isActive = isActive;
        _chromaView.color = ActiveColour;
    }

    public void UpdateState()
    {
        _isActive = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_isActive && !_isOpened) Toggle();
    }
}
