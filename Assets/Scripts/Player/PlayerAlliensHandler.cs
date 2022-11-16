using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAlliensHandler : MonoBehaviour, ICharactersHandler
{
    public Action OnCharacterAdded;
    public Action OnPlayerLose;

    public SphereCollider Collider { get; private set; }
    public float DistanceToFarthestRight { get; private set; }
    public float DistanceToFarthestLeft { get; private set; }


    [SerializeField] private CharacterSpawner _characterSpawner;
    [SerializeField] private PlayerLabel _playerLabel;
    [SerializeField] private float _characterXOffset;

    public List<CharacterKeeper> Characters { get; private set; }

    private BunchHandler _enemyBunch;

    private void Awake()
    {
        Characters = new List<CharacterKeeper>();
        Collider = GetComponent<SphereCollider>();
        DistanceToFarthestRight = 0;
        DistanceToFarthestLeft = 0;
    }

    public void AddCharacter(CharacterKeeper character)
    {
        Characters.Add(character);
        OnCharacterAdded?.Invoke();
        _playerLabel.SetAmount(Characters.Count);
        StartCoroutine(RecountDistances());
    }

    public void RemoveCharacter(CharacterKeeper character)
    {
        Characters.Remove(character);
        _playerLabel.SetAmount(Characters.Count);
        StartCoroutine(RecountDistances());

        if (Characters.Count == 0)
        {
            if(_enemyBunch != null)
            {
                _enemyBunch.IsPlayerLose = true;
                _enemyBunch.Reset();
            }
            OnPlayerLose?.Invoke();
        }
    }

    public void MoveTo(Vector3 destination)
    {
        foreach(var character in Characters)
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
        foreach (var characterKeeper in Characters)
        {
            characterKeeper.ResetDestination();
        }
    }
    public IEnumerator RecountDistances()
    {
        yield return new WaitForSeconds(0.1f);

        float maxPositivDistance = 0;
        float maxNegativeDistance = 0;

        foreach (var characterKeeper in Characters)
        {
            float currentDistance = characterKeeper.transform.position.x - transform.position.x ;

            if (currentDistance > maxPositivDistance)
                maxPositivDistance = currentDistance + _characterXOffset;

            if (currentDistance < maxNegativeDistance)
                maxNegativeDistance = currentDistance - _characterXOffset;
        }

        DistanceToFarthestRight = maxPositivDistance;
        DistanceToFarthestLeft = maxNegativeDistance;
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
