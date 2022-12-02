using System.Collections.Generic;
using UnityEngine;

public class PlatformSetter : MonoBehaviour
{
    [SerializeField] private GameObject _finishBlock;

    [Header("Spawners")]
    [SerializeField] private RoadSegmentSpawner _roadSegmentSpawner;
    [SerializeField] private SelectionBlockSpawner _selectionBlockSpawner;
    [SerializeField] private CharacterSpawner _characterSpawner;
    [SerializeField] private BarrierSpawner _barrierSpawner;

    private List<RoadSegmentKeeper> _roadSegments;
    private int _residualValue = 0;
    private int _countOfPlatforms = 0;
    private int _countOfMultipliedBlocks = 0;
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

        _roadSegments.Add(roadSegmentKeeper);
        _countOfPlatforms += 1;

    }

    private void SetFinishBlock(RoadSegmentKeeper roadSegmentKeeper)
    {
        Instantiate(_finishBlock, roadSegmentKeeper.GetPlatformStart(), Quaternion.identity);
    }

    private void SpawnRoadObjects()
    {



        foreach (var roadSegment in _roadSegments)
        {
            int value = Random.Range(10, 50);
            int decreasedValue = (int)(value * Random.Range(0.5f, 0.9f));

            int multiplier;
            if (Random.Range(0, 2) == 1 && _countOfMultipliedBlocks < 2 && _residualValue <= 50)
            {
                multiplier = CountMultiplier(_residualValue);
                _countOfMultipliedBlocks += 1;
                _residualValue += value * multiplier - decreasedValue;
            }
            else
            {
                multiplier = 0;
                _residualValue += value - decreasedValue;
            }

            _selectionBlockSpawner.SetSelectionBlockOnSegment(roadSegment, value, decreasedValue, multiplier);
            _barrierSpawner.SpawnBarrierOnSegment(roadSegment);
            _characterSpawner.SpawnEnemies(roadSegment, decreasedValue);
        }
    }

    private int CountMultiplier(int value)
    {
        int multiplier = 5;

        for (int i = 5; i > 0; i--)
            if (multiplier * value >= 100)
                multiplier--;
        return multiplier;
    }

    private void OnEnable()
    {
        _countOfPlatforms = 0;
        _countOfMultipliedBlocks = 0;
        _residualValue = 0;
        _roadSegments = new List<RoadSegmentKeeper>();
        _isFinishSet = false;

        _roadSegmentSpawner.OnRoadSegmentAdded += DefineObjects;
        _roadSegmentSpawner.OnRoadCompleted += SpawnRoadObjects;
    }

    private void OnDisable()
    {
        _roadSegmentSpawner.OnRoadSegmentAdded -= DefineObjects;
        _roadSegmentSpawner.OnRoadCompleted -= SpawnRoadObjects;
    }
}

