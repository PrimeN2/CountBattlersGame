using System;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    public static bool IsPlayerDead;

    public Action OnPlayerDamaged;
    public Action OnPlayerDied;

    public int PlayerHealth { get => _playerHealth; }
    private int _playerHealth;

    private void Awake()
    {
        _playerHealth = 3;
        IsPlayerDead = false;
    }

    public void DamagePlayer(int damage)
    {
        if (damage >= _playerHealth)
        {
            IsPlayerDead = true;
            OnPlayerDied?.Invoke();
        }
        _playerHealth -= damage;
        OnPlayerDamaged?.Invoke();
    }
}
