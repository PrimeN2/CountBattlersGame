using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Pool;

public class CharacterSpawner : MonoBehaviour
{
    public Action<Vector3> OnBunchTriggered;
    public Action OnBunchDefeated;

    private Action<CharacterKeeper> OnCharacterReleased;

    [SerializeField] private PlayerAlliensHandler _playerAlliensHandler;
    [SerializeField] private SessionDataManager _sessionData; 

    [SerializeField] private Material _playerMaterial;
    [SerializeField] private Material _enemyMaterial;

    [SerializeField] private Dictionary<int, GameObject> _characterPrefabList;
    [SerializeField] private GameObject _bunchPrefab;
    [SerializeField] private Vector3 _bunchOffset;

    [SerializeField] private AudioClip _spawnSound;
    [SerializeField] private AudioClip _dyingSound;

    private IObjectPool<CharacterKeeper> _charactersPool;
    private ICharactersHandler _currentCharacterHandler;

    private float _spawnRatio = 0.1f;
    
    private void Awake()
    {
        _charactersPool = new ObjectPool<CharacterKeeper>(CreateCharacter, GetCharacter,
            character => { character.gameObject.SetActive(false); },
            character => { Destroy(character); }, false, 500, 1000);
        _characterPrefabList = new Dictionary<int, GameObject>()
        {
            { 1, Resources.Load("CharacterRoundSkin") as GameObject},
            { 2, Resources.Load("CharacterTallSkin") as GameObject }
        };
    }

    private void Start()
    {
        Spawn(1);
    }

    private IEnumerator Reset()
    {
        for (int i = 0; i < 8; ++i)
            yield return new WaitForEndOfFrame();

        _playerAlliensHandler.ResetDestination();
        _playerAlliensHandler.RecountDistances();
    }

    private CharacterKeeper CreateCharacter()
    {
        CharacterKeeper characterKeeper = Instantiate(_characterPrefabList[_sessionData.CurrentSkin]).GetComponent<CharacterKeeper>();
        characterKeeper.Init(OnCharacterReleased);
        return characterKeeper;
    }

    private void GetCharacter(CharacterKeeper character)
    {
        character.gameObject.SetActive(true);
        character.Set(_currentCharacterHandler);
    }

    public void SpawnEnemies(RoadSegmentKeeper roadSegment, int count)
    {
        BunchHandler bunch = CreateBunch(roadSegment);
        _currentCharacterHandler = bunch;

        for (int i = 0; i < count; ++i)
        {
            var character = _charactersPool.Get();

            Transform transform = character.transform;
            character.gameObject.GetComponent<NavMeshAgent>().Warp(
                bunch.GetPositionForSpawn() + new Vector3(UnityEngine.Random.Range(-0.1f, 0.1f), 0, UnityEngine.Random.Range(-0.1f, 0.1f)));
            character.transform.SetParent(bunch.transform);
            SetCharacter(character, transform);
        }

        void SetCharacter(CharacterKeeper character, Transform characterTransform)
        {
            character.SetMaterial(_enemyMaterial);
            characterTransform.SetParent(bunch.transform);
            characterTransform.Rotate(new Vector3(0, 180, 0));
            bunch.AddCharacter(character);
        }
    }

    private IEnumerator SpawnEffects(int count)
    {
        _spawnRatio = 0.01f * count;

        for (int i = 0; i < Mathf.Round(count * 0.1f) + 1; i++)
        {
            AudioManager.Instance.PlaySound(_spawnSound);
            yield return new WaitForSeconds(0.01f);
        }
    }

    public void Spawn(int count)
    {
        _currentCharacterHandler = _playerAlliensHandler;

        StartCoroutine(SpawnEffects(count));

        for (int i = 0; i < count; ++i)
        {
            var character = _charactersPool.Get();
            character.SetMaterial(_playerMaterial);
            character.gameObject.GetComponent<NavMeshAgent>().Warp(
                _playerAlliensHandler.GetPositionForSpawn() + 
                new Vector3(UnityEngine.Random.insideUnitCircle.x * _spawnRatio, 0,
                UnityEngine.Random.insideUnitCircle.y * _spawnRatio));
        character.transform.SetParent(_playerAlliensHandler.transform);
            _playerAlliensHandler.AddCharacter(character);
        }
        //_playerAlliensHandler.MoveTo(_playerAlliensHandler.transform.position + new Vector3(0, 0, -0.1f));
        //StartCoroutine(Reset());
        //Can be useful for convergence to the center, after getting hit from obstacle
    }

    private void ReleaseCharacter(CharacterKeeper character)
    {
        character.transform.parent = null;
        character.transform.rotation = new Quaternion(0, 0, 0, 0);

        _charactersPool.Release(character);

        AudioManager.Instance.PlaySound(_dyingSound);
    }

    private BunchHandler CreateBunch(RoadSegmentKeeper roadSegment)
    {
        BunchHandler bunch = Instantiate(_bunchPrefab, transform).GetComponent<BunchHandler>();
        bunch.Init(OnBunchTriggered, OnBunchDefeated);
        bunch.transform.position = roadSegment.GetPlatformStart() + _bunchOffset;

        return bunch;
    }

    //private Vector3 GetOffsetFor(int i, int amount, float multiplier)
    //{
    //    float angle = i * Mathf.PI * 2f / amount;
    //    float radius = multiplier * _distanceBetweenCharacters;

    //    Vector3 offset = new Vector3(Mathf.Cos(angle) * radius, 0.05f, Mathf.Sin(angle) * radius);

    //    return offset;
    //}
    //Another way to spawn characters(doesn't really work well)

    private void OnEnable()
    {
        OnCharacterReleased += ReleaseCharacter;
    }

    private void OnDisable()
    {
        OnCharacterReleased -= ReleaseCharacter;
    }
}

