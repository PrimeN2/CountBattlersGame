using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System;

[RequireComponent(typeof(Image))]
public class ChromaView : MonoBehaviour, IPointerDownHandler 
{
    public event Action<bool, Color> OnChromaBought;

    [SerializeField] private PlayerSkinsHandler _skinsHandler;
    [SerializeField] private ChromaPanel _panel;
    [SerializeField] private TMP_Text _costLabel;

    [SerializeField] private int _cost;
    [SerializeField] private int _chromaID;
    [SerializeField] private int _skinID;

    private Image _chromaView;
    private Color _color;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_skinsHandler.TryBuyChroma(_skinID, _chromaID, _cost))
        {
            SetView(_color, "");
            _panel.Toggle();
            OnChromaBought?.Invoke(true, _color);
        }
    }

    private void OnEnable()
    {
        _chromaView = GetComponent<Image>();
        _color = _skinsHandler.GetChromaColorBy(_chromaID);

        if (_skinsHandler.AvailableSkins.Contains((_skinID, _chromaID)))
            SetView(_color, "");
        else
            SetView(
                new Color(_color.r,
                _color.g,
                _color.b, 
                100f), 
                _cost.ToString());
        OnChromaBought += _panel.UpdateState;
    }

    private void SetView(Color color, string title)
    {
        _chromaView.color = color;
        _costLabel.text = title;
    }
}
