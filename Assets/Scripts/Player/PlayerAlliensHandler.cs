using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAlliensHandler : MonoBehaviour, ICharactersHandler
{
    public Action OnPlayerLose;
    public SphereCollider Collider { get; private set; }
    public float DistanceToFartherstRight { get; private set; }
    public float DistanceToFartherstLeft { get; private set; }


    [SerializeField] private CharacterSpawner _characterSpawner;
    [SerializeField] private float _characterXOffset;

    private List<CharacterKeeper> _characters;

    private BunchHandler _enemyBunch;

    private void Awake()
    {
        _characters = new List<CharacterKeeper>();
        Collider = GetComponent<SphereCollider>();
        DistanceToFartherstRight = 0;
        DistanceToFartherstLeft = 0;
    }

    public void AddCharacter(CharacterKeeper character)
    {
        _characters.Add(character);
        RecountDistances();
    }

    public void RemoveCharacter(CharacterKeeper character)
    {
        _characters.Remove(character);
        RecountDistances();

        if (_characters.Count == 0)
        {
            _enemyBunch.IsPlayerLose = true;
            OnPlayerLose?.Invoke();
        }

    }

    public void MoveTo(Vector3 destination)
    {
        foreach(var character in _characters)
        {
            character.SetDestination(destination);
        }
    }

    public Vector3 GetPositionForSpawn()
    {
        return transform.position;
    }

    public void ResetDestination()
    {
        foreach (var characterKeeper in _characters)
        {
            characterKeeper.GetComponent<NavMeshAgent>().ResetPath();
        }
    }
    public void RecountDistances()
    {
        float maxPositivDistance = 0;
        float maxNegativeDistance = 0;

        foreach (var characterKeeper in _characters)
        {
            float currentDistance = transform.position.x - characterKeeper.transform.position.x;

            if (currentDistance > maxPositivDistance)
                maxPositivDistance = currentDistance + _characterXOffset;

            if (currentDistance < maxNegativeDistance)
                maxNegativeDistance = currentDistance - _characterXOffset;
        }

        DistanceToFartherstRight = maxPositivDistance;
        DistanceToFartherstLeft = maxNegativeDistance;
    }

    public void SetEnemyBunch(BunchHandler currentBunch)
    {
        _enemyBunch = currentBunch;
    }

    private void OnEnable()
    {
        _characterSpawner.OnBunchDefeated += ResetDestination;
    }

    private void OnDisable()
    {
        _characterSpawner.OnBunchDefeated -= ResetDestination;
    }
}
