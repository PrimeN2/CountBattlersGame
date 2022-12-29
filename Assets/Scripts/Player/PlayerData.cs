using System;
using System.Collections.Generic;

[Serializable]
public class PlayerData
{
    public List<(int skinID, int chromaID)> AvailableSkins;

    public int[] ActiveChromaForSkin;
    public (int skinID, int chromaID) CurrentCharacterData;

    public int Score;
    public bool IsMusicOn;
    public bool AreSoundsOn;

    public PlayerData(SaveDataArguments arguments)
    {
        CurrentCharacterData = arguments.CurrentCharacterData;

        SetDefaults();
        SetLists(arguments.CountOfSkins);
    }

    private void SetLists(int countOfSkins)
    {
        AvailableSkins = new List<(int skinID, int chromaID)> { CurrentCharacterData };
        ActiveChromaForSkin = new int[countOfSkins];
    }

    private void SetDefaults()
    {
#if UNITY_EDITOR
        Score = 1000000;
#else
        Score = 0;
#endif
        IsMusicOn = true;
        AreSoundsOn = true;
    }
}

public readonly struct SaveDataArguments
{
    public readonly (int skinID, int chromaID) CurrentCharacterData;
    public readonly int CountOfSkins;

    public SaveDataArguments((int skinID, int chromaID) defaultData, int countOfSkins)
    {
        CurrentCharacterData = defaultData;
        CountOfSkins = countOfSkins;
    }
}

