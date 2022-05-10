using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Pool;

public class CharacterSpawner : MonoBehaviour
{
    public Action<Vector3> OnBunchTriggered;
    private Action<CharacterKeeper> OnCharacterReleased;

    [SerializeField] private PlayerAlliensHandler _playerAlliensHandler;
    [SerializeField] private GameObject _characterPrefab;
    [SerializeField] private GameObject _bunchPrefab;

    [SerializeField] private Vector3 _triggerOffset;
    [SerializeField]
    [Range(1, 200)] private int _maxCountOfEnemiesInAGroup = 100;

    private Transform _currentTransformForCharacters;

    private IObjectPool<CharacterKeeper> _characterPool;

    private void Awake()
    {
        _characterPool = new ObjectPool<CharacterKeeper>(
            CreateCharacter,
            character => { character.gameObject.SetActive(true); },
            character =>
            { character.gameObject.SetActive(false); },
            character =>
            { Destroy(character); }, 
            false, 500, 1000);

    }

    private void Start()
    {
        Spawn(1);
    }

    private CharacterKeeper CreateCharacter()
    {
        CharacterKeeper characterKeeper = Instantiate(
            _characterPrefab, _currentTransformForCharacters.position, Quaternion.identity, _currentTransformForCharacters)
            .GetComponent<CharacterKeeper>();
        characterKeeper.Init(OnCharacterReleased);
        return characterKeeper;
    }

    public void Spawn(RoadSegmentKeeper roadSegment)
    {
        BunchHandler bunch = Instantiate(_bunchPrefab, transform).GetComponent<BunchHandler>();
        _currentTransformForCharacters = bunch.transform;
        bunch.Init(OnBunchTriggered);
        bunch.transform.position = roadSegment.GetPlatformStart() + _triggerOffset;

        for (int i = 0; i < _maxCountOfEnemiesInAGroup; ++i)
        {
            var character = _characterPool.Get();
            character.gameObject.GetComponent<NavMeshAgent>().Warp(character.transform.position + GetOffsetFor(i));
            bunch.AddCharacter(character);
        }
    }


    public void Spawn(int count)
    {
        _currentTransformForCharacters = _playerAlliensHandler.transform;

        for (int i = 0; i < count; ++i)
        {
            var character = _characterPool.Get();
            character.gameObject.GetComponent<NavMeshAgent>().Warp(_playerAlliensHandler.GetPositionForCharacters() + GetOffsetFor(i) - new Vector3(0, 0, 5));
            _playerAlliensHandler.AddCharacter(character);
        }
    }

    private void ReleaseCharacter(CharacterKeeper character)
    {
        character.transform.parent = null;
        _characterPool.Release(character);
    }

    private Vector3 GetOffsetFor(int i)
    {
        return new Vector3(UnityEngine.Random.Range(-1, 2) * 0.01f * i, 0.05f, 5 + UnityEngine.Random.Range(-1, 2) * 0.01f * i);
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

