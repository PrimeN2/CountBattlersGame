using System.Collections.Generic;
using UnityEngine;

public class RoadSegmentSpawner : MonoBehaviour
{
    [SerializeField] private List<DefaultPlatform> _platformSetting;
    [SerializeField] private GameObject _roadSegmentPrefab;
    [SerializeField] private GameObject _startRoadSegment;
    [SerializeField] private GameObject _startPlatform;
    [SerializeField] private BarrierSpawner _barrierSpawner;
    [SerializeField] private int _platformsCount;

    private Dictionary<GameObject, RoadSegmentKeeper> _roadSegments;
    private RoadSegmentCreator _roadSegmentCreator;
    private List<GameObject> _currentRoadSegments;
    private float _platformZPosition = 0;
    private float _platformLength;

    private void Awake()
    {
        _roadSegmentCreator = new RoadSegmentCreator(_platformSetting);
        _currentRoadSegments = new List<GameObject>();
        _roadSegments = new Dictionary<GameObject, RoadSegmentKeeper>();

        _currentRoadSegments.Add(_startRoadSegment);
        _roadSegments.Add(_startRoadSegment, _startRoadSegment.GetComponent<RoadSegmentKeeper>());

        _platformLength = _startPlatform.GetComponent<MeshCollider>().bounds.size.z;
        _platformZPosition = _startPlatform.transform.position.x + _platformLength;
        _platformZPosition = 0;

        _barrierSpawner.SpawnBarrier(_currentRoadSegments[0], false);
        for (int i = 0; i < _platformsCount; ++i)
        {
            SpawnRoadSegment();

            if (i <= 3)
                _barrierSpawner.SpawnBarrier(_currentRoadSegments[i + 1], false);
            else
                _barrierSpawner.SpawnBarrier(_currentRoadSegments[i + 1], true);
        }
    }

    private void SpawnRoadSegment()
    {
        GameObject roadSegment = Instantiate(_roadSegmentPrefab);
        roadSegment.transform.SetParent(transform);
        roadSegment.transform.position = new Vector3(0, 0, _platformZPosition);
        _roadSegments.Add(roadSegment, roadSegment.GetComponent<RoadSegmentKeeper>());
        PutRoadSegment(roadSegment);
        _currentRoadSegments.Add(roadSegment);
    }
    private void ReuseRoadSegment(GameObject roadSegment)
    {
        _barrierSpawner.InitBarrierOnSegment(roadSegment, true);
        PutRoadSegment(roadSegment);
        _currentRoadSegments.Remove(roadSegment);
        _currentRoadSegments.Add(roadSegment);
    }

    private void PutRoadSegment(GameObject roadSegment)
    {
        if (_currentRoadSegments.Count > 0)
            roadSegment.transform.position = _currentRoadSegments[_currentRoadSegments.Count - 1].transform.position + Vector3.forward * _platformLength;

        _roadSegmentCreator.DefineRoadSegment(_roadSegments[roadSegment]);
    }
    private void OnEnable()
    {
        RoadSegmentKeeper.OnSegmentOverFliew += ReuseRoadSegment;
    }
    private void OnDisable()
    {
        RoadSegmentKeeper.OnSegmentOverFliew -= ReuseRoadSegment;
    }
}
