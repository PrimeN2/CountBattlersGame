using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BunchHandler : MonoBehaviour, ICharactersHandler
{
    public Action OnBunchDefeated;

    public bool Triggered { get; private set; }
    [HideInInspector] public bool IsPlayerLose;

    [SerializeField] private PlayerLabel _label;
    private Action<Vector3> _onCrowdTriggered;

    private List<CharacterKeeper> _characters;

    public void Init(Action<Vector3> onCrowdTriggered, Action onBunchDefeated)
    {
        _characters = new List<CharacterKeeper>();
        _onCrowdTriggered = onCrowdTriggered;
        OnBunchDefeated = onBunchDefeated;
        Triggered = false;
    }

    public void AddCharacter(CharacterKeeper character)
    {
        _characters.Add(character);
        _label.SetAmount(_characters.Count);
    }
    public void RemoveCharacter(CharacterKeeper character)
    {
        _characters.Remove(character);
        _label.SetAmount(_characters.Count);

        if (_characters.Count == 0 && !IsPlayerLose)
        {
            OnBunchDefeated?.Invoke();
            Destroy(gameObject);
        }
    }

    public void MoveTo(Vector3 position)
    {
        foreach (var character in _characters)
        {
            character.SetDestination(position);
        }
        Triggered = true;
        _onCrowdTriggered?.Invoke(transform.position);
    }

    public void Reset()
    {
        foreach (var character in _characters)
        {
            character.ResetDestination();
        }
    }

    public Vector3 GetPositionForSpawn()
    {
        return transform.position;
    }
}
