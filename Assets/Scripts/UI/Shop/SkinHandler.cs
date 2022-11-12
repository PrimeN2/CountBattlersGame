using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SkinHandler : MonoBehaviour
{
    [SerializeField] private ShopManager _shopManager;
    [SerializeField] private SessionDataManager _sessionData;

    [SerializeField] private int _skinCost;
    [SerializeField] private int _skinID;

    private TextMeshProUGUI _label;
    private SkinArguments _arguments;

    private void OnEnable()
    {
        _arguments = new SkinArguments(_skinCost, _skinID);
        GetComponent<Button>().onClick.AddListener(delegate { _shopManager.TryBuySkin(_arguments); } );
        GetComponent<Button>().onClick.AddListener(delegate { UpdateValues(_arguments); });

        _label = GetComponentInChildren<TextMeshProUGUI>(true);

        UpdateValues(_arguments);
    }

    private void UpdateValues(SkinArguments arguments)
    {
        if (_sessionData.CurrentSkinsAvalible.Contains(_skinID))
            _label.text = $"Use skin {_skinID}";
        else
            _label.text = $"Buy skin {_skinID} for {_skinCost} coins";
    }
}
