using System;
using System.Collections.Generic;
using UnityEngine;

public class SessionData : MonoBehaviour
{
    public Action OnScoreChanged;

    public int CurrentSkin { get => _playerData.CurrentSkin; }
    public int PlayerScore { get => _playerData.Score; }
    public List<int> CurrentSkinsAvalible { get => _playerData.CurrentSkinsAvalible; }

    private ISaveSystem _saveSystem;

    private PlayerData _playerData;

    private void Awake()
    {
        SetSaveSystem(new BinarySaveSystem());
        _playerData = _saveSystem.Load();
    }

    public void SetSaveSystem(ISaveSystem saveSystem)
    {
        _saveSystem = saveSystem;
    }
    public void MarkSkinAsBought(int itemID)
    {
        if (_playerData.CurrentSkinsAvalible.Contains(itemID))
        {
            Debug.LogWarning($"Already have skin {itemID}");
            return;
        }

        _playerData.CurrentSkinsAvalible.Add(itemID);
        _saveSystem.Save(_playerData);
    }

    public void UseSkin(int itemID)
    {
        _playerData.CurrentSkin = itemID;
        _saveSystem.Save(_playerData);
    }

    public void DecreaseScore(int score)
    {
        if (_playerData.Score < score)
            return;

        _playerData.Score -= score;

        OnScoreChanged?.Invoke();
        _saveSystem.Save(_playerData);
    }

    public void IncreaseScore(int score)
    {
        _playerData.Score += score;

        OnScoreChanged?.Invoke();
        _saveSystem.Save(_playerData);
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause == true)
        {
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
