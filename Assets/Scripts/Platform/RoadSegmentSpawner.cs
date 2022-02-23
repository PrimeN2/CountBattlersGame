using System.Collections.Generic;
using UnityEngine;

public class RoadSegmentSpawner : MonoBehaviour
{
    [HideInInspector] public float LeftBorder;
    [HideInInspector] public float RightBorder;

    [SerializeField] private SelectionBlockSpawner _selectionBlockSpawner;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private GameObject _roadSegment;
    [SerializeField] private RoadSegmentKeeper _startRoadSegmentKeeper;
    [SerializeField] private GameObject _startRoadSegmen;
    [SerializeField] private BarrierSpawner _barrierSpawner;
    [SerializeField] private int _segmentsCount;
    [SerializeField] private int _countOfSelectionBlocksOnAPlatform;
    [SerializeField] private float _bounds;
    
    private List<GameObject> _currentRoadSegments;
    private List<RoadSegmentKeeper> _roadSegmentKeepers;
    private float _segmentLength;

    private void Awake()
    {
        _selectionBlockSpawner.InitSpawner();
        _barrierSpawner.InitSpawner();
        _currentRoadSegments = new List<GameObject>();
        _roadSegmentKeepers = new List<RoadSegmentKeeper>();

        Initialize(_startRoadSegmentKeeper);

        _currentRoadSegments.Add(_startRoadSegmen);
        _roadSegmentKeepers.Add(_startRoadSegmentKeeper);

        Vector3 StartSegmentColliderSize = _startRoadSegmen.GetComponent<Collider>().bounds.size;
        _segmentLength = StartSegmentColliderSize.z;
        LeftBorder = -StartSegmentColliderSize.x;
        RightBorder = StartSegmentColliderSize.x;

        _selectionBlockSpawner.SpawnNewSelectionBlock(_roadSegmentKeepers[0], _countOfSelectionBlocksOnAPlatform, false);
        _barrierSpawner.SpawnBarrier(_currentRoadSegments[0], false);
        for (int i = 0; i < _segmentsCount; ++i)
        {
            SpawnRoadSegment();
            _selectionBlockSpawner.SpawnNewSelectionBlock(_roadSegmentKeepers[i + 1], _countOfSelectionBlocksOnAPlatform, i >= 4);
            _barrierSpawner.SpawnBarrier(_currentRoadSegments[i + 1], i >= 4);
        }
    }

    private void SpawnRoadSegment()
    {
        GameObject roadSegment = Instantiate(_roadSegment);
        RoadSegmentKeeper roadSegmentKeeper = roadSegment.GetComponent<RoadSegmentKeeper>();

        Initialize(roadSegmentKeeper);
        PutRoadSegment(roadSegment);

        _currentRoadSegments.Add(roadSegment);
        _roadSegmentKeepers.Add(roadSegmentKeeper);

    }
    private void ReuseRoadSegment(GameObject roadSegment)
    {
        _selectionBlockSpawner.SetSelectionBlockOnSegment(
            roadSegment.GetComponent<RoadSegmentKeeper>(), true);
        _barrierSpawner.SetBarrierOnSegment(roadSegment, true);

        PutRoadSegment(roadSegment);

        _currentRoadSegments.Remove(roadSegment);
        _currentRoadSegments.Add(roadSegment);
    }

    private void PutRoadSegment(GameObject roadSegment)
    {
        if (_currentRoadSegments.Count > 0)
            roadSegment.transform.position = 
                _currentRoadSegments[_currentRoadSegments.Count - 1].transform.position + 
                Vector3.forward * _segmentLength;
    }

    private void Initialize(RoadSegmentKeeper roadSegmentKeeper)
    {
        roadSegmentKeeper.Init(transform, _playerMovement, _bounds);
        roadSegmentKeeper.OnSegmentOverFlew += ReuseRoadSegment;
    }

    private void OnDisable()
    {
        foreach(var roadSegmentKeeper in _roadSegmentKeepers)
        {
            roadSegmentKeeper.Unsubscribe(ReuseRoadSegment);
        }
    }
}
