using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    [SerializeField] private List<DefaultPlatform> _platformSetting;
    [SerializeField] private GameObject _blockPlatformsPrefab;
    [SerializeField] private GameObject _startPlatformsBlock;
    [SerializeField] private GameObject _startPlatform;
    [SerializeField] private int _platformsCount;

    private Dictionary<GameObject, PlatformsBlockKeeper> _platformsBlocks;
    private PlatformsBlockCreator _platformsBlockCreator;
    private List<GameObject> _currentBlocksPlatforms;
    private float _platformZPosition = 0;
    private float _platformLength;

    private void Start()
    {
        _platformsBlockCreator = new PlatformsBlockCreator(_platformSetting);
        _currentBlocksPlatforms = new List<GameObject>();
        _platformsBlocks = new Dictionary<GameObject, PlatformsBlockKeeper>();

        _currentBlocksPlatforms.Add(_startPlatformsBlock);
        _platformsBlocks.Add(_startPlatformsBlock, _startPlatformsBlock.GetComponent<PlatformsBlockKeeper>());

        _platformLength = _startPlatform.GetComponent<BoxCollider>().bounds.size.z;
        _platformZPosition = _startPlatform.transform.position.x + _platformLength;
        _platformZPosition = 0;

        for (int i = 0; i < _platformsCount; ++i)
        {
            SpawnPlatformsBlock();
        }
    }

    private void SpawnPlatformsBlock()
    {
        GameObject blockPlatforms = Instantiate(_blockPlatformsPrefab);
        blockPlatforms.transform.SetParent(transform);
        blockPlatforms.transform.position = new Vector3(0, 0, _platformZPosition);
        _platformsBlocks.Add(blockPlatforms, blockPlatforms.GetComponent<PlatformsBlockKeeper>());
        PutPlatfomrsBlock(blockPlatforms);
        _currentBlocksPlatforms.Add(blockPlatforms);
    }
    private void ReusePlatformsBlock(GameObject blockPlatforms)
    {
        PutPlatfomrsBlock(blockPlatforms);
        _currentBlocksPlatforms.Remove(blockPlatforms);
        _currentBlocksPlatforms.Add(blockPlatforms);
    }

    private void PutPlatfomrsBlock(GameObject blockPlatforms)
    {
        if (_currentBlocksPlatforms.Count > 0)
            blockPlatforms.transform.position = _currentBlocksPlatforms[_currentBlocksPlatforms.Count - 1].transform.position + Vector3.forward * _platformLength;

        _platformsBlockCreator.DefinePlatformsBlock(_platformsBlocks[blockPlatforms]);
    }
    private void OnEnable()
    {
        PlatformsBlockKeeper.OnPlatformOverFliew += ReusePlatformsBlock;
    }
    private void OnDisable()
    {
        PlatformsBlockKeeper.OnPlatformOverFliew -= ReusePlatformsBlock;
    }
}
