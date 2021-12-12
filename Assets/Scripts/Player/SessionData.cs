﻿using System;
using UnityEngine;

public class SessionData : MonoBehaviour
{
    public Action OnScoreIncreased;

    public int PlayerScore { get; private set; }
    public float PlayerDistance { get; private set; }
    public int BestScore { get => _playerData.BestScore; }

    [SerializeField] private UILoader _UILoader;

    private ISaveSystem _saveSystem;
    private PlayerData _playerData;

    private void Awake()
    {
        SetSaveSystem(new BinarySaveSystem());
        _playerData = _saveSystem.Load();
        PlayerScore = 0;
        PlayerDistance = 0;
    }

    public void SetSaveSystem(ISaveSystem saveSystem)
    {
        _saveSystem = saveSystem;
    }

    public void IncreaseScore(int score)
    {
        PlayerScore += score;
        if (PlayerScore > _playerData.BestScore)
        {
            _playerData.BestScore = PlayerScore;
        }
        OnScoreIncreased?.Invoke();
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause == true)
        {
            _UILoader.LoadMenu();
            _saveSystem.Save(_playerData);
        }

    }
    private void OnApplicationQuit()
    {
        _saveSystem.Save(_playerData);
#if UNITY_EDITOR
        _saveSystem.DeleteSaves();
#endif
    }
}
