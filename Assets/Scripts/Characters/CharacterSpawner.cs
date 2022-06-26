using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Pool;

public class CharacterSpawner : MonoBehaviour
{
    public Action<Vector3> OnBunchTriggered;
    public Action OnBunchDefeated;

    private Action<CharacterKeeper> OnCharacterReleased;

    [SerializeField] private PlayerAlliensHandler _playerAlliensHandler;
    [SerializeField] private GameObject _characterPrefab;
    [SerializeField] private GameObject _bunchPrefab;

    [SerializeField] private Vector3 _bunchOffset;
    [SerializeField] private float _distanceBetweenCharacters;
    [SerializeField]
    [Range(1, 200)] private int _maxCountOfEnemiesInAGroup = 100;

    private IObjectPool<CharacterKeeper> _charactersPool;
    private ICharactersHandler _currentCharacterHandler;
    
    private void Awake()
    {
        _charactersPool = new ObjectPool<CharacterKeeper>(CreateCharacter, GetCharacter,
            character => { character.gameObject.SetActive(false); },
            character => { Destroy(character); }, false, 500, 1000);
    }

    private void Start()
    {
        Spawn(15, true);

        //_currentCharacterHandler = _playerAlliensHandler;

        //for (int i = 0; i < 15; ++i)
        //{
        //    var character = _charactersPool.Get();
        //    character.gameObject.GetComponent<NavMeshAgent>().Warp(
        //        _playerAlliensHandler.GetPositionForSpawn());
        //    character.transform.SetParent(_playerAlliensHandler.transform);
        //    _playerAlliensHandler.AddCharacter(character);
        //}
        //_playerAlliensHandler.MoveTo(new Vector3(0, 0, 0.01f));
        //StartCoroutine(Reset());
    }

    private IEnumerator Reset()
    {
        yield return new WaitForEndOfFrame();

        _playerAlliensHandler.ResetDestination();
        _playerAlliensHandler.RecountDistances();
    }

    private CharacterKeeper CreateCharacter()
    {
        CharacterKeeper characterKeeper = Instantiate(_characterPrefab).GetComponent<CharacterKeeper>();
        characterKeeper.Init(OnCharacterReleased);
        return characterKeeper;
    }

    private void GetCharacter(CharacterKeeper character)
    {
        character.gameObject.SetActive(true);
        character.Set(_currentCharacterHandler);
    }

    public void Spawn(RoadSegmentKeeper roadSegment)
    {
        BunchHandler bunch = CreateBunch(roadSegment);
        _currentCharacterHandler = bunch;

        var firstCharacter = _charactersPool.Get();
        Transform firstTransform = firstCharacter.transform;
        firstCharacter.gameObject.GetComponent<NavMeshAgent>().Warp(
            bunch.GetPositionForSpawn());
        firstTransform.SetParent(bunch.transform);
        firstTransform.Rotate(new Vector3(0, 180, 0));
        bunch.AddCharacter(firstCharacter);

        for (int j = 1; j <= (_maxCountOfEnemiesInAGroup - 1) / 12 + 1; ++j)
        {
            for (int i = 0; i < j * 6; ++i)
            {
                var character = _charactersPool.Get();
                Transform transform = character.transform;
                character.gameObject.GetComponent<NavMeshAgent>().Warp(
                    bunch.GetPositionForSpawn() + GetOffsetFor(i, j * 6, j));
                transform.SetParent(bunch.transform);
                transform.Rotate(new Vector3(0, 180, 0));
                bunch.AddCharacter(character);
            }
        }
    }

    public void Spawn(int count, bool isFirst)
    {
        _currentCharacterHandler = _playerAlliensHandler;

        if (isFirst)
        {
            count -= 1;
            var character = _charactersPool.Get();
            character.gameObject.GetComponent<NavMeshAgent>().Warp(
                _playerAlliensHandler.GetPositionForSpawn());
            character.transform.SetParent(_playerAlliensHandler.transform);
            _playerAlliensHandler.AddCharacter(character);

            if (count <= 0)
                return;
        }

        for (int j = 1; j <= count / 12 + 1; ++j)
        {
            for (int i = 0; i < j * 6; ++i)
            {
                var character = _charactersPool.Get();
                character.gameObject.GetComponent<NavMeshAgent>().Warp(
                    _playerAlliensHandler.GetPositionForSpawn() + GetOffsetFor(i, j * 6, j));
                character.transform.SetParent(_playerAlliensHandler.transform);
                _playerAlliensHandler.AddCharacter(character);
            }

            _playerAlliensHandler.Collider.radius = j * _distanceBetweenCharacters;
        }
    }

    private void ReleaseCharacter(CharacterKeeper character)
    {
        character.transform.parent = null;
        character.transform.rotation = new Quaternion(0, 0, 0, 0);

        _charactersPool.Release(character);
    }

    private BunchHandler CreateBunch(RoadSegmentKeeper roadSegment)
    {
        BunchHandler bunch = Instantiate(_bunchPrefab, transform).GetComponent<BunchHandler>();
        bunch.Init(OnBunchTriggered, OnBunchDefeated);
        bunch.transform.position = roadSegment.GetPlatformStart() + _bunchOffset;

        return bunch;
    }

    private Vector3 GetOffsetFor(int i, int amount, float multiplier)
    {
        float angle = i * Mathf.PI * 2f / amount;
        float radius = multiplier * _distanceBetweenCharacters;

        Vector3 offset = new Vector3(Mathf.Cos(angle) * radius, 0.05f, Mathf.Sin(angle) * radius);

        return offset;
    }

    private void OnEnable()
    {
        OnCharacterReleased += ReleaseCharacter;
    }

    private void OnDisable()
    {
        OnCharacterReleased -= ReleaseCharacter;
    }
}

