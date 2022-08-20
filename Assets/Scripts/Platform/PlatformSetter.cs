using UnityEngine;

public class PlatformSetter : MonoBehaviour
{
    [SerializeField] private GameObject _finishBlock;

    [Header("Spawners")]
    [SerializeField] private RoadSegmentSpawner _roadSegmentSpawner;
    [SerializeField] private SelectionBlockSpawner _selectionBlockSpawner;
    [SerializeField] private CharacterSpawner _characterSpawner;
    [SerializeField] private BarrierSpawner _barrierSpawner;

    private int _countOfPlatforms = 0;
    private bool _isFinishSet = false;

    private void DefineObjects(RoadSegmentKeeper roadSegmentKeeper)
    {
        if (_isFinishSet)
            return;

        if (_countOfPlatforms > 4)
        {
            SetFinishBlock(roadSegmentKeeper);
            _isFinishSet = true;
            return;
        }

        int firstValue = Random.Range(10, 20);
        int secondValue = Random.Range(10, 20);

        _selectionBlockSpawner.SetSelectionBlockOnSegment(roadSegmentKeeper, firstValue, secondValue);
        _barrierSpawner.SpawnBarrierOnSegment(roadSegmentKeeper);
        _characterSpawner.Spawn(roadSegmentKeeper, Mathf.Max(firstValue, secondValue) + Random.Range(-5, 0));
        _countOfPlatforms += 1;
    }

    private void SetFinishBlock(RoadSegmentKeeper roadSegmentKeeper)
    {
        Instantiate(_finishBlock, roadSegmentKeeper.GetPlatformStart(), Quaternion.identity);
    }

    private void OnEnable()
    {
        _countOfPlatforms = 0;
        _isFinishSet = false;

        _roadSegmentSpawner.OnRoadSegmentAdded += DefineObjects;
    }

    private void OnDisable()
    {
        _roadSegmentSpawner.OnRoadSegmentAdded -= DefineObjects;
    }
}

