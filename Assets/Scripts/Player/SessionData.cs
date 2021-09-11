using System;
using UnityEngine;

public class SessionData : MonoBehaviour
{
    public Action OnPlayerDied;
    public Action OnPlayerDamaged;
    public Action OnScoreIncreased;

    public int PlayerScore { get; private set; }
    public int PlayerHealth { get; private set; }
    public float PlayerDistance { get; private set; }
    public int BestScore { get => _playerData.BestScore; }

    [SerializeField] private DebugLogger _debuger;

    private ISaveSystem _saveSystem;
    private PlayerData _playerData;

    private void Awake()
    {
        SetSaveSystem(new BinarySaveSystem());
        _playerData = _saveSystem.Load();
        PlayerScore = 0;
        PlayerDistance = 0;
        PlayerHealth = 3;
    }

    public void SetSaveSystem(ISaveSystem saveSystem)
    {
        _saveSystem = saveSystem;
    }

    public void DamagePlayer(int damage)
    {
        if (damage >= PlayerHealth)
        {
            OnPlayerDamaged?.Invoke();
        }
        PlayerHealth -= damage;
        OnPlayerDamaged?.Invoke();
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


    //TO-DO: Test how it works on mobile platform
    private void OnApplicationPause(bool pause)
    {
        if (pause == true)
        {
            Debug.Log("Application paused");
            _saveSystem.Save(_playerData);
            _debuger.Log("Application paused");
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
