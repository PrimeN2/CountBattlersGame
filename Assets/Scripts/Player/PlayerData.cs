using System;
using System.Collections.Generic;

[Serializable]
public class PlayerData
{
    public PlayerData(int score, int currentSkin)
    {
        Score = score;

        CurrentSkinsAvalible = new List<int> { currentSkin };
        CurrentSkin = currentSkin;
    }

    public List<int> CurrentSkinsAvalible;
    public int Score;
    public int CurrentSkin;
}

