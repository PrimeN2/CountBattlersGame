using System;
using UnityEngine;
using UnityEngine.AI;

public class CharacterKeeper : MonoBehaviour
{
    public Animator Animator { get; private set; }

    private Action<CharacterKeeper> _onCharacterReleased;
    private NavMeshAgent _AI;
    private ICharactersHandler _handler;

    public void Init(Action<CharacterKeeper> onCharacterReleased)
    {
        _AI = GetComponent<NavMeshAgent>();
        Animator = GetComponent<Animator>();
        _onCharacterReleased = onCharacterReleased;
    }

    public void Set(ICharactersHandler handler)
    {
        _handler = handler;
    }

    public void SetDestination(Vector3 position)
    {
        _AI.SetDestination(position);
    }

    public void ResetDestination()
    {
        _AI.ResetPath();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<CharacterKeeper>(out _) || other.TryGetComponent<BarrierKeeper>(out _))
        {
            _onCharacterReleased?.Invoke(this);
            _handler.RemoveCharacter(this);
        }

    }
}
