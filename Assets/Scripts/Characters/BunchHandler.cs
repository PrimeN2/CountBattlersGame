using System.Collections.Generic;
using UnityEngine;
using System;

public class BunchHandler : MonoBehaviour
{
    private Action<Vector3> _onCrowdTriggered;

    private List<CharacterKeeper> _characterKeepers;

    public void Init(Action<Vector3> onCrowdTriggered)
    {
        _characterKeepers = new List<CharacterKeeper>();
        _onCrowdTriggered = onCrowdTriggered;
    }

    public void AddCharacter(CharacterKeeper character)
    {
        _characterKeepers.Add(character);
    }

    private void SetDestination(Vector3 position)
    {
        foreach (var character in _characterKeepers)
        {
            character.SetDestination(position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        SetDestination(other.transform.position);
        _onCrowdTriggered?.Invoke(transform.position);
    }
}
