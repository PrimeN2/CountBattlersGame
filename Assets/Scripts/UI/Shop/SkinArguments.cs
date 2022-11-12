using UnityEngine;

public class SkinArguments
{
    public int SkinCost { get; private set; }
    public int SkinID { get; private set; }

    public SkinArguments(int skinCost, int skinID)
    {
        SkinCost = skinCost;
        SkinID = skinID;
    }
}
