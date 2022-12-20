using UnityEngine;

public class RoadSetter : MonoBehaviour
{
    [SerializeField] private GameObject _finishBlock;

    [Header("Spawners")]
    [SerializeField] private Road _road;
    [SerializeField] private SelectionBlockSpawner _selectionBlockSpawner;
    [SerializeField] private CharacterSpawner _characterSpawner;
    [SerializeField] private BarrierSpawner _barrierSpawner;

    private RoadValueGenerator _roadValue;

    public void SetRoad()
    {
        _road.Initialize();
        _characterSpawner.Init();
        _barrierSpawner.Init();
        _roadValue = new RoadValueGenerator();

        SpawnRoadObjects();
    }

    private void SpawnRoadObjects()
    {
        for (int i = 0; i < _road.RoadSegments.Count; i++)
        {
            if (TryPutFinish(i)) break;

            int value, decreasedValue, multiplier;
            _roadValue.GetValues(out value, out decreasedValue, out multiplier);

            _selectionBlockSpawner.SetSelectionBlockOnSegment(
                _road.RoadSegments[i], value, decreasedValue, multiplier);

            _barrierSpawner.SpawnBarrierOnSegment(_road.RoadSegments[i]);

            _characterSpawner.SpawnEnemies(_road.RoadSegments[i], decreasedValue);
        }
    }
    private bool TryPutFinish(int index)
    {
        if (index == _road.RoadSegments.Count - 1)
        {
            Instantiate(
                _finishBlock, 
                _road.RoadSegments[index].GetPlatformStart(), 
                Quaternion.identity);
            return true;
        }
        return false;
    }
}

