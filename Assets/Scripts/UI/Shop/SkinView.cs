using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using System;

public class SkinView : MonoBehaviour, IPointerDownHandler
{
    public event Func<int, int, bool> OnSkinBuyButtonClicked;

    [SerializeField] private ChromaPanel _chromaPanel;
    [SerializeField] private PlayerSkinsHandler _playerSkins;
    [SerializeField] private AudioClip _uiClick;

    [SerializeField] private int _cost;
    [SerializeField] private int _skinID;

    private TextMeshProUGUI _label;


    public void OnPointerDown(PointerEventData eventData)
    {
        AudioManager.Instance.PlaySound(_uiClick);
        OnSkinBuyButtonClicked?.Invoke(_skinID, _cost);
    }

    private void Start()
    {
        _label = GetComponentInChildren<TextMeshProUGUI>(true);
        UpdateValues();
    }

    private void OnEnable()
    {
        _playerSkins.OnSkinChanged += UpdateValues;
        OnSkinBuyButtonClicked += _playerSkins.TryBuySkin;
    }
    private void OnDisable()
    {
        _playerSkins.OnSkinChanged -= UpdateValues;
        OnSkinBuyButtonClicked -= _playerSkins.TryBuySkin;
    }

    private void UpdateValues()
    {
        bool isCurrentSkinBought = _playerSkins.AvailableSkins.Contains((_skinID, 0));

        if (isCurrentSkinBought)
        {
            if (_playerSkins.CurrentSkinID == _skinID)
            {
                _label.text = $"Skin active";
                _chromaPanel.UpdateState(true, _playerSkins.GetCurrentChromaColorForSkin(_skinID));

                return;
            }
            _label.text = $"Use skin {_skinID}";
            _chromaPanel.UpdateState(false, _playerSkins.GetCurrentChromaColorForSkin(_skinID));

            return;
        }

        else
        {
            _label.text = $"Buy skin {_skinID} for {_cost} coins";
            _chromaPanel.UpdateState();

            return;
        }
    }
}
