using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterKeeper : MonoBehaviour
{
    private Action<CharacterKeeper> _onCharacterReleased;
    private NavMeshAgent _AI;

    public void Init(Action<CharacterKeeper> onCharacterReleased)
    {
        _AI = GetComponent<NavMeshAgent>();
        _onCharacterReleased = onCharacterReleased;
    }

    public void SetDestination(Vector3 position)
    {
        _AI.SetDestination(position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<CharacterKeeper>(out _) || other.TryGetComponent<BarrierKeeper>(out _))
            _onCharacterReleased?.Invoke(this);
    }
}
