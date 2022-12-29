using System;
using System.Collections.Generic;
using UnityEngine;

public class SessionData : MonoBehaviour
{
    public event Action OnScoreChanged;
    public event Action<int> OnScoreIncreased;
    public event Action<bool> OnMusicToggled;
    public event Action<bool> OnSoundsToggled;

    public List<(int skinID, int chromaID)> AvailableSkins { get => _playerData.AvailableSkins; }

    public int[] CurrentCharacterData { get => _playerData.ActiveChromaForSkin; }

    public int CurrentPlayerChroma { get => _playerData.CurrentCharacterData.chromaID; }
    public int CurrentSkin { get => _playerData.CurrentCharacterData.skinID; }
    public int PlayerScore { get => _playerData.Score; }
    public bool IsMusicOn { get => _playerData.IsMusicOn; }
    public bool AreSoundsOn { get => _playerData.AreSoundsOn; }

    [SerializeField] private CharacterData _characterData;

    private ISaveSystem _saveSystem;

    private PlayerData _playerData;

    private void Awake()
    {
        SetSaveSystem(new BinarySaveSystem(new SaveDataArguments((0, 0),
            _characterData.CharacterSkinsPrefabs.Length)));
        _playerData = _saveSystem.Load();
    }

    public void MarkChromaAsBoughtForSkin(int skinID, int chromaID)
    {
        if (_playerData.AvailableSkins.Contains((skinID, chromaID)))
        {
            Debug.LogWarning($"Already have chroma {chromaID} for skin {skinID}");
            return;
        }

        _playerData.AvailableSkins.Add((skinID, chromaID));
        _saveSystem.Save(_playerData);
    }

    public void UseChromaForSkin(int skinID, int chromaID)
    {
        _playerData.ActiveChromaForSkin[skinID] = chromaID;
        _playerData.CurrentCharacterData = (skinID, chromaID);
        _saveSystem.Save(_playerData);
    }

    public void MarkSkinAsBought(int skinID)
    {
        if (_playerData.AvailableSkins.Contains((skinID, 0)))
        {
            Debug.LogWarning($"Already have skin {skinID}");
            return;
        }

        _playerData.AvailableSkins.Add((skinID, 0));
        _saveSystem.Save(_playerData);
    }

    public void UseSkin(int skinID)
    {
        _playerData.CurrentCharacterData = (skinID, _playerData.ActiveChromaForSkin[skinID]);
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

        OnScoreIncreased?.Invoke(score);
        OnScoreChanged?.Invoke();
        _saveSystem.Save(_playerData);
    }

    public void ToggleMusic()
    {
        _playerData.IsMusicOn = !_playerData.IsMusicOn;
        OnMusicToggled?.Invoke(!_playerData.IsMusicOn);
        _saveSystem.Save(_playerData);
    }

    public void ToggleSounds()
    {
        _playerData.AreSoundsOn = !_playerData.AreSoundsOn;
        OnSoundsToggled?.Invoke(!_playerData.AreSoundsOn);
        _saveSystem.Save(_playerData);
    }

    private void SetSaveSystem(ISaveSystem saveSystem)
    {
        _saveSystem = saveSystem;
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
