using System;
using UnityEngine;

public class SessionData : MonoBehaviour
{
    public Action OnPlayerDied;
    public Action OnRecordAchieved;

    public int PlayerScore { get; private set; }
    public int PlayerHealth { get; private set; }
    public float PlayerDistance { get; private set; }


    private ISaveSystem _saveSystem;
    private PlayerData _playerData;

    private void Start()
    {
        SetPlayerData();
        PlayerScore = 0;
        PlayerDistance = 0;
        PlayerHealth = 3;
    }
    private void SetPlayerData()
    {
        _saveSystem = new BinarySaveSystem();
        _playerData = _saveSystem.Load();
        Debug.Log(_playerData.BestScore);
    }

    public void DamagePlayer(int damage)
    {
        if (damage >= PlayerHealth)
        {
            OnPlayerDied?.Invoke();
        }
        PlayerHealth -= damage;
    }
    public void IncreaseScore(int score)
    {
        PlayerScore += score;
        if (PlayerScore > _playerData.BestScore)
        {
            _playerData.BestScore = PlayerScore;
            OnRecordAchieved?.Invoke();
        }
    }


    //TO-DO: Test how it works on mobile platform
    private void OnApplicationPause(bool pause)
    {
        if (pause == true)
            _saveSystem.Save(_playerData);
    }
    private void OnApplicationQuit()
    {
        _saveSystem.Save(_playerData);
#if UNITY_EDITOR
        _saveSystem.DeleteSaves();
#endif
    }
}
