using System;

[Serializable]
public class PlayerData
{
    public PlayerData(int score,float distance)
    {
        BestScore = score;
        BestDistance = distance;
    }

    public int BestScore;
    public float BestDistance;
}

