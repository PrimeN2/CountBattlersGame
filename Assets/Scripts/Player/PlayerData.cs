using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : Singleton<PlayerData>
{
    public int BestScore { get; private set; }
    public float BestDistance { get; private set; }

    public void SetBestScore(int newScore)
    {
        if (newScore > BestScore)
            BestScore = newScore;
    }

    private void Save()
    {

    }
}
