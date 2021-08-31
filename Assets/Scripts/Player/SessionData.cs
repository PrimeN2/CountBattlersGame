using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionData : MonoBehaviour
{
    public Action OnPlayerDied;
    public Action OnRecordAchieved;

    public int PlayerScore { get; private set; }
    public int PlayerHealth { get; private set; }
    public float PlayerDistance { get; private set; }

    private void Awake()
    {
        PlayerScore = 0;
        PlayerDistance = 0;
        PlayerHealth = 3;
    }

    public void DamagePlayer(int damage)
    {
        if (damage >= PlayerHealth)
        {
            OnPlayerDied?.Invoke();
        }
        PlayerHealth -= damage;
        Debug.Log($"PlayerHealth equals {PlayerHealth}");
    }
    public void IncreaseScore(int score)
    {
        PlayerScore += score;
        if (PlayerScore > PlayerData.Instance.BestScore)
        {
            PlayerData.Instance.SetBestScore(PlayerScore);
            OnRecordAchieved?.Invoke();
        }
        Debug.Log($"PlayerScore equals {PlayerScore}");
    }
}
