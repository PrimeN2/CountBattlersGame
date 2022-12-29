using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkinsHandler : MonoBehaviour
{
    public event Action OnSkinChanged;

    public List<(int skinID, int chromaID)> AvailableSkins { get => _sessionData.AvailableSkins; }

    public int CurrentChromaID { get => _sessionData.CurrentPlayerChroma; }
    public int CurrentSkinID { get => _sessionData.CurrentSkin; }

    private const string MainColor = "_HColor";

    [SerializeField] private SessionData _sessionData;
    [SerializeField] private CharacterData _characterData;

    public bool TryBuyChroma(int skinID, int chromaID, int cost)
    {
        if (_sessionData.AvailableSkins.Contains((skinID, chromaID)))
        {
            _sessionData.UseChromaForSkin(skinID, chromaID);
            return true;
        }

        if (cost > _sessionData.PlayerScore)
            return false;

        _sessionData.DecreaseScore(cost);
        _sessionData.MarkChromaAsBoughtForSkin(skinID, chromaID);
        _sessionData.UseChromaForSkin(skinID, chromaID);

        return true;

    }

    public Color GetChromaColorBy(int index)
    {
        return _characterData.CharacterChromasMaterials[index].GetColor(MainColor);
    }

    public Color GetCurrentChromaColorForSkin(int skinID)
    {
        return _characterData.CharacterChromasMaterials[_sessionData.CurrentCharacterData[skinID]]
            .GetColor(MainColor);
    }

    public bool TryBuySkin(int skinID, int cost)
    {
        if (cost > _sessionData.PlayerScore)
        {
            Debug.Log("not enough score to purchase");
            return false;
        }

        if (_sessionData.AvailableSkins.Contains((skinID, 0)))
        {
            _sessionData.UseSkin(skinID);
            OnSkinChanged?.Invoke();

            return true;
        }

        _sessionData.DecreaseScore(cost);
        _sessionData.MarkSkinAsBought(skinID);
        _sessionData.UseSkin(skinID);

        OnSkinChanged?.Invoke();

        return true;
    }
}
