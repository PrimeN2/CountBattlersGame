using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private SessionDataManager _sessionData;

    public void TryBuySkin(SkinArguments arguments)
    {
        if (arguments.SkinCost > _sessionData.PlayerScore)
        {
            Debug.Log("not enough score to purchase");
            return;
        }

        if (_sessionData.CurrentSkinsAvalible.Contains(arguments.SkinID))
        {
            _sessionData.UseSkin(arguments.SkinID);
            return;
        }

        _sessionData.DecreaseScore(arguments.SkinCost);
        _sessionData.MarkSkinAsBought(arguments.SkinID);
        _sessionData.UseSkin(arguments.SkinID);
    }
}
